using Application.Common.Exceptions;
using Application.Common.Helper;
using Application.Common.Models.DTO.Department;
using Infrastructure.Common.Interfaces;
using Mapster;

namespace Application.Commands.DepartmentCommand;

public class InsertDepartmentCommand : IHttpRequest
{
    public required DepartmentRequest Department { get; set; }

}

public class InsertDepartmentCommandHandler : IHttpRequestHandler<InsertDepartmentCommand>
{
    private IUnitOfWork _unitOfWork;

    public InsertDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(InsertDepartmentCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Common.Helper.Result.Fail<List<DepartmentResponse>>(StatusCodes.Status406NotAcceptable);
        }

        try
        {

            var entity = request.Department.Adapt<Department>();
            entity.Id=Guid.NewGuid();
            entity.CreatedById= Guid.NewGuid();
            entity.IsDeleted = false;
            entity.CreatedDate=DateTime.Now;
            _unitOfWork.DepartmentRepository.Add(entity);
            var result = await _unitOfWork.CommitAsync();
            var response = await _unitOfWork.DepartmentRepository.GetAllNoneDeleted().Where(x => x.Id == entity.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return Common.Helper.Result.Success(response, $"Department {AlertMessage.SaveMessage}");
        }
        catch (Exception ex)
        {
            return Common.Helper.Result.Fail<DepartmentResponse>(StatusCodes.Status500InternalServerError);
        }
    }
}