using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Modules;
using KOG.ECommerce.Models.Pages;

namespace KOG.ECommerce.Features.Pages.CreatePage.Commands
{
    public record CreatePageCommand(string Name):IRequestBase<bool>;
    public class CreatePageCommandHandler : RequestHandlerBase<Page, CreatePageCommand, bool>
    {
        public CreatePageCommandHandler(RequestHandlerBaseParameters<Page> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
            Page page = new Page {  Name=request.Name };
            _repository.Add(page);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);

        }
    }

}
