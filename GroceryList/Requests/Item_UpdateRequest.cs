using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryList.Requests
{
    public class Item_UpdateRequest : Item_InsertRequest
    {
        [Required]
        public int Id { get; set; }
    }
}