using System.ComponentModel.DataAnnotations;

namespace DomainProject.DomainModels
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Permissions { get; set; }
    }

}
