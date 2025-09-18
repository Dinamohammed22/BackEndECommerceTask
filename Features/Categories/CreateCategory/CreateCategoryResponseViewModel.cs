using AutoMapper;

namespace KOG.ECommerce.Features.Categories.CreateCategory
{
    public record CreateCategoryResponseViewModel(string ParentId);

    public class CreateCategoryResponseProfile:Profile
    {
        public CreateCategoryResponseProfile()
        {
            CreateMap<string, CreateCategoryResponseViewModel>()
            .ConstructUsing(ParentId => new CreateCategoryResponseViewModel(ParentId));
        }
    }

}
