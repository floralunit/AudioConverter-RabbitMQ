using System;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace SendMessagesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IBus _bus;

        public  AudioController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async void PostAsync(ConvertWavToWmaMessage audio)
        {
            //var sendEndpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/quartz"));
            //var destinationAddress = new Uri("rabbitmq://localhost/web-service-endpoint");
            //var deliveryTime = DateTime.Now.AddSeconds(10);

            //await sendEndpoint.ScheduleSend(destinationAddress, deliveryTime, book);
            await _bus.Publish(audio);
        }
    }
}