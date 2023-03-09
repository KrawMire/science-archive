using System;
using ScienceArchive.Application.Mappers;
using ScienceArchive.Application.UseCases;
using ScienceArchive.Core.Dtos.UserRequest;
using ScienceArchive.Core.Dtos.UserResponse;
using ScienceArchive.Core.Entities;
using ScienceArchive.Core.Interfaces.Services;

namespace ScienceArchive.Application.Services
{
    public class UserService : IUserService
    {
        private CreateUserUseCase _createUseCase;
        private DeleteUserUseCase _deleteUseCase;
        private readonly UpdateUserUseCase _updateUseCase;

        public UserService(
            CreateUserUseCase createUserUseCase,
            DeleteUserUseCase deleteUserUseCase,
            UpdateUserUseCase updateUserUseCase)
        {
            _createUseCase = createUserUseCase;
            _deleteUseCase = deleteUserUseCase;
            _updateUseCase = updateUserUseCase;
        }

        /// <inheritdoc/>
        public async Task<CreateUserResponseDto> Create(CreateUserRequestDto contract)
        {
            User userToCreate = CreateUserMapper.MapToEntity(contract);
            User createdUser = await _createUseCase.Execute(userToCreate);

            return CreateUserMapper.MapToResponse(createdUser);
        }

        /// <inheritdoc/>
        public async Task<DeleteUserResponseDto> Delete(DeleteUserRequestDto contract)
        {
            Guid deletedUserId = await _deleteUseCase.Execute(contract.Id);

            return DeleteUserMapper.MapToResponse(deletedUserId);
        }

        /// <inheritdoc/>
        public async Task<UpdateUserResponseDto> Update(UpdateUserRequestDto contract)
        {
            User userToUpdate = UpdateUserMapper.MapToEntity(contract);
            User updatedUser = await _updateUseCase.Execute(contract.Id, userToUpdate);

            return UpdateUserMapper.MapToResponse(updatedUser);
        }
    }
}

