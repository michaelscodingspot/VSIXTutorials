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
        public int GetPriceAfterDiscount(int initialPrice, int discountInPercent)
        {
            Debugger.Break();
            int discount = CalculateDiscount(initialPrice, discountInPercent);

            int price = initialPrice - discount;
            return price;
        }













        private int CalculateDiscount(int price, int percent)
        {
            int discount = percent * price / 100 ;
            return discount;
        }
    }
}
