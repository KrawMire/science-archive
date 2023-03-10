using System;
using ScienceArchive.Core.Entities;
using ScienceArchive.Core.Interfaces.Repositories;
using ScienceArchive.Core.Utils;

namespace ScienceArchive.Application.UseCases
{
    public class CheckUserExistUseCase
    {
        private IUserRepository _userRepository;

        public CheckUserExistUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Execute(string login, string password)
        {
            User? foundUser = null;

            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Login or password are empty!");
            }

            var users = await _userRepository.GetAll();
            foundUser = users.Find(u => u.Login == login || u.Email == login);
            _ = foundUser ?? throw new Exception($"User with login or email {login} does not exist!");

            var passwordHash = StringGenerator.HashPassword(password, foundUser.PasswordSalt);

            if (passwordHash != foundUser.Password)
            {
                throw new Exception("Wrong password was sent!");
            }

            return true;
        }
    }
}

