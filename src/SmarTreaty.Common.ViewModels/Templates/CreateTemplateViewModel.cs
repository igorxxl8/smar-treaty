using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Common.ViewModels.Templates
{
    public class CreateTemplateViewModel
    {
        public string Source { get; set; }
        public string Name { get; set; }
        public bool Verified { get; set; }
        public string Abi { get; set; }
        public string ByteCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
