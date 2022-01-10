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
            { "data": "solicitude.titleDegreeWork", "width": "20%"},
            { "data": "solicitude.actNumber", "width": "10%"},
            { "data": "personTypePerson.person.names", "width": "15%" },
            { "data": "teachingFunction.name", "width": "10%"},
            { "data": "assigmentDate", "width": "10%"},
            { "data": "observations", "width": "15%"},
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
                }, "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    if (!active) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/TeachingAssignment/InsertOrUpdateTeachingAssignment/${data}" class="btn btn-block btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/TeachingAssignment/DeleteTeachingAssignment/${data}") class="btn btn-block btn-danger disabled text-white" disabled style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                                <a href="/Admin/TeachingAssignment/DetailTeachingAssignment/${data}" class="btn btn-block btn-primary text-white" style="cursor:pointer">
                                     <i class="far fa-eye"></i>
                                </a>
                            </div>
                         `;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a href="/Admin/TeachingAssignment/InsertOrUpdateTeachingAssignment/${data}" class="btn btn-block btn-warning text-white" style="cursor:pointer;">
                                <i class="far fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Admin/TeachingAssignment/DeleteTeachingAssignment/${data}") class="btn btn-block btn-danger text-white" style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                            </a>
                            <a href="/Admin/TeachingAssignment/DetailTeachingAssignment/${data}" class="btn btn-block btn-primary text-white" style="cursor:pointer">
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