using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SumbitFor16.Models
{
    public partial class ServicePhoto 
    {
        public string MainImage
        { get 
            {
                return "..\\..\\Assets\\Images\\" + PhotoPath;
            } }
    }

    public partial class Service
    {
        public string DiscountText
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "";
                else
                    return $"* скидка {Discount * 100}%";
            }
        }

        public double CostWithDiscount 
        {
            get 
            {
                if (Discount == 0 || Discount == null)
                {
                    return (double)Cost;
                }
                else 
                {
                    var costWithDiscount = (double)Cost * (1.00 - Discount);
                    return costWithDiscount.Value;
                }
            }
        }

        public string TotalCost
        {
            get
            {
                string[] vs = DurationInSeconds.Split(new char[] { ' ' });
                if (vs[1] == "мин.") 
                {
                    int minut = Convert.ToInt32(vs[0]) * 60;
                }
                if (Discount == 0 || Discount == null)
                {
                    return $"{Cost:N2} рублей за {} минут";
                }
                else
                {
                    return $"{CostWithDiscount:N2} рублей за {} минут";
                }
            }
        }

        public Visibility DiscountVisibility 
        {
            get 
            {
                if (Discount == 0 || Discount == null)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string BackColor 
        {
            get 
            {
                if (Discount == 0 || Discount == null)
                {
                    return "#FFFFE1";
                }
                else
                {
                    return "#D1FFD1";
                }
            }
        }
    }
}
