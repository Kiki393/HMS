// <copyright file="AppointmentApiController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HMS.Models.ViewModels;
using HMS.Services;
using HMS.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers.APIs
{
    /// <inheritdoc />
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService appointmentService;
        private readonly string loggedInUserId;
        private readonly string role;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentApiController"/> class.
        /// </summary>
        /// <param name="appointmentService">
        /// The appointment service.
        /// </param>
        /// <param name="httpContextAccessor">
        /// The http context accessor.
        /// </param>
        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            this.appointmentService = appointmentService;
            loggedInUserId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        }

        /// <summary>
        /// Save calendar data.
        /// </summary>
        /// <param name="data">
        /// The appointment view model.
        /// </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVm data)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = appointmentService.AddUpdate(data).Result;
                commonResponse.Message = commonResponse.Status switch
                {
                    1 => NotificationMessages.AppointmentUpdated,
                    2 => NotificationMessages.AppointmentAdded,
                    _ => commonResponse.Message
                };
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.FailureCode;
            }

            return Ok(commonResponse);
        }

        /// <summary>
        /// Get calendar data by doctor id.
        /// </summary>
        /// <param name="doctorId">
        /// The doctor id.
        /// </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            var commonResponse = new CommonResponse<List<AppointmentVm>>();
            try
            {
                switch (role)
                {
                    case UserRoles.Patient:
                        commonResponse.Dataenum = appointmentService.PatientsEventsById(loggedInUserId);
                        commonResponse.Status = NotificationMessages.SuccessCode;
                        break;

                    case UserRoles.Doctor:
                        commonResponse.Dataenum = appointmentService.DoctorsEventsById(loggedInUserId);
                        commonResponse.Status = NotificationMessages.SuccessCode;
                        break;

                    default:
                        commonResponse.Dataenum = appointmentService.DoctorsEventsById(doctorId);
                        commonResponse.Status = NotificationMessages.SuccessCode;
                        break;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.FailureCode;
            }

            return Ok(commonResponse);
        }

        /// <summary>
        /// Get calendar data by id.
        /// </summary>
        /// <param name="id">
        /// The appointment id.
        /// </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            var commonResponse = new CommonResponse<AppointmentVm>();
            try
            {
                commonResponse.Dataenum = appointmentService.GetById(id);
                commonResponse.Status = NotificationMessages.SuccessCode;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.FailureCode;
            }

            return Ok(commonResponse);
        }

        /// <summary>
        /// Delete appointment.
        /// </summary>
        /// <param name="id">
        /// The appointment id.
        /// </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = await appointmentService.Delete(id);
                commonResponse.Message = commonResponse.Status == 1
                    ? NotificationMessages.AppointmentDeleted
                    : NotificationMessages.SomethingWentWrong;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.FailureCode;
            }

            return Ok(commonResponse);
        }

        /// <summary>
        /// Confirm appointment.
        /// </summary>
        /// <param name="id">
        /// The appointment id.
        /// </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("ConfirmEvent/{id}")]
        public IActionResult ConfirmEvent(int id)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                var result = appointmentService.ConfirmEvent(id).Result;
                if (result > 0)
                {
                    commonResponse.Status = NotificationMessages.SuccessCode;
                    commonResponse.Message = NotificationMessages.AppointmentConfirm;
                }
                else
                {
                    commonResponse.Status = NotificationMessages.FailureCode;
                    commonResponse.Message = NotificationMessages.AppointmentConfirmError;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.FailureCode;
            }

            return Ok(commonResponse);
        }
    }
}