using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Services.Interfaces;
using SimpleWebApi.Services.ViewModels;

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET: api/<AccountsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await accountService.GetAllAsync());
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await accountService.GetByIdAsync(id));
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountViewModel accountViewModel)
        {
            var accountCreated = await accountService.PostAsync(accountViewModel);
            return CreatedAtAction(
                nameof(Get),
                new { id = accountCreated.Id },
                accountCreated);
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AccountViewModel accountViewModel)
        {
            await accountService.PutAsync(accountViewModel);
            return NoContent();
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await accountService.DeleteAsync(id);
            return NoContent();
        }

        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError() => Problem();
    }
}
