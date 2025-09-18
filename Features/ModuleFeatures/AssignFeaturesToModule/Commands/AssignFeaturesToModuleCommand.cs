using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ModuleFeatures;
using KOG.ECommerce.Models.RoleFeatures;

namespace KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule.Commands
{
    public record AssignFeaturesToModuleCommand(string ModuleId, Feature Feature) : IRequestBase<bool>;
    public class AssignFeaturesToModuleCommandHandler : RequestHandlerBase<ModuleFeature, AssignFeaturesToModuleCommand, bool>
    {
        public AssignFeaturesToModuleCommandHandler(RequestHandlerBaseParameters<ModuleFeature> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AssignFeaturesToModuleCommand request, CancellationToken cancellationToken)
        {
            var existingFeature = _repository
                .GetWithDeleted().Where(rf => rf.ModuleId == request.ModuleId && rf.Features == request.Feature).FirstOrDefault();

            if (existingFeature == null)
            {
                var moduleFeature = new ModuleFeature
                {
                    ModuleId = request.ModuleId,
                    Features = request.Feature
                };

                _repository.Add(moduleFeature);

            }
            else
            {
                existingFeature.Deleted = false;
                _repository.SaveIncluded(existingFeature, nameof(existingFeature.Deleted));
            }
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        
    }
    }
}
