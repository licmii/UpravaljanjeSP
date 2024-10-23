using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Car.Commands;
using RentalCar.Application.Car.Queries;

namespace RentalCar.Controllers;

[Route("Car")]
public class CarController : ApiControllerBase
{
    [HttpGet("GetOneCar")]
    public async Task<ActionResult> GetCar([FromQuery] GetOneCarQuery query) => Ok(await Mediator.Send(query));
    
    [HttpPost("CreateCar")]
    public async Task<ActionResult> CreateCar(CreateCarCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpGet("List")]
    public async Task<ActionResult> List() => Ok(await Mediator.Send(new GetCarListQuery()));
    
    [HttpPost("many-many-cars")]
    public async Task<ActionResult> manyManyCars(ManyManyCarsCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}