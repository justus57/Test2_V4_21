using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_V4_21
{
    class InvoiceInstance
    {
        private decimal[] serviceCharges;
        private string CompanyName;
        private string customerName;
        private DateTime serviceDate;
        private decimal Rate;


        public InvoiceInstance(string name, DateTime date, decimal[] scharge, decimal rate)
        {
            customerName = name;
            serviceDate = date;
            serviceCharges = scharge;
            Rate = rate;
        }


        public string Name
        {
            get { return customerName; }
        }

        public InvoiceInstance()
        {
            serviceDate = DateTime.Now;
        }

        public string ListServiceCharges()
        {
            string display = "";

            for (int i = 0; i < serviceCharges.Length; i++)
            {
                display += $"{i + 1}: {serviceCharges[i]}\n";
            }

            return display;
        }

        public decimal CalculateTotal(decimal taxRate)
        {
            if (taxRate < 0)
                throw new Exception("Tax Rat cannot be negative");
            decimal total = 0;
            for (int i = 0; i < serviceCharges.Length; i++)
            {
                total += serviceCharges[i];
            }
            decimal tax = total * taxRate;

            decimal finalTotal = tax + total;

            return finalTotal;

        }

    }
}
