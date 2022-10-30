var dataTable;

$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $("#tblData").DataTable({
    ajax: {
      url: "Product/GetAll",
    },
    columns: [
      { data: "name", width: "30%" },
      { data: "price", width: "30%" },
      { data: "category.name", width: "30%" },
      {
        data: "id",
        render: function (data) {
          return `
          <div class="w-75 btn-group" role="group">
          <a  href="/Product/Upsert?id=${data}"
          class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
          <a class="btn btn-danger mx-2"><i class="bi bi-trash"></i>Delete</a>
          </div>
          `;
        },
        width: "30%",
      },
    ],
  });
}
