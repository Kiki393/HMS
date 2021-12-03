// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the EmailSender type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
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
            var request = new MailjetRequest { Resource = Send.Resource, }.Property(
                Send.Messages,
                new JArray
                    {
                        new JObject
                            {
                                {
                                    "From",
                                    new JObject
                                        {
                                            { "Email", "ventus2@protonmail.com" }, { "Name", "Appointment Scheduler" },
                                        }
                                },
                                { "To", new JArray { new JObject { { "Email", email }, } } },
                                { "Subject", subject },
                                { "HTMLPart", htmlMessage },
                            },
                    });
            var response = await client.PostAsync(request);
        }
    }
}