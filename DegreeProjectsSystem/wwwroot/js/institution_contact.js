var dataTable;
var active;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "autoWidth": true,
        language: {
            "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json"
        },
        "ajax": {
            "url": "/Admin/InstitutionContact/GetAllInstitutionContacts"
        },
        "columns": [
            { "data": "institution.name", "width": "20%" },
            { "data": "person.names", "width": "15%" },
            { "data": "person.surnames", "width": "20%" },
            { "data": "institutionContactCharge.name", "width": "20%" },
            {
                "data": "active",
                "render": function (data) {
                active = data;
                if (data) {
                    return `
                                <div class="status-active text-center">Activo</div>
                            `
                }
                else {
                    return `      
                                <div class="status-inactive text-center">Inactivo</div>
                            `
                }
            }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    if (!active) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/InstitutionContact/InsertOrUpdateInstitutionContact/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/InstitutionContact/DeleteInstitutionContact/${data}") class="btn btn-danger disabled text-white" disabled style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                                <a href="/Admin/InstitutionContac/DetailInstitutionContact/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                     <i class="far fa-eye"></i>
                                </a>
                            </div>
                         `;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a href="/Admin/InstitutionContact/InsertOrUpdateInstitutionContact/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                <i class="far fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Admin/InstitutionContact/DeleteInstitutionContact/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                            </a>
                            <a href="/Admin/InstitutionContact/DetailInstitutionContact/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                <i class="far fa-eye"></i>
                            </a>
                            </div >
                         </div >
                         `;
                    }
                }, "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Esta Seguro que quiere Borrar el Contacto Institución?.",
        text: "Este registro se puede  recuperar actualizando su estado a Activo.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((erase) => {
        if (erase) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.options = {
                            "closeButton": true,
                            "debug": false,
                            "newestOnTop": false,
                            "progressBar": true,
                            "positionClass": "toast-top-right",
                            "preventDuplicates": false,
                            "onclick": null,
                            "showDuration": "200",
                            "hideDuration": "1000",
                            "timeOut": "3000",
                            "extendedTimeOut": "1000",
                            "showEasing": "swing",
                            "hideEasing": "linear",
                            "showMethod": "fadeIn",
                            "hideMethod": "fadeOut"
                        }
                        toastr["success"](data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr["error"](data.message);
                        dataTable.ajax.reload();
                    }
                }
            });
        }
    });
}