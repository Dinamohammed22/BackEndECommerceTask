using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Modules.Queries;
using KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleModules;

namespace KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.Queries
{
    public record GetModulesByRoleIdQuery(Role RoleId):IRequestBase<IEnumerable<GetModulesByRoleIdProfileDTO>>;
    public class GetModulesByRoleIdQueryHandler : RequestHandlerBase<RoleModule, GetModulesByRoleIdQuery, IEnumerable<GetModulesByRoleIdProfileDTO>>
    {
        public GetModulesByRoleIdQueryHandler(RequestHandlerBaseParameters<RoleModule> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetModulesByRoleIdProfileDTO>>> Handle(GetModulesByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var ModulesId =  _repository
                .Get(rm => rm.RoleId == request.RoleId)
                .Select(rm => rm.ModuleId)
                .ToList();
            List<GetModulesByRoleIdProfileDTO> Modules= new List<GetModulesByRoleIdProfileDTO>(); ;
            foreach (var module in ModulesId)
            {
                var Module = await _mediator.Send(new GetModuleById(module));
                Modules.Add(Module.Data);
            }
            return RequestResult<IEnumerable<GetModulesByRoleIdProfileDTO>>.Success(Modules);
        }
    }
}
