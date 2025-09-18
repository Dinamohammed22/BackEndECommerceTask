using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Cities.ActiveCity.Commands
{
    public record ActiveCityCommand(string ID) : IRequestBase<bool>;
    public class ActiveCityCommandHandler : RequestHandlerBase<City, ActiveCityCommand, bool>
    {
        public ActiveCityCommandHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActiveCityCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            City city = new City { ID = request.ID };
            city.IsActive = true;
            _repository.SaveIncluded(city, nameof(city.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }

}
