using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Auth.Commands.BeginLoginCommand;
using RentalCar.Application.Auth.Commands.CompleteLoginCommand;

namespace RentalCar.Controllers;

public class AuthController : ApiControllerBase
{

    [AllowAnonymous]
    [HttpPost("BeginLogin")]
    public async Task<ActionResult> BeginLogin(BeginLoginCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpGet("{validationToken}/CompleteLogin")]
    public async Task<ActionResult> BeginLogin([FromRoute] string validationToken)
    {
        await Mediator.Send(new CompleteLoginCommand(validationToken));
        return Ok();
    }
    
}