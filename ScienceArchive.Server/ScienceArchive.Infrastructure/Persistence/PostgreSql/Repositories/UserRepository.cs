using Microsoft.EntityFrameworkCore;
using ScienceArchive.Core.Domain.Aggregates.User.Factories;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;
using User = ScienceArchive.Core.Domain.Aggregates.User.User;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresUserRepository : IUserRepository
{
    private readonly PostgresDbContext _dbContext;

    public PostgresUserRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<User?> GetById(UserId id)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id.Value)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }
            
        var builder = new UserBuilder(user.Id);
                
        return builder
            .AddEmail(user.Email)
            .AddLogin(user.Login)
            .AddName(user.Name)
            .AddAboutText(user.About)
            .Build();
    }

    /// <inheritdoc/>
    public async Task<User?> GetUserByLoginOrEmail(string login)
    {
        var user = await _dbContext.Users
            .Where(u => u.Email == login || u.Login == login)
            .Include(u => u.UsersAuth)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }
            
        var builder = new UserBuilder(user.Id);
                
        return builder
            .AddEmail(user.Email)
            .AddLogin(user.Login)
            .AddName(user.Name)
            .AddAboutText(user.About)
            .AddPassword(user.UsersAuth!.Password)
            .AddPasswordSalt(user.UsersAuth!.PasswordSalt)
            .Build();
    }

    /// <inheritdoc/>
    public async Task<User?> GetUserByLogin(string login)
    {
        var user = await _dbContext.Users
            .Where(u => u.Login == login)
            .Include(u => u.UsersAuth)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }
            
        var builder = new UserBuilder(user.Id);
                
        return builder
            .AddEmail(user.Email)
            .AddLogin(user.Login)
            .AddName(user.Name)
            .AddAboutText(user.About)
            .Build();
    }

    /// <inheritdoc/>
    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users
            .Where(u => u.Email == email)
            .Include(u => u.UsersAuth)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }
            
        var builder = new UserBuilder(user.Id);
                
        return builder
            .AddEmail(user.Email)
            .AddLogin(user.Login)
            .AddName(user.Name)
            .AddAboutText(user.About)
            .Build();
    }
    
    /// <inheritdoc/>
    public async Task<List<User>> GetAll()
    {
        var users = await _dbContext
            .Users
            .Include(u => u.UsersArticles)
            .ThenInclude(a => a.Article)
            .ToListAsync();

        return users.Select(u =>
            {
                var builder = new UserBuilder(u.Id);
                
                foreach (var userArticle in u.UsersArticles)
                {
                    builder.AddArticle(userArticle.ArticleId, userArticle.Article.Title);
                }
                
                return builder
                    .AddEmail(u.Email)
                    .AddLogin(u.Login)
                    .AddName(u.Name)
                    .AddAboutText(u.About)
                    .Build();
            })
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<User> Create(User newUser)
    {
        var user = await _dbContext
            .Users
            .AddAsync(new Entities.User
            {
                Id = newUser.Id.Value,
                Name = newUser.Name,
                Email = newUser.Email,
                Login = newUser.Login,
                About = newUser.About
            });
        
        await _dbContext
            .UsersAuths
            .AddAsync(new UsersAuth
            {
                Password = newUser.Password!.Value!,
                PasswordSalt = newUser.Password!.Salt!,
            });

        await _dbContext.SaveChangesAsync();

        var createdUser = await GetById(UserId.CreateFromGuid(user.Entity.Id));

        if (createdUser is null)
        {
            throw new PersistenceException("User was not created");
        }

        return createdUser;
    }

    /// <inheritdoc/>
    public async Task<User> Update(UserId id, User newUser)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id.Value)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }
        
        user.Name = newUser.Name;
        user.Email = newUser.Email;
        user.Login = newUser.Login;
        user.About = newUser.About;

        var userCredentials = await _dbContext
            .UsersAuths
            .Where(au => au.UserId == id.Value)
            .FirstOrDefaultAsync() ?? new UsersAuth
        {
            UserId = id.Value
        };

        userCredentials.Password = newUser.Password!.Value!;
        userCredentials.PasswordSalt = newUser.Password!.Salt!;

        await _dbContext.SaveChangesAsync();

        return (await GetById(id))!;
    }

    /// <inheritdoc/>
    public async Task<UserId> Delete(UserId id)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id.Value)
            .FirstOrDefaultAsync();
        
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        
        return id;
    }
}