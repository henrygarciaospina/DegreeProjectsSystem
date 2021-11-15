﻿var dataTable;
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
            "url": "/Admin/DepartmentFaculty/GetAllDepartmentFaculties"
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "faculty.name", "width": "30%" },
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
                                <a href="/Admin/DepartmentFaculty/InsertOrUpdateDepartmentFaculty/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/DepartmentFaculty/DeleteDepartmentFaculty/${data}") class="btn btn-danger disabled text-white" disabled style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                            </div>
                         `;
                    } else {
                        return `
                            <div class="text-center">
                                <a href="/Admin/DepartmentFaculty/InsertOrUpdateDepartmentFaculty/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/DepartmentFaculty/DeleteDepartmentFaculty/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                            </div>
                         `;
                    }
                    }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {

    swal({
        title: "Esta Seguro que quiere Eliminar el Departamento de Facultad?",
        text: "Este registro se puede  recuperar actualizando su estado a Activo",
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
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                        dataTable.ajax.reload();
                    }
                }
            });
        }
    });
}