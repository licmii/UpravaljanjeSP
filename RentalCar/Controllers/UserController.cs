using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.User.Commands;

namespace RentalCar.Controllers;

[Route("user")]
public class UserController : ApiControllerBase
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}