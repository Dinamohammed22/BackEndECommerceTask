using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Cities.DeactiveCity.Commands
{
    public record DeactiveCityCommand(string ID) : IRequestBase<bool>;
    public class DeactiveCityCommandHandler : RequestHandlerBase<City, DeactiveCityCommand, bool>
    {
        public DeactiveCityCommandHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactiveCityCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            City city = new City { ID = request.ID };
            city.IsActive = false;
            _repository.SaveIncluded(city, nameof(city.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
