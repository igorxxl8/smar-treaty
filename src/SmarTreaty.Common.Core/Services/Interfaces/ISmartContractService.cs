using SmarTreaty.Common.DomainModel;
using System.Threading.Tasks;

namespace SmarTreaty.Common.Core.Services.Interfaces
{
    public interface ISmartContractService : IBaseService
    {
        Task DeployContract(SmartContract contract, User user, string contractName, string privateKey, params object[] values);
        Task<string> CompileContract(string source);
        void SaveTemplate(SmartContract smartContract);
    }
}
