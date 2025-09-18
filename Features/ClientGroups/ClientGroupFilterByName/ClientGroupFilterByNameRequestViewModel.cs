using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;

namespace KOG.ECommerce.Features.ClientGroups.ClientGroupFilterByName
{
    public record ClientGroupFilterByNameRequestViewModel(string? Name , int pageIndex = 1,
        int pageSize = 100);
    public class ClientGroupFilterByNameRequestValidator : AbstractValidator<ClientGroupFilterByNameRequestViewModel>
    {
        public ClientGroupFilterByNameRequestValidator()
        {
        }
    }
    public class ClientGroupFilterByNameRequestProfile : Profile
    {
        public ClientGroupFilterByNameRequestProfile()
        {
            CreateMap<ClientGroupFilterByNameRequestViewModel, ClientGroupFilterByNameQuery>();
        }
    }
}
