using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.AddRangeCities.Commands;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Governorates.AddRangeGovernrates.Commands;
using KOG.ECommerce.Features.Governorates.CreateGovernorate.Commands;
using KOG.ECommerce.Features.TempController.InitiateGovernrateCitiesData.DTOs;
using KOG.ECommerce.Models.Governorates;
using System.Text.Json;

namespace KOG.ECommerce.Features.TempController.InitiateGovernrateCitiesData.Orchistrator
{
    public record InitiateGovernrateCitiesDataOrchistrator() : IRequestBase<bool>;

    public class InitiateGovernrateCitiesDataOrchistratorHandler : RequestHandlerBase<Governorate, InitiateGovernrateCitiesDataOrchistrator, bool>
    {
        public InitiateGovernrateCitiesDataOrchistratorHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<bool>> Handle(InitiateGovernrateCitiesDataOrchistrator request, CancellationToken cancellationToken)
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data");
            string governoratesFilePath = Path.Combine(basePath, "governorates.json");
            string citiesFilePath = Path.Combine(basePath, "cities.json");

            if (!Directory.Exists(basePath) || !File.Exists(governoratesFilePath) || !File.Exists(citiesFilePath))
            {
                return RequestResult<bool>.Failure();
            }

            var governoratesData = JsonSerializer.Deserialize<List<GovernorateJsonDTO>>(await File.ReadAllTextAsync(governoratesFilePath, cancellationToken));
            var citiesData = JsonSerializer.Deserialize<List<CityJsonDTO>>(await File.ReadAllTextAsync(citiesFilePath, cancellationToken));

            if (governoratesData == null || citiesData == null)
            {
                return RequestResult<bool>.Failure();
            }

            var governorateIdMap = new Dictionary<string, string>();

            foreach (var governorate in governoratesData)
            {
                var governorateIdResult = await _mediator.Send(new CreateGovernorateCommand(governorate.governorate_name_ar, governorate.governorate_code,true));
                if (!governorateIdResult.IsSuccess)
                {
                    return RequestResult<bool>.Failure();
                }

                governorateIdMap[governorate.id] = governorateIdResult.Data;
            }

            foreach (var city in citiesData)
            {
                if (!governorateIdMap.TryGetValue(city.governorate_id, out var newGovernorateId))
                {
                    return RequestResult<bool>.Failure();
                }

                var cityResult = await _mediator.Send(new CreateCityCommand(city.city_name_ar, newGovernorateId,true));
                if (!cityResult.IsSuccess)
                {
                    return RequestResult<bool>.Failure();
                }
            }

            return RequestResult<bool>.Success(true);
        }
    }

}
