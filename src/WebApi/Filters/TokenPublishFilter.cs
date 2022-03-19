namespace WebApi.Filters
{
    using System.Threading.Tasks;
    using MassTransit;


    public class TokenPublishFilter<T> :
        IFilter<PublishContext<T>>
        where T : class
    {
        readonly Token _token;

        public TokenPublishFilter(Token token)
        {
            _token = token;
        }

        public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
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