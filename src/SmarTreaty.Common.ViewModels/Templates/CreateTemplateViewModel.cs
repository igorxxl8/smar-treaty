using SmarTreaty.Common.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.Common.ViewModels.Templates
{
    public class CreateTemplateViewModel
    {
        [Required]
        [Display(Name = "Source")]
        public string Source { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool Verified { get; set; }
        public string Abi { get; set; }
        public string ByteCode { get; set; }
        public string ErrorMessage { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
