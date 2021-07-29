using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            ModuleDefRoles = new HashSet<ModuleDefRole>();
            ModuleRoles = new HashSet<ModuleRole>();
            PageRoles = new HashSet<PageRole>();
            UserRoles = new HashSet<UserRole>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [InverseProperty(nameof(ModuleDefRole.Role))]
        public virtual ICollection<ModuleDefRole> ModuleDefRoles { get; set; }
        [InverseProperty(nameof(ModuleRole.Role))]
        public virtual ICollection<ModuleRole> ModuleRoles { get; set; }
        [InverseProperty(nameof(PageRole.Role))]
        public virtual ICollection<PageRole> PageRoles { get; set; }
        [InverseProperty(nameof(UserRole.Role))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
