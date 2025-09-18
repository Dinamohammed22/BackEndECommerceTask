using AutoMapper;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductByIDWithMediaQuery(string ID) : IRequestBase<ProductMediaProfileDTO>;

    public class GetProductByIDWithMediaQueryHandler : RequestHandlerBase<Product, GetProductByIDWithMediaQuery, ProductMediaProfileDTO>
    {
        public GetProductByIDWithMediaQueryHandler(RequestHandlerBaseParameters<Product> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<ProductMediaProfileDTO>> Handle(GetProductByIDWithMediaQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetWithTracking(p => p.ID == request.ID)
            .Include(p => p.Category).ThenInclude(c => c.Subcategories)
            .Include(p => p.Brand)
            .Include(p => p.Company)
            .FirstOrDefaultAsync();

            if (product == null)
            {
                return RequestResult<ProductMediaProfileDTO>.Failure(ErrorCode.NotFound);
            }

            var productDto = product.MapOne<ProductMediaProfileDTO>();

            return RequestResult<ProductMediaProfileDTO>.Success(productDto);
        } 
    }
    
}
