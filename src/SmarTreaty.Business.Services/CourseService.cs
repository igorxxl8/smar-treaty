using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;

namespace SmarTreaty.Business.Services
{
    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IDatabaseWorkUnit db) : base(db)
        {
        }

        public IEnumerable<Course> GetCourses(Expression<Func<Course, bool>> predicate = null,
            Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
            string properties = "")
        {
            if (predicate == null && orderBy == null && properties == "")
            {
                return Db.Courses.GetAll();
            }

            return Db.Courses.Get(predicate, orderBy, properties);
        }

        public Course GetCourse(Guid id)
        {
            return Db.Courses.GetById(id);
        }

        public void AddCourse(Course course)
        {
            Db.Courses.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            Db.Courses.Update(course);
        }

        public void DeleteCourse(Course course)
        {
            Db.Courses.Remove(course);
        }

        public void DeleteCourse(Guid id)
        {
            Db.Courses.Remove(id);
        }
    }
}