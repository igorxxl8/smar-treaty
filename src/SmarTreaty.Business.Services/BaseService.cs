using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Core.Services.Interfaces;

namespace SmarTreaty.Business.Services
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IDatabaseWorkUnit Db;

        protected BaseService(IDatabaseWorkUnit db)
        {
            Db = db;
        }

        public void Save()
        {
            Db.Save();
        }
    }
}