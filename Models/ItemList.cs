using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryList.Models
{
    public class ItemList
    {
        public int Id { get; set; }
        public string Items { get; set; }
        public int Quantity { get; set; }
    }
}