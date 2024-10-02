using System.Security.Cryptography;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Infrastructure.DomainServices;

internal class ConfirmationService : IConfirmationService
{
    private record ConfirmationEntry(string Code, DateTime ExpireDate, int Attempt, int ResentTimes);
    private readonly Dictionary<string, ConfirmationEntry> _confirmCodesStorage = new();
    
    public Task<string> GenerateConfirmationCode(UserId userId)
    {
        RemoveExpiredCodes();
        var generator = RandomNumberGenerator.Create();
        var randomNumber = new byte[4];
        
        generator.GetBytes(randomNumber);

        var value = BitConverter.ToUInt32(randomNumber, 0);
        value %= 1000000;

        var code = value.ToString("D6");

        var codeKey = userId.ToString();
        var expireDate = DateTime.Now.AddMinutes(5);
        
        if (_confirmCodesStorage.ContainsKey(codeKey))
        {
            var currentCode = _confirmCodesStorage[codeKey];

            if (currentCode.ResentTimes > 5)
            {
                throw new SpentConfirmRegenerationTries();
            }
            
            _confirmCodesStorage[codeKey] = new ConfirmationEntry(code, expireDate, 0, currentCode.ResentTimes + 1);
        }
        else
        {
            _confirmCodesStorage.Add(userId.ToString(), new ConfirmationEntry(code, expireDate, 0, 0));
        }
        
        return Task.FromResult(code);
    }

    public Task<bool> ConfirmUserCode(UserId userId, string code)
    {
        RemoveExpiredCodes();
        var codeKey = userId.ToString();
        
        if (!_confirmCodesStorage.TryGetValue(codeKey, out var confirmCode))
        {
            return Task.FromResult(false); 
        }

        if (confirmCode.Attempt >= 5)
        {
            throw new SpentConfirmationAttemptsException();
        }
        
        var success = confirmCode.Code == code;

        if (success)
        {
            _confirmCodesStorage.Remove(codeKey);
        }
        else
        {
            _confirmCodesStorage[codeKey] = confirmCode with { Attempt = confirmCode.Attempt + 1 };
        }

        return Task.FromResult(success);
    }

    private void RemoveExpiredCodes()
    {
        var currentTime = DateTime.Now;
        var expiredCodes = _confirmCodesStorage
            .Where(conf => conf.Value.ExpireDate < currentTime)
            .ToDictionary()
            .Keys;

        foreach (var expiredCode in expiredCodes)
        {
            _confirmCodesStorage.Remove(expiredCode);
        }
    }
}