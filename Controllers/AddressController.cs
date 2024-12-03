
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QPD_Middle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<AddressResponse>> GetStandardizedAddress([FromQuery] string address)
        {
            var response = await _addressService.StandardizeAddressAsync(address);
            return Ok(response);
        }
    }
}

