using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.DataOperations;
using AutoLotDAL.Models;

namespace AutoLotClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InventoryDAL dal = new InventoryDAL();
            var list = dal.GetAllInventory();
            Console.WriteLine(" ********** All Cars **********1");
            Console.WriteLine("CarId\tMake\tColor\tPet Name");
            foreach (var itm in list)
            {
                Console.WriteLine($"{itm.CarId}\t{itm.Make}\t{itm.Color}" +
                    $"\t{itm.PetName}");
            }
            Console.WriteLine();
            var car = dal.GetCar(list.OrderBy(x => x.color).Select(x => x.CarId)
                .First());
            Console.WriteLine(" ********** First Car By Color **********");
            Console.WriteLine("CarId\tMake\tColor\tPet Name");
            Console.WriteLine($"{car.CarId}\t{car.Make}\t{car.Color}\t" +
                $"{car.PetName}");

            try
            {
                dal.DeleteCar(5);
                Console.WriteLine("Car deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
            dal.InsertAuto(new Car {Color="Blue", Make="Pilot", PetName="TowMonster" });
            list = dal.GetAllInventory();
            var newCar = list.First(x => x.PetName == "TowMonster");
            Console.WriteLine(" ********** New Car ********** ");
            Console.WriteLine("CarId\tMake\tColor\tPet Name");
            Console.WriteLine($"{newCar.CarId}\t{newCar.Make}\t{newCar.Color}\t" +
                $"{newCar.PetName}");
            dal.DeleteCar(newCar.Id);
            var petName = DllNotFoundException.LookUpPetName(car.CarId);
            Console.WriteLine(" ********** New Car ********** ");
            Console.WriteLine($"Car pet name; {petName}");
            Console.Write("Press enter to continue...");
            Console.ReadLine();
        }
    }
}
