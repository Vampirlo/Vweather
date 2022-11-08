using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Vweather.OpenWeather
{
    internal class OpenWeather
    {
        public coord coord;
        public weather[] weather;

        public main main;
        [JsonProperty("base")]
        public string Base;
        public double visibility;
        public wind wind;
        public clouds clouds;
        public double dt;
        public sys sys;
        public int id;
        public string name;
        public double cod;
    }
}
