using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Rental.Commands;
using RentalCar.Application.Rental.Queries;

namespace RentalCar.Controllers;

[Route("rent")]
public class RentalController : ApiControllerBase
{
    [HttpPost("CreateRental")]
    public async Task<ActionResult> CreateRental(CreateRentalCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpGet("GetRentalForOneUser")]
    public async Task<ActionResult> GetRentalForOneUser([FromQuery] GetRentalForOneUserQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}