using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using SmarTreaty.Areas.api.Models.DTO;
using SmarTreaty.Enums;
using SmarTreaty.Helpers;
using Novacode;
using SmarTreaty.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Areas.api.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesApiController : ApiController
    {
        private readonly ICourseService _courseService;
        private readonly ITrainerService _trainerService;
        private readonly ICourseGroupService _courseGroupService;

        public CoursesApiController(ICourseService courseService, ITrainerService trainerService, ICourseGroupService courseGroupService)
        {
            _courseService = courseService;
            _trainerService = trainerService;
            _courseGroupService = courseGroupService;
        }

        [Route("")]
        // GET: api/courses
        public IHttpActionResult GetAllCourses(Guid? courseGroups = null, string search = null, int? type = null, int? arrangement = null)
        {
            var predicate = PredicateBuilder.True<Course>();
            if (courseGroups != null && courseGroups != Guid.Empty)
            {
                predicate = predicate.And(c => c.CourseGroupId == courseGroups);
            }

            if (!string.IsNullOrEmpty(search))
            {
                predicate = predicate.And(c => c.Name.Contains(search));
            }

            if (type != null)
            {
                predicate = predicate.And(c => c.TypeCode == type);
            }

            if (arrangement != null)
            {
                predicate = predicate.And(c => c.PlanningMethodCode == arrangement);
            }

            var courses = _courseService
                .GetCourses(null, properties: "Trainers")
                .Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsNew = c.IsNew,
                    PlanningMethodCode = (CoursePlanningMethodEnum)c.PlanningMethodCode,
                    Description = c.Description,
                    TypeCode = (CourseTypeEnum)c.TypeCode,
                    TrainersId = c.Trainers.Select(t => t.Id),
                    CourseGroupId = c.CourseGroupId
                });

            return Ok(courses);
        }

        // GET: api/courses/5
        [Route("{id}")]
        [ResponseType(typeof(CourseDTO))]
        public IHttpActionResult GetCourse(Guid id)
        {
            var course = _courseService.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseDto = new CourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                IsNew = course.IsNew,
                PlanningMethodCode = (CoursePlanningMethodEnum)course.PlanningMethodCode,
                TypeCode = (CourseTypeEnum)course.TypeCode,
                TrainersId = course.Trainers.Select(t => t.Id),
                CourseGroupId = course.CourseGroupId
            };

            return Ok(courseDto);
        }

        [Route("")]
        // PUT: api/CoursesTextApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(CourseDTO courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                Id = courseDto.Id,
                Description = courseDto.Description,
                IsNew = courseDto.IsNew,
                Name = courseDto.Name,
                PlanningMethodCode = (int)courseDto.PlanningMethodCode,
                TypeCode = (int)courseDto.TypeCode,
                CourseGroupId = courseDto.CourseGroupId,
                Trainers = courseDto.TrainersId.Select(id => _trainerService.GetTrainer(id)).ToList()
            };

            _courseService.UpdateCourse(course);
            _courseService.Save();
            

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/trainers/{remove}")]
        // PUT: api/courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrainerForCourse(Guid id, bool? remove, TrainerDTO trainerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = _courseService.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            if (trainerDto == null)
            {
                return NotFound();
            }

            var trainer = _trainerService.GetTrainer(trainerDto.Id);
            if (trainer == null)
            {
                return NotFound();
            }

            if (remove != null && remove.Value)
            {
                course.Trainers.Remove(trainer);
            }

            else
            {
                course.Trainers.Add(trainer);
            }

            _courseService.UpdateCourse(course);
            _courseService.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("")]
        // POST: api/courses
        [ResponseType(typeof(CourseDTO))]
        public IHttpActionResult PostCourse(CourseDTO courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _courseService.AddCourse(new Course
            {
                Description = courseDto.Description,
                IsNew = courseDto.IsNew,
                Name = courseDto.Name,
                PlanningMethodCode = (int)courseDto.PlanningMethodCode,
                TypeCode = (int)courseDto.TypeCode,
                CourseGroupId = courseDto.CourseGroupId,
                Trainers = courseDto.TrainersId.Select(id =>_trainerService.GetTrainer(id)).ToList()
            });
            
            _courseService.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}")]
        // DELETE: api/courses/5
        [ResponseType(typeof(CourseDTO))]
        public IHttpActionResult DeleteCourse(Guid id)
        {
            var course = _courseService.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseDto = new CourseDTO
            {
                CourseGroupId = course.CourseGroupId,
                Description = course.Description,
                Id = id,
                IsNew = course.IsNew,
                Name = course.Name,
                PlanningMethodCode = (CoursePlanningMethodEnum) course.PlanningMethodCode,
                TypeCode = (CourseTypeEnum) course.TypeCode,
                TrainersId = course.Trainers.Select(t => t.Id)
            };

            _courseService.DeleteCourse(course);
            _courseService.Save();

            return Ok(courseDto);
        }

        // GET: api/courses/docx
        [Route("docx/{isNew}")]
        public HttpResponseMessage GetDataAsDocx(bool isNew)
        {
            var courseGroups = _courseGroupService.GetCourseGroups(properties: "Courses");

            var document = DocX.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Courses.docx");

            var headLineFormat = new Formatting
            {
                FontFamily = new FontFamily("Times New Roman"),
                Size = 16D,
                Position = 12,
                Bold = true
            };

            var paragraphFormat = new Formatting
            {
                FontFamily = new FontFamily("Times New Roman"),
                Size = 14D,
                Bold = true,
                Italic = true
            };

            var actual = isNew ? "actual " : "";
            var headlineText = $"List of {actual}trainings on {DateTime.Now}";
            document.InsertParagraph(headlineText, false, headLineFormat);

            foreach (var group in courseGroups)
            {
                var table = document.AddTable(1, 4);
                table.Alignment = Alignment.center;
                table.Design = TableDesign.TableGrid;

                table.Rows[0].Cells[0].Paragraphs[0].Append("Training name");
                table.Rows[0].Cells[1].Paragraphs[0].Append("Type");
                table.Rows[0].Cells[2].Paragraphs[0].Append("Schedule");
                table.Rows[0].Cells[3].Paragraphs[0].Append("Trainers");

                foreach (var course in group.Courses)
                {
                    if (isNew && !course.IsNew)
                    {
                        continue;
                    }

                    var row = table.InsertRow();
                    var descRow = table.InsertRow();
                    var count = table.RowCount;
                    table.MergeCellsInColumn(3, count - 2, count - 1);
                    row.Cells[0].Paragraphs[0].Append(course.Name);
                    row.Cells[1].Paragraphs[0].Append(((CourseTypeEnum)course.TypeCode).ToString());
                    row.Cells[2].Paragraphs[0].Append(((CoursePlanningMethodEnum)course.PlanningMethodCode).ToString());
                    var trainers = course.Trainers.ToList();

                    foreach (var t in trainers)
                    {
                        var trainer = t.User;
                        var name = trainer.FirstName + " " + trainer.LastName;
                        row.Cells[3].Paragraphs[0].Append(name + "\n");
                    }

                    descRow.MergeCells(0,2);
                    descRow.Cells[0].Paragraphs[0].Append(course.Description);
                }

                document.InsertParagraph();
                document.InsertParagraph(group.Name + " trainings", false, paragraphFormat);
                document.InsertTable(table);
            }

            var ms = new MemoryStream();
            document.SaveAs(ms);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ms.ToArray())
            };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Courses.docx"
            };
            return response;
        }
    }
}
