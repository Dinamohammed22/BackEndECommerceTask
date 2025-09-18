using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Governorates.CreateGovernorate.Commands;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Brands.CreateBrand.Commands
{
    public record CreateBrandCommand(string Name, List<string> Tags, bool IsActive) : IRequestBase<string>;

    public class CreateBrandCommandHandler : RequestHandlerBase<Brand, CreateBrandCommand, string>
    {
        public CreateBrandCommandHandler(RequestHandlerBaseParameters<Brand> parameters) : base(parameters)
        { }

        public async override Task<RequestResult<string>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = new Brand { Name = request.Name,Tags=request.Tags, IsActive=request.IsActive };
            _repository.Add(brand);
            _repository.SaveChanges();

            var result = RequestResult<string>.Success(brand.ID);

            return await Task.FromResult(result);
        }

    }
}
