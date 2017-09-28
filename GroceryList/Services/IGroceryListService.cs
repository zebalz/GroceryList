using System.Collections.Generic;
using GroceryList.Models;
using GroceryList.Requests;

namespace GroceryList.Services
{
    public interface IGroceryListService
    {
        List<ItemList> GetAllItems();
        void DeleteItemsByID(int id);
        int InsertNewItem(Item_InsertRequest model);
        void UpdateGroceryItem(Item_UpdateRequest model);
        void DeleteAllItems();
    }
}