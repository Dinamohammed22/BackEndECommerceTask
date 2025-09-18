using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.ShippingAddresses.SetDefaultShippingAddress.Commands
{
    public record SetDefaultShippingAddressCommand(string ID,string ClientId):IRequestBase<bool>;
    public class SetDefaultShippingAddressCommandHandler : RequestHandlerBase<ShippingAddress, SetDefaultShippingAddressCommand, bool>
    {
        public SetDefaultShippingAddressCommandHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(SetDefaultShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            var DefaultShippingAddress= await _repository.Get(s=>s.ClientId == request.ClientId && s.IsDefualt)
                .Select(s=>s.ID).FirstOrDefaultAsync();
            if (DefaultShippingAddress.IsNullOrEmpty())
                RequestResult<bool>.Failure(ErrorCode.NoDefaultShippingAddress);
            var OldDefault= new ShippingAddress { ID = DefaultShippingAddress };
            OldDefault.IsDefualt = false;
            _repository.SaveIncluded(OldDefault, nameof(OldDefault.IsDefualt));
            _repository.SaveChanges();
            var NewDefault = new ShippingAddress { ID = request.ID };
            NewDefault.IsDefualt = true;
            _repository.SaveIncluded(NewDefault, nameof(NewDefault.IsDefualt));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);

        }
    }

}
