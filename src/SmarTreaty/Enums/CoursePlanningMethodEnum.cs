using System.ComponentModel.DataAnnotations;

namespace SmarTreaty.Enums
{
    public enum CoursePlanningMethodEnum
    {
        [Display(Name="Scheduled")]
        Scheduled,
        [Display(Name = "On demand")]
        OnDemand
    }
}