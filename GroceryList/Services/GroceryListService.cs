using GroceryList.Models;
using GroceryList.Requests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GroceryList.Services
{
    public class GroceryListService : IGroceryListService
    {
        //ADO.Net Manually
        //Create a connection and then open it
        //Create a command has the name of the stored procedures and parameters
        //Execute command and recieve data reader that lets you iterate through rows

        //SELECT ALL ITEMS IN DATABASE
        public List<ItemList> GetAllItems()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryListConnection"].ConnectionString))
            {
                con.Open(); //open connection object-this reaches out and talks to db server

                var cmd = con.CreateCommand(); //builds a command object and hooks it to connection
                cmd.CommandText = "SelectAllItems"; //stored procedure you want to run
                cmd.CommandType = System.Data.CommandType.StoredProcedure; //telling it this is a stored procedure
                //cmd.parameters.add if we had stuff to add

            using(var reader = cmd.ExecuteReader()) //execute commmand and loop through rows
                {
                    var results = new List<ItemList>(); //creating list I want to return


                    while (reader.Read())  //if returns true, therees more results. block of code happens once for each row
                    {
                        results.Add(new ItemList
                        {
                            Id = (int)reader["Id"],
                            Items = (string)reader["Items"],
                            Quantity = (int)reader["Quantity"]

                        });
                    }
                    return results;
                }
            }
        }
        //INSERT NEW ITEM
        public int InsertNewItem (Item_InsertRequest model) 
        {
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryListConnection"].ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "InsertNewItem";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Items", model.Items);
                cmd.Parameters.AddWithValue("@Quantity", model.Quantity);

                SqlParameter outParams = cmd.Parameters.Add("Id", SqlDbType.Int);
                outParams.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                return (int)outParams.Value;

            }
        }
        //UPDATE SINGLE ITEM
        public void UpdateGroceryItem(Item_UpdateRequest model)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryListConnection"].ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "UpdateGroceryItem";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Items", model.Items); //What happens if I leave off @?
                cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                cmd.ExecuteNonQuery();
            }
        }
        //DELETE SINGLE ITEM
        public void DeleteItemsByID(int id)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryListConnection"].ConnectionString))
            {
                con.Open();

                var cmd = con.CreateCommand(); //creating a command object from sql connection
                cmd.CommandText = "DeleteItemsById"; //name of the command were calling
                cmd.CommandType = System.Data.CommandType.StoredProcedure; //command type is stored procedure
                cmd.Parameters.AddWithValue("@Id", id); //passing the parameters that the stored procedure takes
                cmd.ExecuteNonQuery(); //executes the command that is not expecting anything back
            }
        }
        //DELETE ITEMLIST
        public void DeleteAllItems()
        {
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryListConnection"].ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "DeleteAllItems";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }
    }
}