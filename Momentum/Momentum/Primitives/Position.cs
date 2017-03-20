using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Primitives
{
    public class Position
    {       
        public double Latitude { get; }
        public double Longitude { get; }

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Position(string latlng)
        {
            if (string.IsNullOrEmpty(latlng))
                throw new ArgumentOutOfRangeException();

            string[] position = latlng.Split(',');

            if (position.Length != 2)
                throw new ArgumentOutOfRangeException();

            Latitude = Convert.ToDouble(position[0]);
            Longitude = Convert.ToDouble(position[1]);
        }
    }
}
