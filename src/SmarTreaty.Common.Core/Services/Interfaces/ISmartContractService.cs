using SmarTreaty.Common.DomainModel;
using System.Threading.Tasks;

namespace SmarTreaty.Common.Core.Services.Interfaces
{
    public interface ISmartContractService : IBaseService
    {
        Task DeployContract(SmartContract contract, params object[] values);
    }
}
