using InventoryManagement.Application.Services.Interfaces;

namespace InventoryManagement.Application.Mocks
{
	public class EmailSenderMock : IEmailSender
	{
		public Task SendEmailAsync(string to, string subject, string body)
		{
			Console.WriteLine($"[EMAIL MOCK] To: {to}, Subject: {subject}, Body: {body}");
			return Task.CompletedTask;
		}
	}
}
