using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Contracts.Base;
using Shared.DTOs.MainDTOs.Mail;
using Shared.Enums;

namespace Services.Concretes.Base;
public sealed class MailService(IOptions<MailSettingsDto> mailSettingsDto) : IMailService
{
    private readonly MailSettingsDto _mailSettingsDto = mailSettingsDto.Value;

    public async Task SendEmailAsync(MailRequestDto mailRequestDto)
    {
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettingsDto.EmailAddress)
        };

        var toAddresses = new InternetAddressList();
        foreach (var recipient in mailRequestDto.ToEmailList)
        {
            toAddresses.Add(MailboxAddress.Parse(recipient));
        }
        email.To.AddRange(toAddresses);

        email.Subject = mailRequestDto.Subject;

        var builder = new BodyBuilder
        {
            HtmlBody = $"<p>{await GetEmailTemplateAsync(mailRequestDto.EmailTemplate)}</p>"
        };

        foreach (var attachment in mailRequestDto.AttachmentList)
        {
            if (attachment.Length > 0)
            {
                using var ms = new MemoryStream();
                await attachment.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
            }
        }

        email.Body = builder.ToMessageBody();

        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_mailSettingsDto.Host, _mailSettingsDto.Port, SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_mailSettingsDto.Username, _mailSettingsDto.Password);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }


    private static async Task<string> GetEmailTemplateAsync(string templateName)
    {
        var assembly = typeof(EmailTemplates).Assembly;
        var resourceName = $"Workflow.Shared.EmailTemplates.{templateName}.html";

        await using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream!);
        return await reader.ReadToEndAsync();
    }
}
