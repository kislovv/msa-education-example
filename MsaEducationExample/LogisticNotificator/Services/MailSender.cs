
using LogisticNotificator.Abstractions;
using LogisticNotificator.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MsaSharedContracts;

namespace LogisticNotificator.Services;

public class MailSender(IOptionsMonitor<MailConfig> optionsMonitor): IMailSender
{
    private readonly MailConfig _mailConfig = optionsMonitor.CurrentValue;
    public async Task SendMail(OrderNotificationDto orderDto)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailConfig.From);
        email.To.Add(MailboxAddress.Parse(orderDto.EmailContract));
        email.Subject = "Статус по заявке";
        var builder = new BodyBuilder
        {
            HtmlBody = $"<p> Уважаемый клиент! Статус вашей заявки {orderDto.Status}</p>"
        };
        email.Body = builder.ToMessageBody();
        
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailConfig.Host, _mailConfig.Port, SecureSocketOptions.None);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}