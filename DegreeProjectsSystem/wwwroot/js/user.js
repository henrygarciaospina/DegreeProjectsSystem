var dataTable;
var active;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        language: {
            "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json"
        },
        "ajax": {
            "url": "/Admin/User/GetAllUsers"
        },
        "columns": [
            { "data": "userName", "width": "10%" },
            { "data": "names", "width": "10%" },
            { "data": "surnames", "width": "15%" },
            { "data": "email", "width": "10%" },
            { "data": "dependence", "width": "15%" },
            { "data": "role", "width": "10%" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    var today = new Date().getTime();
                    var locked = new Date(data.lockoutEnd).getTime();
                    if (locked > today) {
                        //User is locked
                        return `
                            <div class="text-center">
                               <a onclick=LockedUnlocked('${data.id}') class="btn btn-danger text-white"  style="cursor:pointer; width:130px !important;">
                                    <i class="fas fa-unlock-alt"></i> Desbloquear
                                </a>
                            </div>
                         `;
                    } else {
                        //User is unlocked
                        return `
                            <div class="text-center">
                               <a onclick=LockedUnlocked('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:130px !important;">
                                    <i class="fas fa-lock"></i> Bloquear
                                </a>
                            </div>
                         `;
                    }
                }, "width": "30%"
            }
        ]
    });
}

function LockedUnlocked(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockedUnlocked',
        data: JSON.stringify(id),
        contentType: "application/Json",
        success: function (data) {
            if (data.success) {
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