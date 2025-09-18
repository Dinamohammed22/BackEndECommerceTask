using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.GetClientQuery
{
    public class GetClientRequestViewModel();

    public class GetClientRequestValidator : AbstractValidator<GetClientRequestViewModel>
    {
        public GetClientRequestValidator()
        {

        }
    }

    public class GetClientRequestProfile : Profile
    {
        public GetClientRequestProfile()
        {
            CreateMap<GetClientRequestViewModel, GetClientsQuery>();
        }
    }
}
