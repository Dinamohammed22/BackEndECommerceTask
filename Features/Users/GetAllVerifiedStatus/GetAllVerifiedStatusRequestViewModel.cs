using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;

namespace KOG.ECommerce.Features.Users.GetAllVerifiedStatus
{
    public record GetAllVerifiedStatusRequestViewModel(int pageIndex = 1, int pageSize = 100);
    public class GetAllVerifiedStatusRequestValidator : AbstractValidator<GetAllVerifiedStatusRequestViewModel>
    {
        public GetAllVerifiedStatusRequestValidator()
        {

        }

    }
    public class GetAllVerifiedStatusEndPointRequestProfile : Profile
    {
        public GetAllVerifiedStatusEndPointRequestProfile()
        {
            CreateMap<GetAllVerifiedStatusRequestViewModel, GetAllVerifiedStatusQuery>();
        }
    }
}
