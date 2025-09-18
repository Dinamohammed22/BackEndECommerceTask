using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.FilterClient
{
    public record SearchClientRequestViewModel(
        string? Name, 
        string? Email, 
        string? Mobile,
        string? NationalNumber,
        string? ClientGroupId,
        VerifyStatus? VerifyStatus,
        DateTime? From, 
        DateTime? To,
        bool? Deleted ,
        Religion? Religion,
        int pageIndex = 1,
        int pageSize = 100
    );

    public class SearchClientRequestValidator : AbstractValidator<SearchClientRequestViewModel>
    {
        public SearchClientRequestValidator()
        {

        }
    }

    public class SearchClientRequestProfile : Profile
    {
        public SearchClientRequestProfile()
        {
            CreateMap<SearchClientRequestViewModel, SearchClientQuery>();
        }
    }
}
