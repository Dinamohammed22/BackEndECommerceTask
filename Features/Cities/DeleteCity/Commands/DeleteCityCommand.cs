using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.EditCity.Commands;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Cities.DeleteCity.Commands
{
    public record DeleteCityCommand(string Id) : IRequestBase<bool>;
    public class DeleteCityCommandHandler : RequestHandlerBase<City, DeleteCityCommand, bool>
    {
        public DeleteCityCommandHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var checkCompany = await _mediator.Send(request.MapOne<CheckCompanyCityIdQuery>());
            var checkShippingAddress = await _mediator.Send(request.MapOne<CheckShippingAddressCityIdQuery>());
            if (!checkCompany.Data && !checkShippingAddress.Data)
            {
                _repository.Delete(request.Id);
                _repository.SaveChanges();
                return await Task.FromResult(RequestResult<bool>.Success(true));
            }
            var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

            return await Task.FromResult(result);

        }
    }
}