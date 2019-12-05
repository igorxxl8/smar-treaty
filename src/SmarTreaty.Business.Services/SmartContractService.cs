using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Services.Interfaces;
using System;

namespace SmarTreaty.Business.Services
{
    public class SmartContractService : BaseService, ISmartContractService
    {
        public SmartContractService(IDatabaseWorkUnit db) : base(db)
        {
        }
    }
}
