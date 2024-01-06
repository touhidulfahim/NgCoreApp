using Application.Commands.DepartmentCommand;
using Application.Common.Models.DTO.Department;
using Application.Queries.DepartmentQueries;
using Microsoft.AspNetCore.Mvc;
using IResult = Application.Common.Helper.IResult;
namespace WebApi.Controllers;

public class DepartmentController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<DepartmentResponse>))]
    public Task<IResult> Get()
    {
        return Mediator.Send(new GetAllDepartmentQuery());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentResponse))]
    public Task<IResult> Get(Guid id)
    {
        return Mediator.Send(new GetDepartmentByIdQuery(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentResponse))]
    public Task<IResult> Post([FromBody] DepartmentRequest request)
    {
        InsertDepartmentCommand cmd = new InsertDepartmentCommand() { Department = request };
        return Mediator.Send(cmd);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentResponse))]
    public Task<IResult> Put([FromBody] DepartmentRequest request)
    {
        UpdateDepartmentCommand cmd = new UpdateDepartmentCommand() { Department = request };
        return Mediator.Send(cmd);
    }

    [HttpDelete("{id}")]
    public Task<IResult> Delete(Guid id)
    {
        return Mediator.Send(new DeleteDepartmentCommand(id));
    }
}
