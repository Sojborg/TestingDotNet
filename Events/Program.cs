using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var stock = new Stock("THPW");
            stock.Price = 27.10M;

            stock.PriceChanged += (sender, eventArgs) =>
            {
                if ((eventArgs.NewPrice - eventArgs.LastPrice)/eventArgs.LastPrice > 0.1M)
                    Console.WriteLine("Alert, 10% stock price increase!");
            };

            stock.Price = 31.59M;
            Console.ReadLine();
        }

        public class PriceChangedEventArgs : EventArgs
        {
            public readonly decimal LastPrice;
            public readonly decimal NewPrice;

            public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
            {
                LastPrice = lastPrice;
                NewPrice = newPrice;
            }
        }

        public class Stock
        {
            string symbol;
            decimal price;

            public Stock(string symbol)
            {
                this.symbol = symbol;
            }

            public event EventHandler<PriceChangedEventArgs> PriceChanged;

            protected virtual void OnPriceChanged(PriceChangedEventArgs e)
            {
                if (PriceChanged != null) PriceChanged(this, e);

            }

            public decimal Price
            {
                get { return price; }
                set
                {
                    if (price == value) return;
                    decimal oldPrice = price;
                    price = value;
                    OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
                }
            }
        }
    }
}
