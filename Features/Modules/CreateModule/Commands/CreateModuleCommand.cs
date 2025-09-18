using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Modules;

namespace KOG.ECommerce.Features.Modules.CreateModule.Commands
{
    public record CreateModuleCommand(string Name,string PageId) : IRequestBase<bool>;
    public class CreateModuleCommandHandler : RequestHandlerBase<Module, CreateModuleCommand, bool>
    {
        public CreateModuleCommandHandler(RequestHandlerBaseParameters<Module> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            Module module = new Module { Name = request.Name,PageId=request.PageId };
            _repository.Add(module);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }

}
