using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundProgram
{
    public class Discount
    {
        public void CalcDiscount()
        {
            Debugger.Break();

            int initialPrice = 1000;
            int discountInPercentage = 25;

            int discount = discountInPercentage / 100 * initialPrice;
            int price = initialPrice - discount;

            Debug.Assert(price == 750);
        }
    }
}
