using System;
using ScienceArchive.Core.Entities;
using ScienceArchive.Core.Interfaces.Repositories;

namespace ScienceArchive.Application.UseCases
{
	public class CreateUserUseCase
	{
		private IUserRepository _userRepository;

		public CreateUserUseCase(
			IUserRepository userRepository
		)
		{
			_userRepository = userRepository;
		}

		public async Task<User> Execute(User user)
		{
			return await _userRepository.Create(user);
		}
	}
}

