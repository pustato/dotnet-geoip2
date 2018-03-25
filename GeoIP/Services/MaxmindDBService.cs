using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxMind.Db;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;

namespace GeoIP.Services
{
    public class MaxmindDBService : IMaxmindService
    {
        public DatabaseReader CitiesReader { get; set; }

        public DatabaseReader CoutriesReader { get; set; }

        public DatabaseReader ASNReader { get; set; }

        public void InitCitiesDB(string filePath)
        {
            CitiesReader = new DatabaseReader(filePath);
        }

        public void InitCountriesDB(string filePath)
        {
            CoutriesReader = new DatabaseReader(filePath);
        }

        public void InitASNDB(string filePath)
        {
            ASNReader = new DatabaseReader(filePath);
        }

        public CityResponse City(string IP)
        {
            if (null == CitiesReader)
            {
                throw new Exception("Cities database is not ready");
            }

            return CitiesReader.City(IP);
        }

        public CountryResponse Country(string IP)
        {
            if (null == CoutriesReader)
            {
                throw new Exception("Countries database is not ready");
            }

            return CoutriesReader.Country(IP);
        }

        public AsnResponse ASN(string IP)
        {
            if (null == ASNReader)
            {
                throw new Exception("ASN database is not ready");
            }

            return ASNReader.Asn(IP);
        }
    }
}
