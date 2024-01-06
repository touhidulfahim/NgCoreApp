using Application.Common.Helper;
using Application.Common.Models.DTO.Department;
using Infrastructure.Common.Interfaces;

namespace Application.Queries.DepartmentQueries;

public record GetAllDepartmentQuery : IHttpRequest;

public class GetAllDepartmentQueryHandler : IHttpRequestHandler<GetAllDepartmentQuery>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDepartmentQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var responses = await _unitOfWork.DepartmentRepository.GetAllNoneDeleted().ToListAsync(cancellationToken: cancellationToken);
            if (responses is null)
            {
                return Result.Fail<List<DepartmentResponse>>(StatusCodes.Status404NotFound);
            }
            return Result.Success(responses);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<DepartmentResponse>>(StatusCodes.Status500InternalServerError);
        }
    }
}
