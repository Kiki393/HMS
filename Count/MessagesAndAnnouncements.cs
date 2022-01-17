// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessagesAndAnnouncements.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The messages and announcements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Count
{
    using System.Linq;

    using HMS.Areas.Identity.Data;

    /// <summary>
    /// The messages and announcements.
    /// </summary>
    public class MessagesAndAnnouncements
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesAndAnnouncements"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        public MessagesAndAnnouncements(ApplicationDbContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// The get announcements.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetAnnouncements()
        {
            return this._db.Announcements.ToList().Count;
        }

        /// <summary>
        /// The get messages.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetMessages()
        {
            return this._db.Messages.ToList().Count;
        }
    }
}