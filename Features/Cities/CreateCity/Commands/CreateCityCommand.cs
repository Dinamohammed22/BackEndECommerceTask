using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Cities.CreateCity.Commands
{
    public record CreateCityCommand(string Name, string GovernorateId, bool IsActive) : IRequestBase<string>;
    public class AddCityCommandHandler : RequestHandlerBase<City, CreateCityCommand, string>
    {
        public AddCityCommandHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City city=new City() { Name=request.Name,GovernorateId=request.GovernorateId, IsActive = request.IsActive };
            _repository.Add(city);
            _repository.SaveChanges();

            var result = RequestResult<string>.Success(city.ID);

            return await Task.FromResult(result);

        }
    }
}
