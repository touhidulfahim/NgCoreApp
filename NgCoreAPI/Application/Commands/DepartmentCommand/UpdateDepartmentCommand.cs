using Application.Common.Exceptions;
using Application.Common.Helper;
using Application.Common.Models.DTO.Department;
using Infrastructure.Common.Interfaces;

namespace Application.Commands.DepartmentCommand;

public class UpdateDepartmentCommand : IHttpRequest
{
    public required DepartmentRequest Department { get; set; }
}

public class UpdateDepartmentCommandHandler : IHttpRequestHandler<UpdateDepartmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Fail<DepartmentResponse>(StatusCodes.Status406NotAcceptable);
        }
        try
        {

            var enity = await _unitOfWork.DepartmentRepository.GetSingleNoneDeletedAsync(x => x.Id == request.Department.Id);
            if (enity is null)
            {
                return Result.Fail<DepartmentResponse>(StatusCodes.Status404NotFound);
            }
            enity.DepartmentCode = request.Department.DepartmentCode;
            enity.DepartmentName = request.Department.DepartmentName;
            _unitOfWork.DepartmentRepository.Update(enity);
            var result = await _unitOfWork.CommitAsync();
            var response = await _unitOfWork.DepartmentRepository.GetAllNoneDeleted().Where(x => x.Id == enity.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return Result.Success(response, $"Department {AlertMessage.UpdateMessage}");

        }
        catch (Exception ex)
        {
            return Result.Fail<DepartmentResponse>(StatusCodes.Status500InternalServerError);
        }
    }

}