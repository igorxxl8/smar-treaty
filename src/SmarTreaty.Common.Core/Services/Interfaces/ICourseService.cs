using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Core.Services.Interfaces
{
    public interface ICourseService : IBaseService
    {
        IEnumerable<Course> GetCourses(Expression<Func<Course, bool>> predicate = null,
            Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
            string properties = "");

        Course GetCourse(Guid id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        void DeleteCourse(Guid id);
    }
}