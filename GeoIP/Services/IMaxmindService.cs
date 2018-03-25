using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxMind.GeoIP2.Responses;

namespace GeoIP.Services
{
    public interface IMaxmindService
    {
        CountryResponse Country(string IP);

        CityResponse City(string IP);

        AsnResponse ASN(string IP);
    }
}
