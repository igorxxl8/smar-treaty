using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Core.Services.Interfaces
{
    public interface ICourseGroupService : IBaseService
    {
        IEnumerable<CourseGroup> GetCourseGroups(Expression<Func<CourseGroup, bool>> predicate = null,
            Func<IQueryable<CourseGroup>, IOrderedQueryable<CourseGroup>> orderBy = null,
            string properties = "");

        CourseGroup GetCourseGroup(Guid id);
        void AddCourseGroup(CourseGroup courseGroup);
        void UpdateCourseGroup(CourseGroup courseGroup);
        void DeleteCourseGroup(CourseGroup courseGroup);
        void DeleteCourseGroup(Guid id);
    }
}