using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.AddRangeGovernrates.Commands
{
    public record AddRangeGovernratesCommand(List<GovernrateDTO> Governrates) : IRequestBase<bool>;
    public class AddCityCommandHandler : RequestHandlerBase<Governorate, AddRangeGovernratesCommand, bool>
    {
        public AddCityCommandHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddRangeGovernratesCommand request, CancellationToken cancellationToken)
        {
            List<Governorate> Governrates = request.Governrates.Select(dto => new Governorate
            {
                Name = dto.Name,
                GovernorateCode = dto.GovernorateCode
                
            }).ToList();

            _repository.AddRange(Governrates);
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);

        }
    }
}