using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DegreeProjectsSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitWork _iUnitWork;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IUnitWork iUnitWork)
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _iUnitWork = iUnitWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un nombre de usuario.")]
            [StringLength(15, ErrorMessage = "El {0} debe ser mínimo de {2} y máximo {1} caracteres.", MinimumLength = Startup.requiredLengthForPassword)]
            [Display(Name = "Usuario")]
            public string UserName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un correo electrónico.")]
            [EmailAddress]
            [Display(Name = "Correo electrónico")]
            public string Email { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una clave.")]
            [StringLength(15, ErrorMessage = "El {0} debe ser mínimo de {2} y máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Clave")]
            public string Password { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe confirmar la clave ingresada.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar clave")]
            [Compare("Password", ErrorMessage = "La clave y su confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar los nombre(s).")]
            [Display(Name = "Nombre(s)")]
            public string Names { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar los apellidos(s). ")]
            [Display(Name = "Apellido(s)")]
            public string Surnames { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la dependencia.")]
            [Display(Name = "Dependencia")]
            public string Dependence { get; set; }
            public string Role { get; set; }
            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            Input = new InputModel()
            {
                RoleList = _roleManager.Roles.Where(r => r.Name != DS.Role_Consult)
                   .Select(n => n.Name).Select(l => new SelectListItem
                {
                    Text = l,
                    Value = l

                })
            };

                 ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    Names = Input.Names,
                    Surnames = Input.Surnames,
                    Dependence = Input.Dependence,
                    Role = Input.Role
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuario creado, nueva cuenta con clave.");

                    if (!await _roleManager.RoleExistsAsync(DS.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Admin));
                    }

                    if (!await _roleManager.RoleExistsAsync(DS.Role_Assistant))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Assistant));
                    }

                    if (!await _roleManager.RoleExistsAsync(DS.Role_Consult))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Consult));
                    }

                    //Si es un usuario con rol Consult
                    if (user.Role == null)
                    {
                        await _userManager.AddToRoleAsync(user, DS.Role_Consult);
                    }
                    else
                    {
                        //Crea un usuario con el Rol seleccionado
                        await _userManager.AddToRoleAsync(user, user.Role);
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirme su correo electrónico",
                        $"Confirme su cuenta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haz click aquí</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        //Usuario rol Consult
                        if (user.Role == null)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            //Usuario administrador está registrando un nuevo usuario
                            RedirectToAction("Index", "User", new { Area = "Admin"});

                        }
                    }
                }

                Input = new InputModel()
                {
                    RoleList = _roleManager.Roles.Where(r => r.Name != DS.Role_Consult)
                   .Select(n => n.Name).Select(l => new SelectListItem
                   {
                       Text = l,
                       Value = l

                   })
                };

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
         }
    }
}
