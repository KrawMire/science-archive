using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;

namespace ScienceArchive.Infrastructure.DomainServices;

internal class AuthService : IAuthService 
{
    
    
    public Task<User> RegisterUser(User user, string password)
    {
        throw new NotImplementedException();
        // var userToCreate = contract.User;
        //
        // userToCreate.Password.Salt = StringGenerator.CreateSalt();
        // userToCreate.Password.Value = StringGenerator.HashPassword(userToCreate.Password.Value, userToCreate.Password.Salt);
        //
        // var users = await _userRepository.GetAll();
        //
        // _ = users.Any(user => user.Email == userToCreate.Email)
        //     ? throw new Exception("This email is already in use")
        //     : false;
        //
        // _ = users.Any(user => user.Login == userToCreate.Login)
        //     ? throw new Exception("This login is already in use")
        //     : false;
        //
        // return await _userRepository.Create(userToCreate);
    }

    public Task<User?> AuthorizeUser(string login, string password)
    {
        throw new NotImplementedException();
        // var user = await _userRepository.GetAuthUserByLogin(contract.Login);
        //
        // if (user is null)
        // {
        //     throw new Exception("User with specified credentials was not found!");
        // }
        //
        // var password = StringGenerator.HashPassword(contract.Password, user.Password.Salt);
        //
        // if (user.Password.Value != password)
        // {
        //     throw new Exception("User with specified credentials was not found!");
        // }
        //
        // _ = Task.Run(() => _notificationGateway.SendNotification(new Notification(Guid.NewGuid())
        // {
        //     Message = "You have successfully logged in!",
        //     Receiver = user.Email
        // }));
        //
        // return user;
    }

    public Task<bool> UserHasClaims(UserId userId, IEnumerable<RoleClaim> claims)
    {
        throw new NotImplementedException();
    }
}