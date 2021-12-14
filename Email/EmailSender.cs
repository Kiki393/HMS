// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the EmailSender type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;

namespace HMS.Email
{
    /// <summary>
    /// The email sender.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly INotyfService _notyf;

        public EmailSender(INotyfService notyf)
        {
            _notyf = notyf;
        }

        /// <summary>
        /// The send email async.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="htmlMessage">
        /// The html message.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new MailjetClient(
                             "c3920a9841a8b1ba68f4011b650577aa",
                             "fd0670bd943aa39e0a54551f084d3a0a");

            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
                .Property(Send.FromEmail, "ventus2@protonmail.com")
                .Property(Send.FromName, "HMS")
                .Property(Send.Subject, subject)
                .Property(Send.HtmlPart, htmlMessage)
                .Property(Send.Recipients, new JArray
                {
                    new JObject
                    {
                        { "Email", email },
                    },
                });

            var response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                _notyf.Success("Email Sent.");
            }
            else
            {
                _notyf.Error("Email failed to send.");
            }
        }
    }
}