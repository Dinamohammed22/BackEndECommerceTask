using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.ClientRegister.Commands;
using KOG.ECommerce.Features.Clients.CreateClient.Commands;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Clients.CreateClient.Orchestrators
{

    public record CreateClientOrchestrator(string? NationalNumber,string Name, string Password, string Mobile, string GovernorateId, string CityId, string Street,
        string Landmark, double Latitude, double Longitude, string? Email, string ConfirmPassword, 
        string? ClientGroupId, string? Phone, List<string>? Paths, ClientActivity? ClientActivity,string BuildingData, Religion Religion) : IRequestBase<string>;

    public class CreateClientOrchestratorHandler : RequestHandlerBase<Client, CreateClientOrchestrator, string>
    {
        public CreateClientOrchestratorHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateClientOrchestrator request, CancellationToken cancellationToken)
        {
            var phoneValid = await _repository.AnyAsync(c => c.Mobile == request.Mobile);
            if (!phoneValid)
            {
                var NationalNumberValid = await _repository.AnyAsync(c => c.NationalNumber == request.NationalNumber);
              if (!NationalNumberValid || request.NationalNumber.IsNullOrEmpty())
                {

                    var userIdResult = await _mediator.Send(new CreateUserCommand(
                    Name: request.Name,
                    Password:   request.Password,
                    ConfirmPassword:request.ConfirmPassword,
                    Mobile: request.Mobile,
                    RoleId: Role.Client,
                    VerifyStatus:VerifyStatus.Approve , JobTitle: null));

                if (!userIdResult.IsSuccess)
                {
                    return RequestResult<string>.Failure(userIdResult.ErrorCode);
                }

                  var result = await _mediator.Send(new CreateClientCommand(
                  Name: request.Name,
                  Mobile: request.Mobile,
                  Password: request.Password,
                  ConfirmPassword: request.ConfirmPassword,
                  ID: userIdResult.Data,
                  Email: request.Email,
                  ClientGroupId:request.ClientGroupId,
                  NationalNumber:request.NationalNumber,
                  Phone:request.Phone,
                  ClientActivity:request.ClientActivity,
                  Religion:request.Religion
                 ));


                if (!result.IsSuccess)
                {
                   return RequestResult<string>.Failure(result.ErrorCode);
                }

                    if (request.Paths != null && request.Paths.Any())
                    {
                        var saveMediaResult = await _mediator.Send(new SaveMediaCommand(
                            SourceId: result.Data,
                            SourceType: SourceType.Client,
                            Paths: request.Paths
                        ), cancellationToken);


                        if (!saveMediaResult.IsSuccess)
                        {

                            return RequestResult<string>.Failure(saveMediaResult.ErrorCode);
                        }
                    }

                    if (request.Latitude != null || request.Longitude != null)
                {
                var shipping = await _mediator.Send(new CreateShippingAddressCommand(
                   CityId: request.CityId,
                   Street: request.Street,
                   Landmark: request.Landmark,
                   Latitude: request.Latitude,
                   Longitude: request.Longitude,
                   GovernorateId: request.GovernorateId,
                   IsDefualt: true,
                   ClientId: userIdResult.Data,
                   BuildingData:request.BuildingData,
                   Status:ShippingAddressStatus.Approved
                   ));
                }
                 return RequestResult<string>.Success(userIdResult.Data);
                  
              }
                else
                {
                    return RequestResult<string>.Failure(ErrorCode.ExistNationalNumber);
                }
            }
            else
            {
                return RequestResult<string>.Failure(ErrorCode.ExistMobile);
            }
        }
    }
}
