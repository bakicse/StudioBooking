using Shared.DTOs.MainDTOs.Mail;

namespace Services.Contracts.Base;

public interface IMailService
{
    Task SendEmailAsync(MailRequestDto mailRequestDto);
}
