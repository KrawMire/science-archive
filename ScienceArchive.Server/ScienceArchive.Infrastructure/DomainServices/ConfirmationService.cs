using System.Security.Cryptography;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;

namespace ScienceArchive.Infrastructure.DomainServices;

internal class ConfirmationService : IConfirmationService
{
    private readonly Dictionary<string, string> _confirmCodesStorage = new();
    
    public Task<string> GenerateConfirmationCode(UserId userId)
    {
        var generator = RandomNumberGenerator.Create();
        var randomNumber = new byte[4];
        
        generator.GetBytes(randomNumber);

        var value = BitConverter.ToUInt32(randomNumber, 0);
        value %= 1000000;

        var code = value.ToString("D6");

        var codeKey = userId.ToString();
        
        if (_confirmCodesStorage.ContainsKey(codeKey))
        {
            _confirmCodesStorage[codeKey] = code;
        }
        else
        {
            _confirmCodesStorage.Add(userId.ToString(), code);
        }
        
        return Task.FromResult(code);
    }

    public Task<bool> ConfirmUserCode(UserId userId, string code)
    {
        var codeKey = userId.ToString();
        
        if (!_confirmCodesStorage.TryGetValue(codeKey, out var confirmCode))
        {
            return Task.FromResult(false); 
        }

        var success = confirmCode == code;

        if (success)
        {
            _confirmCodesStorage.Remove(codeKey);
        }

        return Task.FromResult(success);
    }
}