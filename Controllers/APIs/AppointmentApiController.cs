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
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _loggedInUserId;
        private readonly string _role;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentApiController"/> class.
        /// </summary>
        /// <param name="appointmentService"></param>
        /// <param name="httpContextAccessor"></param>
        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            _loggedInUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVm data)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _appointmentService.AddUpdate(data).Result;
                commonResponse.Message = commonResponse.Status switch
                {
                    1 => NotificationMessages.appointmentUpdated,
                    2 => NotificationMessages.appointmentAdded,
                    _ => commonResponse.Message
                };
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            var commonResponse = new CommonResponse<List<AppointmentVm>>();
            try
            {
                switch (_role)
                {
                    case UserRoles.Patient:
                        commonResponse.Dataenum = _appointmentService.PatientsEventsById(_loggedInUserId);
                        commonResponse.Status = NotificationMessages.successCode;
                        break;

                    case UserRoles.Doctor:
                        commonResponse.Dataenum = _appointmentService.DoctorsEventsById(_loggedInUserId);
                        commonResponse.Status = NotificationMessages.successCode;
                        break;

                    default:
                        commonResponse.Dataenum = _appointmentService.DoctorsEventsById(doctorId);
                        commonResponse.Status = NotificationMessages.successCode;
                        break;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            var commonResponse = new CommonResponse<AppointmentVm>();
            try
            {
                commonResponse.Dataenum = _appointmentService.GetById(id);
                commonResponse.Status = NotificationMessages.successCode;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return Ok(commonResponse);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = await _appointmentService.Delete(id);
                commonResponse.Message = commonResponse.Status == 1
                    ? NotificationMessages.appointmentDeleted
                    : NotificationMessages.somethingWentWrong;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ConfirmEvent/{id}")]
        public IActionResult ConfirmEvent(int id)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                var result = _appointmentService.ConfirmEvent(id).Result;
                if (result > 0)
                {
                    commonResponse.Status = NotificationMessages.successCode;
                    commonResponse.Message = NotificationMessages.appointmentConfirm;
                }
                else
                {
                    commonResponse.Status = NotificationMessages.failureCode;
                    commonResponse.Message = NotificationMessages.appointmentConfirmError;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return Ok(commonResponse);
        }
    }
}