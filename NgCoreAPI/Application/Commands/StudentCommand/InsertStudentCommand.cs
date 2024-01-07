using Application.Common.Exceptions;
using Application.Common.Helper;
using Application.Common.Models.DTO.Student;
using Infrastructure.Common.Interfaces;
using Mapster;

namespace Application.Commands.StudentCommand;

public class InsertStudentCommand : IHttpRequest
{
    public required StudentRequest Student { get; set; }
}

public class InsertStudentCommandHandler : IHttpRequestHandler<InsertStudentCommand>
{
    private IUnitOfWork _unitOfWork;

    public InsertStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(InsertStudentCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Common.Helper.Result.Fail<List<StudentResponse>>(StatusCodes.Status406NotAcceptable);
        }

        try
        {

            var entity = request.Student.Adapt<Student>();
            entity.Id = Guid.NewGuid();
            entity.CreatedById = Guid.NewGuid();
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.Now;
            _unitOfWork.StudentRepository.Add(entity);
            var result = await _unitOfWork.CommitAsync();
            var response = await _unitOfWork.StudentRepository.GetAllNoneDeleted().Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return Common.Helper.Result.Success(response, $"Student {AlertMessage.SaveMessage}");
        }
        catch (Exception ex)
        {
            return Common.Helper.Result.Fail<StudentResponse>(StatusCodes.Status500InternalServerError);
        }
    }
}