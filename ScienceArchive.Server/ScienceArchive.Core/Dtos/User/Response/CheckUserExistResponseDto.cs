using System;
namespace ScienceArchive.Core.Dtos.UserResponse
{
    public record class CheckUserExistResponseDto
    {
        /// <summary>
        /// The result of the checking
        /// </summary>
        public bool UserExist { get; set; }
    }
}

