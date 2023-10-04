using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Api
{
    public interface ITaxpayerApiClient
    {
        string ApiUrl { set; }
        Task<List<TaxpayerResponse>> SearchTaxpayer(string name);
    }
}