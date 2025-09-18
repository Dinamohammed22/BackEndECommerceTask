using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.Modules;

namespace KOG.ECommerce.Features.Common.Modules.Queries
{
    public record GetModuleById(string ID):IRequestBase<GetModulesByRoleIdProfileDTO>;
    public class GetModuleByIdHandler : RequestHandlerBase<Module, GetModuleById, GetModulesByRoleIdProfileDTO>
    {
        public GetModuleByIdHandler(RequestHandlerBaseParameters<Module> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetModulesByRoleIdProfileDTO>> Handle(GetModuleById request, CancellationToken cancellationToken)
        {
            var module= _repository.GetByID(request.ID).MapOne<GetModulesByRoleIdProfileDTO>();
            return  RequestResult<GetModulesByRoleIdProfileDTO>.Success(module);
        }
    }
}
