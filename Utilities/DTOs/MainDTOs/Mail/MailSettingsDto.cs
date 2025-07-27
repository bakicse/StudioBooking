﻿namespace Shared.DTOs.MainDTOs.Mail;
public class MailSettingsDto
{
    public string EmailAddress { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
