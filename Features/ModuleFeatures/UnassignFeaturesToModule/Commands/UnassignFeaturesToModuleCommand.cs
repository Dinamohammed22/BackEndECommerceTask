using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule.Commands;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ModuleFeatures;

namespace KOG.ECommerce.Features.ModuleFeatures.UnassignFeaturesToModule.Commands
{
    public record UnassignFeaturesToModuleCommand(string ModuleId, Feature Feature) : IRequestBase<bool>;
    public class UnassignFeaturesToModuleCommandHandler : RequestHandlerBase<ModuleFeature, UnassignFeaturesToModuleCommand, bool>
    {
        public UnassignFeaturesToModuleCommandHandler(RequestHandlerBaseParameters<ModuleFeature> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(UnassignFeaturesToModuleCommand request, CancellationToken cancellationToken)
        {
            var existingFeature = _repository
                .GetWithDeleted().Where(rf => rf.ModuleId == request.ModuleId && rf.Features == request.Feature).FirstOrDefault();

            if (existingFeature != null)
            {
           

                _repository.Delete(existingFeature);
                 _repository.SaveChanges();
            }
          
            return RequestResult<bool>.Success(true);

        }
    }
}
