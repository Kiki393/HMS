function getPrescriptionModal() {
    window.$("#prescriptionModal").modal("show");
}

function onCloseModalPres() {
    window.$("#prescriptionForm")[0].reset();
    window.$("#prescription").val("");
    window.$("#prescriptionModal").modal("hide");
}

function onBtnSaveP() {
    const medication = {
        Prescription: window.$("#prescription").val(),
        PatientId: window.$("#id").val()
    };

    try {
        window.$.ajax({
            type: "POST",
            url: "/Doctor/SavePrescription",
            data: JSON.stringify(medication),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    onCloseModalPres();
                    window.location.reload();
                    window.toastNotifySuccess("Saved");
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