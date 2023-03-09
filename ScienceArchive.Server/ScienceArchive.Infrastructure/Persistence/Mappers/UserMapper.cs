﻿using System;
using ScienceArchive.Core.Entities;
using ScienceArchive.Infrastructure.Persistence.Models;

namespace ScienceArchive.Infrastructure.Persistence.Mappers
{
    public static class UserMapper
    {
        public static UserModel MapToPersistence(User user)
        {
            var userModel = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt ?? "",
            };

            return userModel;
        }

        public static User MapToDomain(UserModel userModel)
        {
            var user = new User(userModel.Id, userModel.PasswordSalt)
            {
                Email = userModel.Email,
                Login = userModel.Login,
                Name = userModel.Name,
                Password = userModel.Password,
            };

            return user;
        }
    }
}

