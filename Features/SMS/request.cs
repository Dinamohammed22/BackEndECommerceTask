using AutoMapper;
using FluentValidation;

namespace KOG.ECommerce.Features.SMS
{
    public record request(string Mobile, string Message);
    public class requestValidat : AbstractValidator<request>
    {
        public requestValidat() { }
    }
    public class requestProfile : Profile
    {
        public requestProfile()
        {
            CreateMap<request, Command>();
        }
    }
}
