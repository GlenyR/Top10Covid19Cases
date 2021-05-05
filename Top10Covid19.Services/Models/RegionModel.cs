using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top10Covid19.Services.Models
{
    public class RegionModel
    {

        public RegionModel(string name, string iso)
        {
            _name = name.ToUpper();
            _iso = iso.ToUpper(); 

        }

        public RegionModel()
        {
        }

        private readonly string _name;
        private readonly string _iso;

        public string Name { get { return _name; } }
        public string Iso { get  { return _iso; } }

    }
}
