using Application.Common.Helper;
using Application.Common.Models.DTO.Student;
using Infrastructure.Common.Interfaces;

namespace Application.Queries.StudentQueries;

public record GetStudentByIdQuery (Guid id):IHttpRequest;
public class GetStudentByIdQueryHandler : IHttpRequestHandler<GetStudentByIdQuery>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStudentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var responses = await _unitOfWork.StudentRepository.GetAllNoneDeleted().Where(s=>s.Id==request.id).Select(x => new StudentResponse()
            {
                Id = x.Id,
                StudentID = x.StudentID,
                StudentName = x.StudentName,
                FathersName = x.FathersName,
                PostalAddress = x.PostalAddress,
                BirthDate = x.BirthDate,
                DOB = x.BirthDate.ToString("dd MMM yyyy"),
                Mobile = x.Mobile,
                Email = x.Email,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName

            }).ToListAsync(cancellationToken: cancellationToken);
            if (responses is null)
            {
                return Result.Fail<List<StudentResponse>>(StatusCodes.Status404NotFound);
            }
            return Result.Success(responses);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<StudentResponse>>(StatusCodes.Status500InternalServerError);
        }
    }
}
