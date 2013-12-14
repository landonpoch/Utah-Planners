using MongoDB.Driver;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Infrastructure.DAO;

namespace UtahPlanners.Infrastructure.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {

        #region IUnitOfWorkFactory Members

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new PropertiesDB(), new MongoClient());
        }

        #endregion
    }
}
