using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.UpdateGovernorate.Commands;

public record UpdateGovernorateCommand(string ID, string Name, string GovernorateCode, bool IsActive) : IRequestBase<bool>;
public class UpdateGovernorateCommandHandler : RequestHandlerBase<Governorate, UpdateGovernorateCommand, bool>
{
    public UpdateGovernorateCommandHandler(RequestHandlerBaseParameters<Governorate> parameters) : base(parameters)
    { }

    public async override Task<RequestResult<bool>> Handle(UpdateGovernorateCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        Governorate governorate = new Governorate { ID=request.ID };
        governorate.Name = request.Name;
        governorate.GovernorateCode = request.GovernorateCode;
        governorate.IsActive=request.IsActive;
        _repository.SaveIncluded(governorate, nameof(governorate.Name),nameof(governorate.GovernorateCode),nameof(governorate.IsActive));
        _repository.SaveChanges();
        var result = RequestResult<bool>.Success(true);

        return await Task.FromResult(result);
    }
}
