using Microsoft.Extensions.Logging;

namespace Startup.Webapi.Services
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }

    public class LocalMailService : IMailService
    {
        private readonly ILogger<LocalMailService> _logger;

        public LocalMailService(ILogger<LocalMailService> logger)
        {
            _logger = logger;
        }

        private string _mailTo = "developer@qq.com";
        private string _mailFrom = "noreply@alibaba.com";

        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }

    public class CloudMailService : IMailService
    {
        private readonly ILogger<CloudMailService> _logger;

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }

        private readonly string _mailTo = "admin@qq.com";
        private readonly string _mailFrom = "noreply@alibaba.com";

        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}
