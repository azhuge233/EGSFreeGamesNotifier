using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using System.Text;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class Email: INotifiable {
		private readonly ILogger<Email> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Email";
		private readonly string debugCreateMessage = "Create notification message";
		#endregion

		public Email(ILogger<Email> logger) {
			_logger = logger;
		}

		private MimeMessage CreateMessage(List<NotifyRecord> pushList, string fromAddress, string toAddress) {
			try {
				_logger.LogDebug(debugCreateMessage);

				var message = new MimeMessage();

				message.From.Add(new MailboxAddress("Epic Game Store Free Games", fromAddress));
				message.To.Add(new MailboxAddress("Receiver", toAddress));

				var sb = new StringBuilder();

				message.Subject = sb.AppendFormat(NotifyFormatStrings.emailTitleFormat, pushList.Count).ToString();
				sb.Clear();

				pushList.ForEach(record => sb.AppendFormat(NotifyFormatStrings.emailBodyFormat, record.ToEmailMessage()));

				message.Body = new TextPart("html") {
					Text = sb.Append("<br>")
						.Append(NotifyFormatStrings.projectLinkHTML)
						.ToString()
				};

				_logger.LogDebug($"Done: {debugCreateMessage}");
				return message;
			} catch (Exception) {
				_logger.LogError($"Error: {debugCreateMessage}");
				throw;
			}
		}


		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var message = CreateMessage(records, config.FromEmailAddress, config.ToEmailAddress);

				using var client = new SmtpClient();
				client.Connect(config.SMTPServer, config.SMTPPort, true);
				client.Authenticate(config.AuthAccount, config.AuthPassword);
				await client.SendAsync(message);
				client.Disconnect(true);

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
