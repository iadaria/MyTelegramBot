using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyTelegramBot.Interface;
using MyTelegramBot.Helpers;
using MyTelegramBot.Dtos.Telegram;
using MyTelegramBot.Checkers.Messages;
using MyTelegramBot.Checkers.Callback;
using Newtonsoft.Json;
using AutoMapper;
using MyTelegramBot.Models.Telegram;

namespace MyTelegramBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly IMyLogger<WebhookController> _logger;
        private readonly IMessageChecker _messageChecker;
        private readonly ICallbackChecker _callbackChecker;
        private readonly IMapper _mapper;
        private readonly IDataRepository _dataRepository;
        public WebhookController(
            IMyLogger<WebhookController> logger
            ,IMessageChecker messageChecker
            ,ICallbackChecker callbackChecker
            ,IMapper mapper
            ,IDataRepository repo)
        {
            _mapper = mapper;
            _logger = logger; 
            _messageChecker = messageChecker;
            _callbackChecker = callbackChecker;
            _dataRepository = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateForCreationDto incomingRequestDto)
        {
            //var temp = HttpContext.Request.Cookies;
            //Debug information
            var request = HttpContext.Request.ReadRequestBody();
            await _logger.LogIncomingRequest(request);  
            
            string responseReceived = "";

            if (incomingRequestDto.Message != null) 
            {
                responseReceived = await _messageChecker.Checker(incomingRequestDto.Message);
            }
            
            if (incomingRequestDto.CallbackQuery != null) 
            {    
                responseReceived = await _callbackChecker.Checker(incomingRequestDto.CallbackQuery);
            }

            await SaveRequestData(incomingRequestDto);
            await SaveResponseData(responseReceived);
     
            return StatusCode(201);//Ok(checkResult);
        }
        private async Task SaveRequestData(UpdateForCreationDto incomingRequestDto)
        {
            await Task.Run(async () => {
                if (!await _dataRepository.UpdateExists(incomingRequestDto.Id))
                {
                    var updateForCreation = _mapper.Map<Update>(incomingRequestDto);
                    _dataRepository.Add(updateForCreation);
                    await _dataRepository.SaveAllAsync();
                }
            });
        }
        private async Task SaveResponseData(string response = "")
        {
            await Task.Run(async () => {
                await _logger.LogInformation($"\nRESPONSE FROM TELEGRAM \n{response}");
                if (!response.Contains("error")) 
                {
                    var responseDto = JsonConvert.DeserializeObject<ResponseDto>(response);
                    await _logger.LogResponseFromTelegram(responseDto);
                    if (responseDto != null) 
                    {
                        var responseForCreation = _mapper.Map<Response>(responseDto);
                        await _logger.LogResponseFromTelegram(responseForCreation);
                        _dataRepository.Add(responseForCreation);
                        await _dataRepository.SaveAllAsync();
                    }
                }
            });
        }
    }
}

//_messageChecker = provider.GetService<DataChecker>();
//_messageChecker.SetNext( provider.GetService<SimpleCommandChecker>() );
//_callbackChecker = provider.GetService<CallbackChecker>();