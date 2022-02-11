var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {
    window.$("#appointmentDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "yyyy/MM/dd hh:mm tt"
    });

    InitializeCalendar();
});

var calendar;

function InitializeCalendar() {
    try {
        const calendarEl = document.getElementById("calendar");
        if (calendarEl != null) {
            calendar = new window.FullCalendar.Calendar(calendarEl, {
                initialView: "dayGridMonth",
                headerToolbar: {
                    left: "prev,next,today",
                    center: "title",
                    right: "dayGridMonth,timeGridWeek,timeGridDay"
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                },
                eventDisplay: "block",
                events: function (fetchInfo, successCallback, failureCallback) {
                    window.$.ajax({
                        url: routeURL + "/api/Appointment/GetCalendarData?doctorId=" + window.$("#doctorId").val(),
                        type: "GET",
                        dataType: "JSON",
                        success: function (response) {
                            var events = [];
                            if (response.status === 1) {
                                window.$.each(response.dataenum, function (i, data) {
                                    events.push({
                                        title: data.title,
                                        description: data.description,
                                        start: data.startDate,
                                        end: data.endDate,
                                        backgroundColor: data.isDoctorApproved ? "#28a745" : "#dc3545",
                                        borderColor: "#162466",
                                        textColor: "white",
                                        id: data.id
                                    });
                                });
                            }
                            successCallback(events);
                        },
                        error: function (xhr) {
                            window.$.notify("Error", "error");
                        }
                    });
                },
                eventClick: function (info) {
                    getEventDetailsByEventId(info.event);
                }
            });
            calendar.render();
        }
    } catch (e) {
        alert(e);
    }
}

function onShowModal(obj, isEventDetail) {
    if (isEventDetail != null) {
        window.$("#title").val(obj.title);
        window.$("#description").val(obj.description);
        window.$("#appointmentDate").val(obj.startDate);
        window.$("#doctorId").val(obj.doctorId);
        window.$("#patientId").val(obj.patientId);
        window.$("#duration").val(obj.duration);
        window.$("#id").val(obj.id);
        window.$("#comments").val(obj.comments);

        window.$("#lblPatientName").html(obj.patientName);
        window.$("#lblDoctorName").html(obj.doctorName);

        if (obj.isDoctorApproved) {
            window.$("#lblStatus").html("Approved");
            window.$("#btnSubmit").addClass("d-none");
            window.$("#btnConfirm").addClass("d-none");
        } else {
            window.$("#lblStatus").html("Pending");
            window.$("#btnSubmit").removeClass("d-none");
            window.$("#btnConfirm").removeClass("d-none");
        }
        window.$("#btnDelete").removeClass("d-none");
    } else {
        window.$("#appointmentDate").val(obj.startStr + " " + new window.moment().format("hh:mm A"));
        window.$("#id").val(0);
        window.$("#btnDelete").addClass("d-none");
        window.$("#btnSubmit").removeClass("d-none");
    }
    window.$("#appointmentInput").modal("show");
}

function onCloseModalA() {
    window.$("#appointmentForm")[0].reset();
    window.$("#id").val(0);
    window.$("#title").val("");
    window.$("#description").val("");
    window.$("#appointmentDate").val("");
    window.$("#comments").val("");

    window.$("#appointmentInput").modal("hide");
}

function onSubmitForm() {
    if (checkValidation()) {
        const requestData = {
            Id: parseInt(window.$("#id").val()),
            Title: window.$("#title").val(),
            Description: window.$("#description").val(),
            StartDate: window.$("#appointmentDate").val(),
            DoctorId: window.$("#doctorId").val(),
            Duration: window.$("#duration").val(),
            PatientId: window.$("#patientId").val(),
            Comments: window.$("#comments").val()
        };

        window.$.ajax({
            url: routeURL + "/api/Appointment/SaveCalendarData",
            type: "POST",
            data: JSON.stringify(requestData),
            contentType: "application/json",
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    calendar.refetchEvents();
                    window.$.notify(response.message, "success");
                    onCloseModal();
                } else {
                    window.$.notify(response.message, "error");
                }
            },
            error: function (xhr) {
                window.$.notify("Error", "error");
            }
        });
    } else {
        window.$.notify("One or more fields is empty.", "error");
    }
}

function onSaveComment() {
    if (checkValidation()) {
        const requestData = {
            Comments: window.$("#comments").val()
        };

        window.$.ajax({
            url: routeURL + "/api/Appointment/SaveCalendarData",
            type: "POST",
            data: JSON.stringify(requestData),
            contentType: "application/json",
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    calendar.refetchEvents();
                    window.$.notify(response.message, "success");
                    onCloseModal();
                } else {
                    window.$.notify(response.message, "error");
                }
            },
            error: function (xhr) {
                window.$.notify("Error", "error");
            }
        });
    } else {
        window.$.notify("One or more fields is empty.", "error");
    }
}

function checkValidation() {
    var isValid = true;
    if (window.$("#title").val() === undefined || window.$("#title").val() === "") {
        isValid = false;
        window.$("#title").addClass("error");
    } else {
        window.$("#title").removeClass("error");
    }

    if (window.$("#appointmentDate").val() === undefined || window.$("#appointmentDate").val() === "") {
        isValid = false;
        window.$("#appointmentDate").addClass("error");
    } else {
        window.$("#appointmentDate").removeClass("error");
    }

    return isValid;
}

function getEventDetailsByEventId(info) {
    window.$.ajax({
        url: routeURL + "/api/Appointment/GetCalendarDataById/" + info.id,
        type: "GET",
        dataType: "JSON",
        success: function (response) {
            if (response.status === 1 && response.dataenum !== undefined) {
                onShowModal(response.dataenum, true);
            }
            window.successCallback(events);
        },
        error: function (xhr) {
            window.$.notify("Error", "error");
        }
    });
}

function onDoctorChange() {
    calendar.refetchEvents();
}

function onDeleteAppointment() {
    const id = window.parseInt(window.$("#id").val());
    window.$.ajax({
        url: routeURL + "/api/Appointment/DeleteAppointment/" + id,
        type: "GET",
        dataType: "JSON",
        success: function (response) {
            if (response.status === 1) {
                window.$.notify(response.message, "success");
                calendar.refetchEvents();
                onCloseModal();
            } else {
                window.$.notify(response.message, "error");
            }
            window.successCallback(events);
        },
        error: function (xhr) {
            window.$.notify("Error", "error");
        }
    });
}

function onConfirm() {
    const id = window.parseInt(window.$("#id").val());
    window.$.ajax({
        url: routeURL + "/api/Appointment/ConfirmEvent/" + id,
        type: "GET",
        dataType: "JSON",
        success: function (response) {
            if (response.status === 1) {
                window.$.notify(response.message, "success");
                calendar.refetchEvents();
                onCloseModal();
            } else {
                window.$.notify(response.message, "error");
            }
            window.successCallback(events);
        },
        error: function (xhr) {
            window.$.notify("Error", "error");
        }
    });
}