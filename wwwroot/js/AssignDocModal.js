$(".btn").click(function () {
});

function onCloseModal() {
    window.$("#vitalsModal")[0].reset();
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