$(document).ready(function () {
    window.$("#date").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "yyyy/MM/dd hh:mm tt"
    });
});

var date = new Date();

function getData() {
    window.$(".select").click(function () {
        const currentTds = window.$(this).closest("tr").find("td"); // find all td of selected row
        const cell0 = window.$(currentTds).eq(0).text(); // eq= cell , text = inner text
        const cell1 = window.$(currentTds).eq(1).text(); // eq= cell , text = inner text
        const cell2 = window.$(currentTds).eq(2).text();

        window.$("#id").val(cell0);
        window.$("#patientId").val(cell1);
        window.$("#name").val(cell2);
        window.$("#vitalsModal").modal("show");
    });
}

function onCloseModalV() {
    window.$("#patientVitals")[0].reset();
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
        PatientId: window.$("#patientId").val(),
        Name: window.$("#name").val(),
        Temperature: window.$("#temp").val(),
        Weight: window.$("#weight").val(),
        Bp: window.$("#bp").val(),
        Date: date.toLocaleString()
    };

    try {
        window.$.ajax({
            type: "POST",
            url: "/Nurse/SaveVitals",
            data: JSON.stringify(vitals),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    window.toastNotifySuccess("Saved");
                    sendVitalsModal();
                    /*window.location.reload();*/
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
    return window.v = vitals;
}

function sendVitalsModal() {
    window.$("#vitalsModal").modal("hide");
    window.$("#confirmSendVitals").modal("show");
}

function assignDocModal() {
    window.$("#confirmSendVitals").modal("hide");
    window.$("#assignDocModal").modal("show");
}

function assignDoc() {
    const select = document.getElementById("doclist");
    const option = select.options[select.selectedIndex].value;
    const data = {
        DocId: option,
        PId: v.PatientId
    };

    try {
        window.$.ajax({
            type: "POST",
            url: "/Nurse/AssignDoc",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    window.location.reload();
                    window.toastNotifySuccess("Assigned");
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