using Application.Common.Exceptions;
using Application.Common.Helper;
using Infrastructure.Common.Interfaces;

namespace Application.Commands.DepartmentCommand;

public record DeleteDepartmentCommand(Guid Id) : IHttpRequest;

public class DeleteDepartmentCommandHandler : IHttpRequestHandler<DeleteDepartmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == Guid.Empty)
            {
                return Result.Fail<string>(StatusCodes.Status406NotAcceptable);
            }

            var enity = await _unitOfWork.DepartmentRepository.GetSingleNoneDeletedAsync(x => x.Id == request.Id);

            if (enity == null)
            {
                return Result.Fail<string>(StatusCodes.Status404NotFound);
            }

            var result = await _unitOfWork.DepartmentRepository.InstantDelete(enity);

            if (result)
            {
                return Result.Success<string>("Department " + AlertMessage.DeleteMessage);
            }
            else
            {
                return Result.Fail<string>("Department delete" + AlertMessage.OperationFailed);
            }
        }
        catch (Exception ex)
        {
            return Result.Fail<string>(StatusCodes.Status500InternalServerError);
        }
    }
}