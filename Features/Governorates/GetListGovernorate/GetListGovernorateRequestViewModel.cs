using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Governorates.Queries;

namespace KOG.ECommerce.Features.Governorates.GetListGovernorate
{
    public record GetListGovernorateRequestViewModel(int pageIndex = 1, int pageSize = 100);


    public class GetGroupListEndPointRequestValidator : AbstractValidator<GetListGovernorateRequestViewModel>
    {
        public GetGroupListEndPointRequestValidator()
        {
            //RuleFor(request => request.Name).NotEmpty();
        }
    }

    public class GetListGovernorateRequestProfile : Profile
    {
        public GetListGovernorateRequestProfile()
        {
            CreateMap<GetListGovernorateRequestViewModel, GetListGovernorateQuery>();
        }
    }
}
