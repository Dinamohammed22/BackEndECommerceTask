using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;

namespace KOG.ECommerce.Features.Users.GetAllUsers
{
    public record GetAllUsersRequestViewModel(string? Mobile, string? UserName, int pageIndex = 1,
        int pageSize = 100);
    public class GetAllUsersRequestValidator : AbstractValidator<GetAllUsersRequestViewModel>
    {
        public GetAllUsersRequestValidator()
        {
        }
    }
    public class GetAllUsersRequestProfile : Profile
    {
        public GetAllUsersRequestProfile()
        {
            CreateMap<GetAllUsersRequestViewModel, GetAllUsersQuery>();
        }
    }
}
