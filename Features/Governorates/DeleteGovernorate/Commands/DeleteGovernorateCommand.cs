using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.DeleteGovernorate.Commands;

public record DeleteGovernorateCommand(string ID) : IRequestBase<bool>;
public class DeleteGovernorateCommandHandler : RequestHandlerBase<Governorate, DeleteGovernorateCommand, bool>
{
    public DeleteGovernorateCommandHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters) { }

    public async override Task<RequestResult<bool>> Handle(DeleteGovernorateCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        var checkCompany = await _mediator.Send(request.MapOne<CheckCompanyGovernorateIdQuery>());
        var checkShippingAddress = await _mediator.Send(request.MapOne<CheckShippingAddressGovernorateIdQuery>());
        if (!checkCompany.Data && !checkShippingAddress.Data)
        {
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
        var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

        return await Task.FromResult(result);
    }
}
