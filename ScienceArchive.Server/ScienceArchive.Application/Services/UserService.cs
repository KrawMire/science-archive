using System;
using ScienceArchive.Application.UseCases;
using ScienceArchive.Core.Entities;
using ScienceArchive.Core.Interfaces.Services;

namespace ScienceArchive.Application.Services
{
	public class UserService : IUserService
	{
        private CreateUserUseCase _createUseCase;
        private DeleteUserUseCase _deleteUseCase;
        private UpdateUserUseCase _updateUseCase;

        public UserService(
            CreateUserUseCase createUserUseCase,
            DeleteUserUseCase deleteUserUseCase,
            UpdateUserUseCase updateUserUseCase
        )
        {
            _createUseCase = createUserUseCase;
            _deleteUseCase = deleteUserUseCase;
            _updateUseCase = updateUserUseCase;
        }

        /// <inheritdoc/>
        public async Task<User> Create(User newUser)
        {
            return await _createUseCase.Execute(newUser);
        }

        /// <inheritdoc/>
        public async Task<Guid> Delete(Guid userId)
        {
            return await _deleteUseCase.Execute(userId);
        }

        /// <inheritdoc/>
        public async Task<User> Update(Guid userId, User newUser)
        {
            return await _updateUseCase.Execute(userId, newUser);
        }
    }
}

