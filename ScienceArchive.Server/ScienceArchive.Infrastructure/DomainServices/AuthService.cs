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
    private readonly IConfirmationService _confirmationService;
    public AuthService(
        IEncryptionService encryptionService, 
        IDbUnitOfWork dbUnitOfWork, 
        IConfirmationService confirmationService)
    {
        _encryptionService = encryptionService;
        _dbUnitOfWork = dbUnitOfWork;
        _confirmationService = confirmationService;
    }
    
    public async Task<(User User, string Code)> RegisterUser(User user, string password)
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
        
        user.Disconfirm();
        
        user.Password ??= new UserPassword();
        user.Password.Salt = _encryptionService.CreateSalt();
        user.Password.Value = _encryptionService.HashPassword(password, user.Password.Salt);
        
        var createdUser = await _dbUnitOfWork.UserRepository.Create(user);
        var code = await _confirmationService.GenerateConfirmationCode(createdUser.Id);

        return (createdUser, code);
    }

    public async Task<(User User, string? Code)> RegenerateConfirmCode(UserId userId)
    {
        var user = await _dbUnitOfWork.UserRepository.GetById(userId);

        if (user is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }
        
        if (user.IsConfirmed)
        {
            return (user, Code: null);
        }
        
        var code = await _confirmationService.GenerateConfirmationCode(userId);

        return (user, code);
    }

    public async Task<User> ConfirmUser(UserId userId, string confirmCode)
    {
        var success = await _confirmationService.ConfirmUserCode(userId, confirmCode);

        if (!success)
        {
            throw new WrongConfirmationCodeException();
        }

        var user = await _dbUnitOfWork.UserRepository.GetById(userId);

        if (user is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }
        
        user.Confirm();
        var confirmedUser = await _dbUnitOfWork.UserRepository.Update(userId, user);

        return confirmedUser;
    }

    public async Task<(User User, string? Code)> AuthorizeUser(string login, string password)
    {
        var user = await _dbUnitOfWork.UserRepository.GetUserByLoginOrEmail(login);

        if (user is null)
        {
            throw new WrongCredentialsException();
        }
        
        var hash = _encryptionService.HashPassword(password, user.Password!.Salt);

        if (hash != user.Password.Value)
        {
            throw new WrongCredentialsException();
        }

        if (user.IsConfirmed)
        {
            return (user, null);
        }
        
        var code = await _confirmationService.GenerateConfirmationCode(user.Id);
        return (user, code);

    }

    public async Task<bool> UserHasClaims(UserId userId, IEnumerable<RoleClaim> claims)
    {
        var user = await _dbUnitOfWork.UserRepository.GetById(userId);

        if (user is null)
        {
            return false;
        }
        
        var userClaims = await _dbUnitOfWork.RoleRepository.GetUserClaims(userId);
        var success = claims.All(claim => userClaims.Any(uc => uc.Value == claim.Value));

        return success;
    }
}