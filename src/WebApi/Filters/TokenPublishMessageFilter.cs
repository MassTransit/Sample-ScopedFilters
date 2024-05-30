using System.Threading.Tasks;
using MassTransit;
using WebApi.Contracts;

namespace WebApi.Filters;

public class TokenPublishMessageFilter :
    IFilter<PublishContext<CheckInventory>>
{
    public Task Send(PublishContext<CheckInventory> context, IPipe<PublishContext<CheckInventory>> next)
    {
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}