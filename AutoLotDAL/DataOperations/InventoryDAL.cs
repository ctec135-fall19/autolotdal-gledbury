using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace AutoLotDAL.DataOperations
{
    public class InventoryDAL
    {
        private readonly string _connectionString;
        public InventoryDAL() : this(@"Data Source - (localdb)\mssqllocaldb;
        Integrated Security=True;Initial Catalog-AutoLot")
        {
        }
        public InventoryDAL(string connectionString)
            => _connectionString = connectionString;

        private SqlConnection _sqlConnection = null;
        private void OpenConnection()
        {
            _sqlConnection = new SqlConnection { ConnectionString =
                _connectionString }; _sqlConnection.Open();
        }
        private void CloseConnection()
        { 
                {
                    if (_sqlConnection?.State != ConnectionState.Closed)
                    {
                    _sqlConnection?.Close();
                    }
                }
        }

        public void InsertAuto(string color, string make, string petName)
        {
            OpenConnection();
            //format and execute SQL statement.
            string sql = $"Insert Into Inventory (Make, Color, PetName) " +
                $"Values ('{make}', '{color}', '{petName}')";
            //execute using our connection
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            CloseConnection();
        }

        public void InsertAuto(Car car)
        {
            OpenConnection();
            //format and execute SQL statement
            string sql = "Insert Into Inventory (Make, Color, PetName) Values "
                + $"('{car.Make}', '{car.Color}', '{car.petName}')";

            // execute using our connection
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.CommandType = CommandType.Text;
                command.BeginExecuteNonQuery();
            }
            CloseConnection(); 
        }

        public void DeleteCar (int id)
        {
            OpenConnection();
            // Get ID of car to delete, then do so
            string sql = $"Delete from Inventory where CarId = '{id}'";
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    command.BeginExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Sorry! That car is on order!", ex);
                    throw error;
                }
            }
            CloseConnection();
        }

        public void UpdateCarPetName(int id, string newPetName)
        {
            OpenConnection();
            // get ID of car to modify the pet name
            string sql = $"Update Inventory Set PetName = '{newPetName}' Where" +
                $"CarId = '{id}'";
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }
}
