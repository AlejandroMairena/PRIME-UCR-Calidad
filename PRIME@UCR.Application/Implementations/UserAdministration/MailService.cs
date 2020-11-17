﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class MailService : IMailService
    {
        private readonly MailSettingsModel mailSettings;

        public MailService(IOptions<MailSettingsModel> _mailSettings)
        {
            mailSettings = _mailSettings.Value;
        }

        public async Task SendEmailAsync(EmailContentModel emailContent)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(emailContent.Destination));
            email.Subject = emailContent.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailContent.Body };
            var smtpClient = new SmtpClient();
            smtpClient.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            smtpClient.Authenticate(mailSettings.Mail, mailSettings.Password);
            await smtpClient.SendAsync(email);
            smtpClient.Disconnect(true);
        }
    }
}