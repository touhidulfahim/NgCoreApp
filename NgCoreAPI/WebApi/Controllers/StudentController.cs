using Application.Commands.StudentCommand;
using Application.Common.Models.DTO.Student;
using Application.Queries.StudentQueries;
using Microsoft.AspNetCore.Mvc;
using IResult = Application.Common.Helper.IResult;

namespace WebApi.Controllers;

public class StudentController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<StudentResponse>))]
    public Task<IResult> Get()
    {
        return Mediator.Send(new GetAllStudentQuery());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentResponse))]
    public Task<IResult> Get(Guid id)
    {
        return Mediator.Send(new GetStudentByIdQuery(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentResponse))]
    public Task<IResult> Post([FromBody] StudentRequest request)
    {
        InsertStudentCommand cmd = new InsertStudentCommand() { Student = request };
        return Mediator.Send(cmd);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentResponse))]
    public Task<IResult> Put([FromBody] StudentRequest request)
    {
        UpdateStudentCommand cmd = new UpdateStudentCommand() { Student = request };
        return Mediator.Send(cmd);
    }

    [HttpDelete("{id}")]
    public Task<IResult> Delete(Guid id)
    {
        return Mediator.Send(new DeleteStudentCommand(id));
    }

}
