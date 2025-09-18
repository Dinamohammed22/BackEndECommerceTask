using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Companies.Commands;
using KOG.ECommerce.Features.Companies;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies;

public record GetCompanyByIDRequestViewModel(string ID);
public class GetGroupByIDEndPointRequestValidator : AbstractValidator<GetCompanyByIDRequestViewModel>
{
    public GetGroupByIDEndPointRequestValidator()
    {
        RuleFor(request => request.ID).NotEmpty();
    }
}

public class GetCompanyByIDRequestProfile : Profile
{
    public GetCompanyByIDRequestProfile()
    {
        CreateMap<GetCompanyByIDRequestViewModel, GetCompanyByIDQuery>();
    }
}
