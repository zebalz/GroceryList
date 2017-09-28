using GroceryList.Requests;
using GroceryList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroceryList.Controllers
{
    [RoutePrefix("api/GroceryList")] //setting up route prefix to controller
    public class GroceryListController : ApiController
    {
        readonly IGroceryListService groceryListService;

        public GroceryListController(IGroceryListService groceryListService)
        {
            this.groceryListService = groceryListService;
        }
        //GET ALL ITEMS
        [Route("getItems"), HttpGet]
        public HttpResponseMessage GetAllItems() //Questions for Dan: 1.Why use Response Message? 2. Why does Request.CreateResponse Work?
        {
            return Request.CreateResponse(HttpStatusCode.OK, groceryListService.GetAllItems());
        }
        //INSERT A NEW ITEM TO LIST
        [Route("newItem"),HttpPost]
        public object InsertNewItem(Item_InsertRequest model)
        {
            return groceryListService.InsertNewItem(model); //best practice to make an itemsresponse object
        }
        //UPDATE SINGLE ITEM IN LIST
        [Route("{id:int}"),HttpPut]
        public void UpdateGroceryItem(Item_UpdateRequest model)
        {
             groceryListService.UpdateGroceryItem(model);
        }
        //DELETE SINGLE ITEM
        [Route("{id:int}"),HttpDelete]
        public object DeleteItemsById(int id)
        {
            groceryListService.DeleteItemsByID(id);
            return "You Deleted Something";
        }
        //DELETE ALL ITEMS
        [Route("DeleteAll"),HttpDelete]
        public void DeleteAllItems()
        {
            groceryListService.DeleteAllItems();
        }
    }
}
