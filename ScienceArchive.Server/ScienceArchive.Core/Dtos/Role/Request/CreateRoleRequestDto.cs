﻿using System;
namespace ScienceArchive.Core.Dtos.Role.Request
{
    /// <summary>
    /// Request contract to create role
    /// </summary>
    /// <param name="Role">Role to create</param>
    public record CreateRoleRequestDto(RoleDto Role);
}

