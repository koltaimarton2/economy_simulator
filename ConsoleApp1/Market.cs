using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Market
    {
        public List<Buyer> buyers = new List<Buyer>();
        public List<Seller> sellers = new List<Seller>();

        public Market()
        {
            buyers = new List<Buyer>();
            sellers = new List<Seller>();
        }

        public void Simulate(int rounds)
        {

        }

    }
}
