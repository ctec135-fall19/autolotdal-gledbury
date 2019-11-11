using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;



namespace AutoLotDAL.Models
{
    using AutoLotDAL.Models;
    public class Car
    {
        public int CarId { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string PetName { get; set; }
    }

    public list<Car> GetAllInventory()
    {
        OpenConnection();
        //this will hold the records
        List<Car> inventory = new List<Car>();

        //Prep command object
        string sql = "Select * From Inventory";
        using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
        {
            command.CommandType = CommandType.Text;
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior
                .CloseConnection);
            while (dataReader.Read())
            {
                inventory.Add(new Car
                {
                    CarId = (int)dataReader["CarId"],
                    Color = (string)dataReader["Color"],
                    Make = (string)dataReader["Make"],
                    PetName = (string)dataReader["PetName"]
                });
            }
            dataReader.Close();
        }
        return inventory;
    }

    public Car GetCar(int id)
    {
        OpenConnection();
        Car car = null;
        string sql = $"Select * From Inventory where CarID = {id}";
        using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
        {
            command.CommandType = CommandType.Text;
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior
                .CloseConnection);
            while (dataReader.Read())
            {
                car = new Car
                {
                    CarId = (int)dataReader["CarId"],
                    Color = (string)dataReader["Color"],
                    Make = (string)dataReader["Make"],
                    PetName = (string)dataReader["PetName"]
                };
            }
            dataReader.Close();
        }
        return car;
    }
}
