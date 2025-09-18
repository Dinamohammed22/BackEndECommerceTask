using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Cities.AddRangeCities.Commands
{
    public record AddRangeCitiesCommand(List<CityDTO> cities) : IRequestBase<bool>;
    public class AddCityCommandHandler : RequestHandlerBase<City, AddRangeCitiesCommand, bool>
    {
        public AddCityCommandHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddRangeCitiesCommand request, CancellationToken cancellationToken)
        {
            List<City> cities = request.cities.Select(dto => new City
            {
                Name = dto.Name,
                GovernorateId = dto.GovernorateId,
                IsActive = dto.IsActive,
            }).ToList();

            _repository.AddRange(cities);
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);

        }
    }
}
