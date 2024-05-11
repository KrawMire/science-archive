using ScienceArchive.Application.Abstractions.Encryption;
using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Infrastructure.DomainServices;

internal class AuthService : IAuthService
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IEncryptionService _encryptionService;
    public AuthService(IEncryptionService encryptionService, IDbUnitOfWork dbUnitOfWork)
    {
        _encryptionService = encryptionService;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<User> RegisterUser(User user, string password)
    {
        var dupEmailUser = await _dbUnitOfWork.UserRepository.GetUserByEmail(user.Email);

        if (dupEmailUser is not null)
        {
            throw new DuplicateEmailException();
        }

        var dupLoginUser = await _dbUnitOfWork.UserRepository.GetUserByLogin(user.Login);
        
        if (dupLoginUser is not null)
        {
            throw new DuplicateLoginException();
        }
        
        user.Password.Salt = _encryptionService.CreateSalt();
        user.Password.Value = _encryptionService.HashPassword(password, user.Password.Salt);
        
        return await _dbUnitOfWork.UserRepository.Create(user);
    }

    public async Task<User> AuthorizeUser(string login, string password)
    {
        var user = await _dbUnitOfWork.UserRepository.GetUserByLoginOrEmail(login);

        if (user is null)
        {
            throw new WrongCredentialsException();
        }

        var hash = _encryptionService.HashPassword(password, user.Password.Salt);

        if (hash != user.Password.Value)
        {
            throw new WrongCredentialsException();
        }

        return user;
    }

    public Task<bool> UserHasClaims(UserId userId, IEnumerable<RoleClaim> claims)
    {
        throw new NotImplementedException();
    }
}