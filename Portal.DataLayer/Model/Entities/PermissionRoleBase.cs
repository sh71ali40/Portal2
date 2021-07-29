using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("PermissionRoleBase")]
    public partial class PermissionRoleBase
    {
        public PermissionRoleBase()
        {
            ModuleDefRoles = new HashSet<ModuleDefRole>();
            ModuleRoles = new HashSet<ModuleRole>();
            PermissionRoleModuleDefs = new HashSet<PermissionRoleModuleDef>();
        }

        [Key]
        public int PermissionId { get; set; }
        [Required]
        [StringLength(250)]
        public string PermissionName { get; set; }
        public bool IsManager { get; set; }

        [InverseProperty(nameof(ModuleDefRole.Permission))]
        public virtual ICollection<ModuleDefRole> ModuleDefRoles { get; set; }
        [InverseProperty(nameof(ModuleRole.Permission))]
        public virtual ICollection<ModuleRole> ModuleRoles { get; set; }
        [InverseProperty(nameof(PermissionRoleModuleDef.Permission))]
        public virtual ICollection<PermissionRoleModuleDef> PermissionRoleModuleDefs { get; set; }
    }
}
