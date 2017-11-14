using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryList.Requests
{
    public class Item_InsertRequest
    {
        [Required]
        public string Items { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}