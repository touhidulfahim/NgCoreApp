using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private ISender? _sender;
    internal ISender Mediator => (_sender ??= HttpContext.RequestServices.GetService<ISender>())!;
}
