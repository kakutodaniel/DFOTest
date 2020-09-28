using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DFO.Test.Application.Contracts.User;
using DFO.Test.Application.Services.Interfaces;

namespace DFO.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(
            IUserService service,
            IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }

        [HttpGet("{hashId}")]
        public async Task<IActionResult> Get(string hashId)
        {
            var response = await _service.GetByHashId(hashId);
            if (response.Items == null || !response.Items.Any())
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequest user)
        {
            var response = await _service.Add(user);

            if (!response.Success)
            {
                return StatusCode((int)response.HttpStatusCode, response);
            }

            var hashId = response.Items.Select(x => x.HashId).First();
            var host = _httpContextAccessor.HttpContext.Request.Host.Host;
            var port = _httpContextAccessor.HttpContext.Request.Host.Port;
            var location = $"http://{host}:{port}/api/user/{hashId}";

            return Created(location, response);
        }

        [HttpPut("{hashId}")]
        public async Task<IActionResult> Put(string hashId, [FromBody] UserRequest userRequest)
        {
            var response = await _service.UpdateByHashId(hashId, userRequest);

            if (!response.Success)
            {
                return StatusCode((int)response.HttpStatusCode, response);
            }

            if (response.Items == null || !response.Items.Any())
            {
                return NotFound(response);
            }

            return Ok(response);
        }


    }
}
