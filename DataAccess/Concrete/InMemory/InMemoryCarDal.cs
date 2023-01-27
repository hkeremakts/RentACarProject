//using DataAccess.Abstract;
//using Entities.Abstract;
//using Entities.Concrete;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

//namespace DataAccess.Concrete.InMemory
//{
//    public class InMemoryCarDal : ICarDal
//    {
//        List<Car> _cars;
//        public InMemoryCarDal()
//        {
//            _cars = new List<Car>()
//            {
//                new Car(){Id=1,ColorId=1,BrandId=1,DailyPrice=200,ModelYear=2020,Description="Temiz"},
//                new Car(){Id=2,ColorId=3,BrandId=6,DailyPrice=350,ModelYear=2021,Description="Boyalı"},
//                new Car(){Id=3,ColorId=4,BrandId=4,DailyPrice=450,ModelYear=2020,Description="Vires Arızalı"},
//                new Car(){Id=4,ColorId=3,BrandId=3,DailyPrice=500,ModelYear=2015,Description="Temiz"},
//                new Car(){Id=5,ColorId=3,BrandId=2,DailyPrice=300,ModelYear=2018,Description="Temiz"},
//                new Car(){Id=6,ColorId=2,BrandId=5,DailyPrice=800,ModelYear=2022,Description="Temiz"}
//            };
//        }
//        public void Add(Car car)
//        {
//            _cars.Add(car);
//        }

//        public void Add(IEntity entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(int carId)
//        {
//            Car carToDelete = null;
//            carToDelete = _cars.SingleOrDefault(c => c.Id == carId);
//            _cars.Remove(carToDelete);
//        }

//        public Car Get(Expression<Func<Car, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public List<Car> GetAll()
//        {
//            return _cars;
//        }

//        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public Car GetById(int carId)
//        {
//            Car carById = null;
//            carById = _cars.SingleOrDefault(c => c.Id == carId);
//            return carById;
//        }
//        public void Update(Car car)
//        {
//            Car carToUpdate = null;
//            carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
//            carToUpdate.BrandId = car.BrandId;
//            carToUpdate.ColorId = car.ColorId;
//            carToUpdate.ModelYear = car.ModelYear;
//            carToUpdate.DailyPrice = car.DailyPrice;
//            carToUpdate.Description = car.Description;
//        }

//        public void Update(IEntity entity)
//        {
//            throw new NotImplementedException();
//        }

//        List<IEntity> IEntityRepository.GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        IEntity IEntityRepository.GetById(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
