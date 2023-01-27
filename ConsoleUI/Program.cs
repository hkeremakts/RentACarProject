using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EfCarDal efCarDal = new EfCarDal();
            CarManager a = new CarManager(efCarDal);
            //Car car = new Car
            //{
            //    Id = 1,
            //    CarName = "Kerem",
            //    BrandId = 0,
            //    ColorId = 0,
            //    DailyPrice = 1000,
            //    ModelYear = 2004,
            //    Description = "meh"
            //};
            //a.Add(car);
            //a.Delete(1);
            foreach (var item in a.GetAll().Data)
            {
                Console.WriteLine(item.CarName);
            }





            string path = @"c:\temp\MyTest.txt";
            using (FileStream fs = File.Create(path))
            {
            }


        }
    }
}
