using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums; 
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record ClientsOfCompanyStatisticsQuery(DateTime From, DateTime To) : IRequestBase<ClientsStatisticsDTO>;

    public class ClientsOfCompanyStatisticsQueryHandler : RequestHandlerBase<Client, ClientsOfCompanyStatisticsQuery, ClientsStatisticsDTO>
    {
        public ClientsOfCompanyStatisticsQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<ClientsStatisticsDTO>> Handle(ClientsOfCompanyStatisticsQuery request, CancellationToken cancellationToken)
        {
          
            var numberOfCustomers = await _repository
                .Get(c => c.CreatedDate >= request.From &&c.CreatedDate <= request.To)
                .CountAsync();

            var customersWaitingApproval = await _repository
                .Get(c => c.User != null &&c.User.RoleId==Role.Client && c.User.VerifyStatus == VerifyStatus.Verified)
                .CountAsync();

            var clientsStatistics = new ClientsStatisticsDTO(
                NumberOfCustomers: numberOfCustomers,
                CustomersWaitingApproval: customersWaitingApproval
            );

            return RequestResult<ClientsStatisticsDTO>.Success(clientsStatistics);
        }
    }
}
