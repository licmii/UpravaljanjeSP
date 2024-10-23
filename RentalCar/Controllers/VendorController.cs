using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Vendor.commands;

namespace RentalCar.Controllers;

[Route("vendor")]
public class VendorController : ApiControllerBase
{
    
    [HttpPost("CreateVendor")]
    public async Task<ActionResult> CreateVendor(CreateVendorCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}