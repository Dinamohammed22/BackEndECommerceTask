using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.BlackListTokens;

namespace KOG.ECommerce.Features.BlackListTokens.AddBlackListToken.Commands
{
    public record AddBlackListTokenCommand(string Token) : IRequestBase<bool>;
    public class AddBlackListTokenCommandHandler : RequestHandlerBase<BlackListToken, AddBlackListTokenCommand, bool>
    {
        public AddBlackListTokenCommandHandler(RequestHandlerBaseParameters<BlackListToken> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddBlackListTokenCommand request, CancellationToken cancellationToken)
        {
            BlackListToken blackListToken= new BlackListToken { Token = request.Token };
            _repository.Add(blackListToken);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
