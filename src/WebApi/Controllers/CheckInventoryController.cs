namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("[controller]")]
    public class CheckInventoryController :
        ControllerBase
    {
        readonly IRequestClient<CheckInventory> _client;

        public CheckInventoryController(IRequestClient<CheckInventory> client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string sku)
        {
            Response<InventoryStatus> response = await _client.GetResponse<InventoryStatus>(new {sku});

            return Ok(response.Message);
        }
    }
}