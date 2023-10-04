using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Api
{
    public class TaxpayerResponse
    {
        public long vunp { get; set; }
        public string vnaimk { get; set; }
        public string vnaimp { get; set; }
        public int nmns { get; set; }
        public string vmns { get; set; }
        public string? dreg { get; set; }
        public string? ckodsost { get; set; }
        public string? dlikv { get; set; }
        public string? vlikv { get; set; }
        public string? vpadres { get; set; }
        public string vkods { get; set; }
        public bool? active { get; set; }
    }
}
