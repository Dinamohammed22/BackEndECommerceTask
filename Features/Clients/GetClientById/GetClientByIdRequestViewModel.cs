using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.GetClientById
{
    public record GetClientByIdRequestViewModel(string ID);
    public class GetClientByIdRequestValidator : AbstractValidator<GetClientByIdRequestViewModel>
    {
        public GetClientByIdRequestValidator()
        {
        }
    }
    public class GetClientByIdRequestProfile : Profile
    {
        public GetClientByIdRequestProfile()
        {
            CreateMap<GetClientByIdRequestViewModel, GetClientByIdQuery>();
        }
    }
}
