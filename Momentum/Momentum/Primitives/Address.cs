using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Primitives
{
    public class Address
    {
        public string City { get; set; }

        public string PostalCode { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        //public override string ToString()
        //{
        //    return City + ", " + CountryName;
        //}
    }
}
