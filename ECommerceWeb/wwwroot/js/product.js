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
      { data: "name", width: "33%" },
      { data: "price", width: "33%" },
      { data: "category.name", width: "33%" },
    ],
  });
}
