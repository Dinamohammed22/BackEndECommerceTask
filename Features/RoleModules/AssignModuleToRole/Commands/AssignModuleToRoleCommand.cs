using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleModules;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.RoleModules.AssignModuleToRole.Commands
{
    public record AssignModuleToRoleCommand(Role RoleId, List<string> ModulesId) : IRequestBase<bool>;

    public class AssignModuleToRoleCommandHandler : RequestHandlerBase<RoleModule, AssignModuleToRoleCommand, bool>
    {
        public AssignModuleToRoleCommandHandler(RequestHandlerBaseParameters<RoleModule> requestParameters)
            : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AssignModuleToRoleCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(Role), request.RoleId))
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            // Retrieve existing modules assigned to the role
            var existingModules = await _repository
                .Get(rm => rm.RoleId == request.RoleId && request.ModulesId.Contains(rm.ModuleId))
                .Select(rm => rm.ModuleId)
                .ToListAsync();

            // Add only new modules that are not already assigned
            foreach (var moduleId in request.ModulesId)
            {
                if (!existingModules.Contains(moduleId))
                {
                    var roleModule = new RoleModule
                    {
                        RoleId = request.RoleId,
                        ModuleId = moduleId
                    };
                    _repository.Add(roleModule);
                }
            }

            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
