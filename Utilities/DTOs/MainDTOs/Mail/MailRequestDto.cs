using Microsoft.AspNetCore.Http;

namespace Shared.DTOs.MainDTOs.Mail;
public class MailRequestDto
{
    public List<string> ToEmailList { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string EmailTemplate { get; set; } = null!;
    public IEnumerable<IFormFile> AttachmentList { get; set; } = [];
}
