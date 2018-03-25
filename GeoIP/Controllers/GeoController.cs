using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeoIP.Requests;
using GeoIP.Responses;
using GeoIP.Services;
using MaxMind.GeoIP2.Responses;

namespace GeoIP.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GeoController : Controller
    {
        private IMaxmindService maxmindService;

        public GeoController(IMaxmindService maxmind)
        {
            maxmindService = maxmind;
        }

        [HttpGet]
        public IActionResult City(GeoRequest request)
        {
            if (! ModelState.IsValid) {
                return ApiResponse<string>.ValidationError(request.IP);
            }

            var city = maxmindService.City(request.IP);

            return new ApiResponse<CityResponse>() { Payload = city };
        }

        [HttpGet]
        public IActionResult Country(GeoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponse<string>.ValidationError(request.IP);
            }

            var country = maxmindService.Country(request.IP);

            return new ApiResponse<CountryResponse>() { Payload = country };
        }

        [HttpGet]
        public IActionResult ASN(GeoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponse<string>.ValidationError(request.IP);
            }

            var asn = maxmindService.ASN(request.IP);

            return new ApiResponse<AsnResponse>() { Payload = asn };
        }
    }
}