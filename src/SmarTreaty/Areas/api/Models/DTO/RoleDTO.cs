using System;
using System.Collections.Generic;

namespace SmarTreaty.Areas.api.Models.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Guid> UsersId { get; set; }
    }
}