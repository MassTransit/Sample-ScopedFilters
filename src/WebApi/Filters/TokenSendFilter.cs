namespace WebApi.Filters
{
    using System.Threading.Tasks;
    using MassTransit;


    public class TokenSendFilter<T> :
        IFilter<SendContext<T>>
        where T : class
    {
        readonly Token _token;

        public TokenSendFilter(Token token)
        {
            _token = token;
        }

        public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            if (!string.IsNullOrWhiteSpace(_token.Value))
                context.Headers.Set("Token", _token.Value);

            return next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}