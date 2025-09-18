using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.GetClientByMobile
{
    public record GetClientByMobileRequestViewModel(string Mobile);
    public class GetClientByNationalNumberRequestValidator : AbstractValidator<GetClientByMobileRequestViewModel>
    {
        public GetClientByNationalNumberRequestValidator() { }
    }
    public class GetClientByNationalNumberRequestProfile : Profile
    {
        public GetClientByNationalNumberRequestProfile()
        {
            CreateMap<GetClientByMobileRequestViewModel, GetClientByMobileQuery>();
        }
    }

}
