// <copyright file="AppointmentApiController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HMS.Controllers.APIs
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HMS.Models.ViewModels;
    using HMS.Services;
    using HMS.Utilities;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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
            this._appointmentService = appointmentService;
            this._httpContextAccessor = httpContextAccessor;
            this._loggedInUserId = this._httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this._role = this._httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVm data)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = this._appointmentService.AddUpdate(data).Result;
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
            return this.Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            var commonResponse = new CommonResponse<List<AppointmentVm>>();
            try
            {
                switch (this._role)
                {
                    case UserRoles.Doctor:
                        commonResponse.Dataenum = this._appointmentService.DoctorsEventsById(this._loggedInUserId);
                        commonResponse.Status = NotificationMessages.successCode;
                        break;

                    default:
                        commonResponse.Dataenum = this._appointmentService.DoctorsEventsById(doctorId);
                        commonResponse.Status = NotificationMessages.successCode;
                        break;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return this.Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            var commonResponse = new CommonResponse<AppointmentVm>();
            try
            {
                commonResponse.Dataenum = this._appointmentService.GetById(id);
                commonResponse.Status = NotificationMessages.successCode;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return this.Ok(commonResponse);
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
                commonResponse.Status = await this._appointmentService.Delete(id);
                commonResponse.Message = commonResponse.Status == 1
                    ? NotificationMessages.appointmentDeleted
                    : NotificationMessages.somethingWentWrong;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = NotificationMessages.failureCode;
            }

            return this.Ok(commonResponse);
        }

        [HttpGet]
        [Route("ConfirmEvent/{id}")]
        public IActionResult ConfirmEvent(int id)
        {
            var commonResponse = new CommonResponse<int>();
            try
            {
                var result = this._appointmentService.ConfirmEvent(id).Result;
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

            return this.Ok(commonResponse);
        }
    }
}