using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wManagerProject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root
    {
        public List<Warframe> Warframes { get; set; }
    }
    public class Warframe
    {
        public string name { get; set; }
        public string date { get; set; }
        public int polarized { get; set; }
        public bool OrokinCatalysts { get; set; }
        public bool OrokinReactors { get; set; }
        public bool Exilus { get; set; }
    }

}
