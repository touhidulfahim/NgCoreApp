using Application.Common.Helper;
using Application.Common.Models.DTO.Department;
using Infrastructure.Common.Interfaces;

namespace Application.Queries.DepartmentQueries;

public record GetDepartmentByIdQuery (Guid id):IHttpRequest;
public class GetDepartmentByIdQueryHandler : IHttpRequestHandler<GetDepartmentByIdQuery>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDepartmentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _unitOfWork.DepartmentRepository
                .GetAllNoneDeleted().Where(x => x.Id == request.id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (response is null)
            {
                return Result.Fail<DepartmentResponse>(StatusCodes.Status404NotFound);
            }

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return Result.Fail<DepartmentResponse>(StatusCodes.Status500InternalServerError);
        }
    }
}
