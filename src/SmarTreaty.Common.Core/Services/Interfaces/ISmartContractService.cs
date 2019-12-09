using SmarTreaty.Common.DomainModel;
using System.Threading.Tasks;

namespace SmarTreaty.Common.Core.Services.Interfaces
{
    public interface ISmartContractService : IBaseService
    {
        Task DeployContract(Template template, string privateKey, params object[] values);
        Task<string> CompileContract(string source);
    }
}
