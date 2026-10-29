using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem_Kristiqn_11g
{
    internal class Article
    {
        public string Name { get; set; }
        public int ProductionYear { get; set; }
        public int ExpirationMonths { get; set; }
        public int Quantity { get; set; }

        public Article(string name, int productionYear, int expirationMonths, int quantity)
        {
            Name = name;
            ProductionYear = productionYear;
            ExpirationMonths = expirationMonths;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Име: {Name} | Година: {ProductionYear} | Срок: {ExpirationMonths} мес. | Количество: {Quantity} бр.";
        }

        public string ToFileString()
        {
            return $"{Name};{ProductionYear};{ExpirationMonths};{Quantity}";
        }
    }
}
