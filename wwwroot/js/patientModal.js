$(".btn").click(function () {
    const currentTds = window.$(this).closest("tr").find("td"); // find all td of selected row
    const cell1 = window.$(currentTds).eq(1).text(); // eq= cell , text = inner text
    const cell2 = window.$(currentTds).eq(2).text();
    const cell4 = window.$(currentTds).eq(4).text();
    const cell5 = window.$(currentTds).eq(5).text();
    window.$("#pId").val(cell1);
    window.$("#name").val(cell2);
    window.$("#age").val(cell4);
    window.$("#phone").val(cell5);
    window.$("#patientModal").modal("show");
});

function onCloseModal() {
    window.$("#patientModal")[0].reset;
    window.$("#pId").val("");
    window.$("#name").val("");
    window.$("#age").val("");
    window.$("#phone").val("");
    window.$("#id").val("");
    window.$("#patientModal").modal("hide");
}

function onBtnSend() {
    var patientData = {
        PatientId: window.$("#pId").val()
    };

    try {
        window.$.ajax({
            type: "POST",
            url: "/Receptionist/SendResult",
            data: JSON.stringify(patientData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    window.toastNotifySuccess("Sent");
                    onCloseModal();
                }
                else {
                    window.toastNotifyError("Something went wrong");
                }
            },
            failure: function (response) {
                window.toastNotifyError(response.responseText);
            },
            error: function (response) {
                window.toastNotifyError(response.responseText);
            }
        });
    } catch (e) {
        window.toastNotifyError(e);
    }
}

