$(document).ready(function () {
    window.$("#date").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "yyyy/MM/dd hh:mm tt"
    });
});

$(".btn").click(function () {
    const currentTds = window.$(this).closest("tr").find("td"); // find all td of selected row
    const cell0 = window.$(currentTds).eq(0).text(); // eq= cell , text = inner text
    const cell1 = window.$(currentTds).eq(1).text(); // eq= cell , text = inner text
    const cell2 = window.$(currentTds).eq(2).text();
    const cell3 = window.$(currentTds).eq(3).text();
    const cell4 = window.$(currentTds).eq(4).text();
    const cell5 = window.$(currentTds).eq(5).text();

    window.$("#id").val(cell0);
    window.$("#patientId").val(cell1);
    window.$("#temp").val(cell2);
    window.$("#bp").val(cell3);
    window.$("#weight").val(cell4);
    window.$("#date").val(cell5);
    window.$("#vitalsModal").modal("show");
});

function onCloseModal() {
    window.$("#vitalsModal")[0].reset;
    window.$("#id").val("");
    window.$("#patientId").val("");
    window.$("#temp").val("");
    window.$("#weight").val("");
    window.$("#bp").val("");
    window.$("#date").val("");
    window.$("#vitalsModal").modal("hide");
}

function onBtnSave() {
    const vitals = {
        Id: window.$("#id").val(),
        PatientId: window.$("#patientId").val(),
        Temperature: window.$("#temp").val(),
        Weight: window.$("#weight").val(),
        Bp: window.$("#bp").val(),
        Date: window.$("#date").val()
    };

    if (vitals != null) {
        window.$.ajax({
            type: "POST",
            url: "/Nurse/SaveVitals",
            data: JSON.stringify(vitals),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    window.toastNotifySuccess("Saved");
                    onCloseModal();
                    window.location.reload();
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
    }
}