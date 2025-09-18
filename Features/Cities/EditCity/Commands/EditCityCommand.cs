using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Cities.EditCity.Commands
{
    public record EditCityCommand(string Id,string Name, string GovernorateId, bool IsActive) : IRequestBase<bool>;
    public class EditCityCommandHandler : RequestHandlerBase<City, EditCityCommand, bool>
    {
        public EditCityCommandHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(EditCityCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            City city=new City { ID=request.Id};
            city.Name = request.Name;
            city.GovernorateId = request.GovernorateId;
            city.IsActive=request.IsActive;
            _repository.SaveIncluded(city, [nameof(city.Name), nameof(city.GovernorateId),nameof(city.IsActive)]);

            _repository.SaveChanges();

            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }

    }
}
