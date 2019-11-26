using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.DomainModel;
using SmarTreaty.Core.Services.Interfaces;

namespace SmarTreaty.Business.Services
{
    public class CourseGroupsService : BaseService, ICourseGroupService
    {
        public CourseGroupsService(IDatabaseWorkUnit db) : base(db)
        {
        }

        public IEnumerable<CourseGroup> GetCourseGroups(Expression<Func<CourseGroup, bool>> predicate = null,
            Func<IQueryable<CourseGroup>, IOrderedQueryable<CourseGroup>> orderBy = null,
            string properties = "")
        {
            if (predicate == null && orderBy == null && properties == "")
            {
                return Db.CourseGroups.GetAll();
            }

            return Db.CourseGroups.Get(predicate, orderBy, properties);
        }

        public CourseGroup GetCourseGroup(Guid id)
        {
            return Db.CourseGroups.GetById(id);
        }

        public void AddCourseGroup(CourseGroup courseGroup)
        {
            Db.CourseGroups.Add(courseGroup);
        }

        public void UpdateCourseGroup(CourseGroup courseGroup)
        {
            Db.CourseGroups.Update(courseGroup);
        }

        public void DeleteCourseGroup(CourseGroup courseGroup)
        {
            Db.CourseGroups.Remove(courseGroup);
        }

        public void DeleteCourseGroup(Guid id)
        {
            Db.CourseGroups.Remove(id);
        }
    }
}