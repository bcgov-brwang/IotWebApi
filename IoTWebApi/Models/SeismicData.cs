using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTWebApi.Models
{
    public class SeismicData
    {
        public int Id { get; set; }
        public string EventTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Depth { get; set; }
        public double Maganitude { get; set; }
        public string Region { get; set; }
        public bool HasShakeMap { get; set; }

        public string ShakeMapUrl { get; set; }

    }
}
