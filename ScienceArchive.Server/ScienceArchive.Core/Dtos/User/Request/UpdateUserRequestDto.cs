﻿using System;
namespace ScienceArchive.Core.Dtos.UserRequest
{
	public record class UpdateUserRequestDto
	{
		/// <summary>
		/// ID of the user to update
		/// </summary>
		public required Guid Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// User auth login
        /// </summary>
        public required string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public required string Password { get; set; }
    }
}

