using Application.Common.Exceptions;
using Application.Common.Helper;
using Infrastructure.Common.Interfaces;

namespace Application.Commands.StudentCommand;

public record DeleteStudentCommand(Guid Id) : IHttpRequest;

public class DeleteStudentCommandHandler : IHttpRequestHandler<DeleteStudentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == Guid.Empty)
            {
                return Result.Fail<string>(StatusCodes.Status406NotAcceptable);
            }

            var enity = await _unitOfWork.StudentRepository.GetSingleNoneDeletedAsync(x => x.Id == request.Id);

            if (enity == null)
            {
                return Result.Fail<string>(StatusCodes.Status404NotFound);
            }

            var result = await _unitOfWork.StudentRepository.InstantDelete(enity);

            if (result)
            {
                return Result.Success<string>("Student " + AlertMessage.DeleteMessage);
            }
            else
            {
                return Result.Fail<string>("Student delete" + AlertMessage.OperationFailed);
            }
        }
        catch (Exception ex)
        {
            return Result.Fail<string>(StatusCodes.Status500InternalServerError);
        }
    }
}