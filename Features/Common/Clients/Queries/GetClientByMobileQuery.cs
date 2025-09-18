using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record GetClientByMobileQuery(string Mobile) :IRequestBase<GetClientByNationalNumberDTO>;
    public class GetClientByNationalNumberQueryHandler : RequestHandlerBase<Client, GetClientByMobileQuery, GetClientByNationalNumberDTO>
    {
        public GetClientByNationalNumberQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetClientByNationalNumberDTO>> Handle(GetClientByMobileQuery request, CancellationToken cancellationToken)
        {
            var client = await _repository
                .Get(c => c.Mobile == request.Mobile)
                .Include(c => c.ShippingAddresses)  
                .ThenInclude(sa => sa.Governorate)  
                .Include(c => c.ShippingAddresses)
                .ThenInclude(sa => sa.City)   
                .Include(c=>c.User)
                .Include(c=>c.ClientGroup)
                .FirstOrDefaultAsync(cancellationToken);

            if (client == null)
            {
                return RequestResult<GetClientByNationalNumberDTO>.Failure(ErrorCode.NotFound);
            }
            var status=await _mediator.Send(new GetUserStatusQuery(client.ID));
            if (!status.IsSuccess) { 
                return RequestResult<GetClientByNationalNumberDTO>.Failure(status.ErrorCode);
            }
            if( status.Data == VerifyStatus.Pending)
            {
                return RequestResult<GetClientByNationalNumberDTO>.Failure(ErrorCode.NotVerified);

            }
            if (status.Data == VerifyStatus.Verified)
            {
                return RequestResult<GetClientByNationalNumberDTO>.Failure(ErrorCode.NotApproved);

            }
            if (!client.User.IsActive)
            {
                return RequestResult<GetClientByNationalNumberDTO>.Failure(ErrorCode.NotActive);
            }
            var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(client.ID, SourceType.Client), cancellationToken);
            string? clientPath = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;
            var clientDto = new GetClientByNationalNumberDTO(
                client.NationalNumber,
                client.Name,
                client.Mobile,
                client.ShippingAddresses
                    .Where(sa => sa.IsDefualt)  
                    .Select(sa => new GetShippingAddressDTO(
                        sa.ID,
                        sa.Governorate.Name, 
                        sa.Governorate.ID,
                        sa.City.Name, 
                        sa.City.ID,
                        sa.Street, 
                        sa.Landmark, 
                        sa.Latitude,  
                        sa.Longitude
                       
                    ))
                    .FirstOrDefault(),  
                client.User.IsActive,
                client.User?.ID  ,
                client.Email,
                client.Phone,
                client.ClientGroupId,
                client.ClientGroup?.Name,
                clientPath,
                status.Data,
                client.Religion
            );

            return RequestResult<GetClientByNationalNumberDTO>.Success(clientDto);
        }


    }


}
