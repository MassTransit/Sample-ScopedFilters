namespace WebApi.Filters
{
    using System.Threading.Tasks;
    using MassTransit;


    public class TokenConsumeFilter<T> :
        IFilter<ConsumeContext<T>>
        where T : class
    {
        readonly Token _token;

        public TokenConsumeFilter(Token token)
        {
            _token = token;
        }

        public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var token = context.Headers.Get<string>("Token");

            _token.Value = token;

            return next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}