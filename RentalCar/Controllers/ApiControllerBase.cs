using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RentalCar.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{

    private ISender? _mediator;

    protected ISender Mediator
    {
        get { return _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>(); }
    }
}