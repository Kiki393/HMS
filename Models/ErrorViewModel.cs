// <copyright file="ErrorViewModel.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

namespace HMS.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
