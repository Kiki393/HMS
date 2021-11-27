$(".btn").click(function () {
    debugger;
    var currentTds = $(this).closest("tr").find("td"); // find all td of selected row
    var cell1 = $(currentTds).eq(0).text(); // eq= cell , text = inner text
    var cell2 = $(currentTds).eq(1).text();
    var cell4 = $(currentTds).eq(3).text();
    var cell5 = $(currentTds).eq(4).text();
    window.$("#pId").val(cell1);
    window.$("#name").val(cell2);
    window.$("#age").val(cell4);
    window.$("#phone").val(cell5);
    window.$("#patientModal").modal('show');
});

function onCloseModal() {
    $('#patientModal')[0].reset
    window.$("#pId").val('');
    window.$("#name").val('');
    window.$("#age").val('');
    window.$("#phone").val('');
    window.$('#patientModal').modal('hide');
}