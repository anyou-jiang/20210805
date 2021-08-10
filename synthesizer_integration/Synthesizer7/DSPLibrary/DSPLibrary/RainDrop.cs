using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPLibrary
{
    public class RainDrop
    {
        public float Direction;
        public double Distance;
        public double SourceRadius;
        public double TimeShort;
        public double TimeLong;
        public double TimeHit;

        public RainDrop()
        {

        }


       public static float NextFloat(float min, float max)
        {
           Random random = new Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

    }
}
