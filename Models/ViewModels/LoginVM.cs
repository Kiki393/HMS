// <copyright file="LoginVM.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [DisplayName("Remember Me?")]
        public bool RememberMe { get; init; }
    }
}