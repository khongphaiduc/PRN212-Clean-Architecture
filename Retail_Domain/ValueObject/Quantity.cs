using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Domain.ValueObject
{
    public class Quantity
    {
        public int Value { get; set; }
        public Quantity(int value)
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative.", nameof(value));
        }
    }
}
