using System;
using ScienceArchive.Core.Entities;
using ScienceArchive.Core.Interfaces.Repositories;

namespace ScienceArchive.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		public UserRepository() { }

        public async Task<User> GetById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User[]> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Create(User newUser)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(Guid userId, User newUser)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Delete(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}

