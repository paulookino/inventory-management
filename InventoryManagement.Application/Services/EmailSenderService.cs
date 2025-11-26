using InventoryManagement.Application.Services.Interfaces;

namespace InventoryManagement.Application.Services
{
	public class EmailSenderService : IEmailSender
	{
		public Task SendEmailAsync(string to, string subject, string body)
		{
			Console.WriteLine($"[EMAIL] Sending to: {to}, Subject: {subject}, Body: {body}");
			return Task.CompletedTask;
		}
	}
}
