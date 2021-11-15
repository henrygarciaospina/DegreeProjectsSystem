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
            "url": "/Admin/TeachingAssignment/GetAllTeachingAssignments"
        },
        "columns": [
            { "data": "solicitude.titleDegreeWork" },
            //{ "data": "teachingAssignment.studentRequest.solicitude.actNumber" },
            //{ "data": "teachingAssignment.studentRequest.person.names"},
            //{ "data": "teachingAssignment.studentRequest.person.surnames",},
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
                },
            },
            {
                "data": "id",
                "render": function (data) {
                    if (!active) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/TeachingAssignment/InsertOrUpdateTeachingAssignment/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/TeachingAssignment/DeleteTeachingAssignment/${data}") class="btn btn-danger disabled text-white" disabled style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                                <a href="/Admin/TeachingAssignment/DetailTeachingAssignment/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                     <i class="far fa-eye"></i>
                                </a>
                            </div>
                         `;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a href="/Admin/TeachingAssignment/InsertOrUpdateTeachingAssignment/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                <i class="far fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Admin/TeachingAssignment/DeleteTeachingAssignment/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                            </a>
                            <a href="/Admin/TeachingAssignment/DetailTeachingAssignment/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                <i class="far fa-eye"></i>
                            </a>
                            </div >
                         </div >
                         `;
                    }
                },
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Esta Seguro que quiere Borrar la asignación docente?.",
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