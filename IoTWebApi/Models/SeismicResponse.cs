using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTWebApi.Models
{
    public class SeismicResponse
    {
        public List<SeismicData> Data { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
