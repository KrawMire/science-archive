using System.Reflection;
using ScienceArchive.Shared.Abstractions.Templates;

namespace ScienceArchive.Infrastructure.Services;

internal class TemplateService : ITemplateService
{
    private readonly string _emailTemplateRelativePath = Path.Combine("Resources", "Templates", "otp-code.template.html"); 
    
    public async Task<string> GetPopulatedOtpEmailTemplate(string userName, string confirmCode)
    {
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var assemblyFolder = Path.GetDirectoryName(assemblyPath)!;
        var htmlTemplateFilePath = Path.Combine(assemblyFolder, _emailTemplateRelativePath);
        
        if (!File.Exists(htmlTemplateFilePath))
        {
            throw new FileNotFoundException("Cannot find template file for email");
        }
        
        var emailTemplate = await File.ReadAllTextAsync(htmlTemplateFilePath);
        return PopulateOtpEmailTemplate(emailTemplate, userName, confirmCode);
    }
    
    private string PopulateOtpEmailTemplate(string template, string userName, string confirmationCode)
    {
        return template
            .Replace("{UserName}", userName)
            .Replace("{ConfirmCode}", confirmationCode);
    }
}