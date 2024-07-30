using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Order_Management.Core.Entities;
using Microsoft.Extensions.Logging;
using Castle.Core.Logging;

namespace Order_Management.Service.implementation
{
	public interface IEmailService
	{
		Task SendEmailAsync(string to, string subject, string message);
	}
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _emailSettings;
		private readonly ILogger<EmailService> _logger;
		public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
		{
			_emailSettings = emailSettings.Value;
			_logger = logger;
		}
		public async Task SendEmailAsync(string to, string subject, string message)
		{
			try
			{
				var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
				{
					Port = _emailSettings.SmtpPort,
					Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
					EnableSsl = true,
					UseDefaultCredentials = false
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
					Subject = subject,
					Body = message,
					IsBodyHtml = true,
				};
				mailMessage.To.Add(to);

				_logger.LogInformation("Sending email to {To}", to);
				await smtpClient.SendMailAsync(mailMessage);
				_logger.LogInformation("Email sent successfully to {To}", to);
			}
			catch (SmtpException smtpEx)
			{
				_logger.LogError(smtpEx, "SMTP error occurred while sending email.");
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to send email.");
				throw;
			}
		}
	}
}
