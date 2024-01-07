using Application.Common.Exceptions;
using Application.Common.Helper;
using Application.Common.Models.DTO.Student;
using Infrastructure.Common.Interfaces;

namespace Application.Commands.StudentCommand;

public class UpdateStudentCommand : IHttpRequest
{
    public required StudentRequest Student { get; set; }
}

public class UpdateStudentCommandHandler : IHttpRequestHandler<UpdateStudentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Fail<StudentResponse>(StatusCodes.Status406NotAcceptable);
        }
        try
        {

            var entity = await _unitOfWork.StudentRepository.GetSingleNoneDeletedAsync(x => x.Id == request.Student.Id);
            if (entity is null)
            {
                return Result.Fail<StudentResponse>(StatusCodes.Status404NotFound);
            }
            entity.StudentName = request.Student.StudentName;
            entity.FathersName = request.Student.FathersName;
            entity.PostalAddress = request.Student.PostalAddress;
            entity.Mobile = request.Student.Mobile;
            entity.Email = request.Student.Email;
            _unitOfWork.StudentRepository.Update(entity);
            var result = await _unitOfWork.CommitAsync();
            var response = await _unitOfWork.StudentRepository.GetAllNoneDeleted().Where(x => x.Id == entity.Id).Select(x => new StudentResponse()
            {
                Id = x.Id,
                StudentID = x.StudentID,
                StudentName = x.StudentName,
                FathersName = x.FathersName,
                PostalAddress = x.PostalAddress,
                Mobile = x.Mobile,
                Email = x.Email,
                BirthDate = x.BirthDate,
                DOB=x.BirthDate.ToString("dd MMM yyyy"),
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName

            }).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return Result.Success(response, $"Student {AlertMessage.UpdateMessage}");

        }
        catch (Exception ex)
        {
            return Result.Fail<StudentResponse>(StatusCodes.Status500InternalServerError);
        }
    }

}