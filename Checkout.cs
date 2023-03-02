using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPG_MiniProject
{
    class Checkout
    {
        private float fltTotalCost;
        public float TotalCost
        {
            get { return fltTotalCost; }
        }
        public virtual void CalTotalCost(float itemcost)
        {
            fltTotalCost = 0;
            fltTotalCost = fltTotalCost + (itemcost);
        }
    }
    class Membership : Checkout
    {
        private int intDisc = 20;
        public override void CalTotalCost(float itemcost)
        {
            itemcost = itemcost * (1 - intDisc / 100f);
            base.CalTotalCost(itemcost);
            
        }
    }
}
