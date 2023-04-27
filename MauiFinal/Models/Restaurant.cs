using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiFinal.Models
{
    public abstract class Restaurant
    {
        public int id;
        public string name;
        public int Id { get; set; }
        public string Name { get; set; }

        public Restaurant() { }

        public Restaurant(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
