using Microsoft.AspNetCore.Mvc;
using static TechChallenge.Application.Interfaces.IKafkaClient;

namespace TechChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IKafkaProducer _kafkaProducer;

        public KafkaController(IKafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishMessage([FromBody] TestMessageDto messageDto)
        {
            if (string.IsNullOrWhiteSpace(messageDto.Message))
                return BadRequest("Message cannot be empty.");

            await _kafkaProducer.SendMessageAsync(messageDto);

            return Ok(new { status = "Message published", message = messageDto.Message });
        }
    }

    public class TestMessageDto
    {
        public string Message { get; set; }
    }
}
