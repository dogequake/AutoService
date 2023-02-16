using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace SumbitFor16.Models
{
    public partial class ServicePhoto 
    {
        public byte[] MainImage
        { get 
            {
                return File.ReadAllBytes($"{Directory.GetCurrentDirectory()}\\..\\..\\Assets\\Images\\{PhotoPath}");
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
                DurationInSeconds = DurationInSeconds.TrimEnd().TrimStart();
                string[] vs = DurationInSeconds.Split(new char[] { ' ' });
                int minut = Convert.ToInt32(vs[0]);
                if (vs[1] == "мин.")
                {
                    minut = Convert.ToInt32(vs[0]) / 60;
                }
                else 
                {

                }
                if (Discount == 0 || Discount == null)
                {
                    return $"{Cost:N2} рублей за {minut} минут";
                }
                else
                {
                    return $"{CostWithDiscount:N2} рублей за {minut} минут";
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

        public string AdminControlsVisibility 
        {
            get 
            {
                //1-admin 2-user
                if (App.CurrentClient.RoleId == 1)
                {
                    return "Visible";
                }
                else 
                {
                    return "Collapsed";
                }
            }
        }
    }
}
