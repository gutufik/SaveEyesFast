using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveEyesFast.Data
{
    public partial class Agent
    {
        public int SellsCount => ProductSales.Count(x => x.SaleDate.Year == DateTime.Now.Year );

        public int Discount => ProductSales.Sum(x => x.ProductCount * x.Product.MinCostForAgent) < 10000 ? 0 :
                                ProductSales.Sum(x => x.ProductCount * x.Product.MinCostForAgent) < 50000 ? 5 :
                                ProductSales.Sum(x => x.ProductCount * x.Product.MinCostForAgent) < 150000 ? 10 :
                                ProductSales.Sum(x => x.ProductCount * x.Product.MinCostForAgent) < 500000 ? 20 : 25;

        public string Background => Discount >= 10 ? "LightGreen" : "";


    }
}
