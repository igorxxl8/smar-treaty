using SmarTreaty.Areas.api.Models.DTO;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace CMSys.Areas.api.Controllers
{
    [RoutePrefix("api/roles")]
    public class RolesApiController : ApiController
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RolesApiController(IRoleService roleService, IUserService usrService)
        {
            _roleService = roleService;
            _userService = usrService;
        }

        [Route("")]
        // GET: api/roles
        public IHttpActionResult GetRoles()
        {
            var roles = _roleService.GetRoles()
                .Select(r => new RoleDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    UsersId = r.Users.Select(u => u.Id)
                });

            return Ok(roles);
        }

        [Route("{id}")]
        // GET: api/roles/5
        [ResponseType(typeof(RoleDTO))]
        public IHttpActionResult GetRole(int id)
        {
            var role = _roleService.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            var roleDto = new RoleDTO
            {
                Id = id,
                Name = role.Name,
                UsersId = role.Users.Select(u => u.Id)
            };
            return Ok(roleDto);
        }

        // PUT: api/roles/5
        [Route("")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(RoleDTO roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                Users = roleDto.UsersId.Select(id => _userService.GetUser(id)).ToList()
            };

            _roleService.UpdateRole(role);
            _roleService.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/users/{remove}")]
        // PUT: api/courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserForRole(int id, bool? remove, UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = _roleService.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            if (userDto == null)
            {
                return NotFound();
            }

            var user = _userService.GetUser(userDto.Id);
            if (user == null)
            {
                return NotFound();
            }

            if (remove != null && remove.Value)
            {
                role.Users.Remove(user);
            }

            else
            {
                role.Users.Add(user);
            }

            _roleService.UpdateRole(role);
            _roleService.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/roles
        [Route("")]
        [ResponseType(typeof(RoleDTO))]
        public IHttpActionResult PostRole(RoleDTO roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _roleService.AddRole(new Role
            {
                Name = roleDto.Name,
                Users = roleDto.UsersId.Select(id => _userService.GetUser(id)).ToList()
            });
            _roleService.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/roles/5
        [Route("{id}")]
        [ResponseType(typeof(RoleDTO))]
        public IHttpActionResult DeleteRole(int id)
        {
            var role = _roleService.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            var roleDto = new RoleDTO
            {
                Id = id,
                Name = role.Name,
                UsersId = role.Users.Select(u => u.Id)
            };

            _roleService.DeleteRole(role);
            _roleService.Save();

            return Ok(roleDto);
        }
    }
}