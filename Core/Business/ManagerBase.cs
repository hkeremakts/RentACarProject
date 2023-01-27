using Core.DataAccess;
using Core.Entities.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
    public class ManagerBase<T> : IService<T> where T : class, IEntity, new()
    {
        IEntityRepository<T> _entityDal;
        public ManagerBase(IEntityRepository<T> iEntityDal)
        {
            _entityDal = iEntityDal;
        }
        public IResult Add(T entity)
        {
            _entityDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(T entity)
        {
            _entityDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<T>> GetAll()
        {
            return new SuccessDataResult<List<T>>(_entityDal.GetAll());
        }

        public IDataResult<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
