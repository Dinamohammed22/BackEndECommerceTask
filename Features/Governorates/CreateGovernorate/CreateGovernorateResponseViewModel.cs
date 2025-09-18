using AutoMapper;
using KOG.ECommerce.Features.Governorates.CreateGovernorate.Commands;

namespace KOG.ECommerce.Features.Governorates.CreateGovernorate
{
    public record CreateGovernorateResponseViewModel(string ID);
    public class CreateGovernorateResponseProfile : Profile
    {
        public CreateGovernorateResponseProfile()
        {
            CreateMap<CreateGovernorateCommand, CreateGovernorateResponseViewModel>();
        }
    }
}
