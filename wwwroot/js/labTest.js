function confirmForward() {
    window.$("#forwardLabModal").modal("show");
}

function onForwardLab() {
    var patientData = {
        PatientId: window.$("#patientId").val()
    };

    try {
        window.$.ajax({
            type: "POST",
            url: "/Doctor/ForwardToLab",
            data: JSON.stringify(patientData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    window.toastNotifySuccess("Sent");
                    window.$("#forwardLabModal").modal("hide");
                }
                else {
                    window.toastNotifyError("Something went wrong");
                    window.$("#forwardLabModal").modal("hide");
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