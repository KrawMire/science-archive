﻿using System;
namespace ScienceArchive.Core.Dtos
{
    /// <summary>
    /// User DTO
    /// </summary>
    public record UserDto
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid? Id { get; set; }

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
    }
}
