using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yu_gi_oh_website
{
    public class Root
    {
        public Root()
        {
            Data = new List<NormalMonsters>();
        }
        public List<NormalMonsters> Data { get; set; } = null!;
    }
}
