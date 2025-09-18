using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.GetClientViewById
{
    public record GetClientViewByIdRequestViewModel(string ID);
    public class GetClientViewByIdRequestValidator : AbstractValidator<GetClientViewByIdRequestViewModel>
    {
        public GetClientViewByIdRequestValidator()
        {
        }
    }
    public class GetClientViewByIdRequestProfile : Profile
    {
        public GetClientViewByIdRequestProfile()
        {
            CreateMap<GetClientViewByIdRequestViewModel, GetClientViewByIdQuery>();
        }
    }
}
