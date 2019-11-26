using System;
using System.Collections.Generic;
using SmarTreaty.Enums;

namespace SmarTreaty.Areas.api.Models.DTO
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public CourseTypeEnum TypeCode { get; set; }
        public CoursePlanningMethodEnum PlanningMethodCode { get; set; }
        public IEnumerable<Guid> TrainersId { get; set; }
        public Guid CourseGroupId { get; set; }
    }
}