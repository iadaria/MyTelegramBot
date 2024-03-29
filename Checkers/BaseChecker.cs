using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyTelegramBot.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace MyTelegramBot.Checkers
{
    public class BaseChecker
    {
        protected readonly ILogger _logger;
        protected readonly IMyLogger<BaseChecker> _filelogger;
        protected readonly ITelegramRequest _telegramRequest;
        protected BaseChecker(
            ILogger logger,
            IMyLogger<BaseChecker> filelogger,
            ITelegramRequest TelegramRequest
        )
        {
            _logger = logger;//loggerFactory.CreateLogger<BaseChecker>();
            _filelogger = filelogger;
            _telegramRequest = TelegramRequest;
           /*  _logger = provider.GetService<ILoggerFactory>()
                .CreateLogger<BaseChecker>();
            _filelogger = provider.GetService<IMyLogger>();
            _telegramRequest = provider.GetService<ITelegramRequest>(); */
        }
        protected async Task LogInformation(string message) 
        {
            _logger?.LogInformation(message);
            if (_filelogger != null)
            {
                await _filelogger.WriteInformationAsync(message);
            }
        }
    }

    /* public interface ICheckerInjection {
        ILogger<BaseChecker> Logger();
        IMyLogger Filelogger();
        ITelegramRequest TelegramRequest();
    } */
}