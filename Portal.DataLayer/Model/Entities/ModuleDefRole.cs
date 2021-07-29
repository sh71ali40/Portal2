using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("ModuleDefRole")]
    public partial class ModuleDefRole
    {
        [Key]
        public int Id { get; set; }
        public int ModuleDefId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey(nameof(ModuleDefId))]
        [InverseProperty("ModuleDefRoles")]
        public virtual ModuleDef ModuleDef { get; set; }
        [ForeignKey(nameof(PermissionId))]
        [InverseProperty(nameof(PermissionRoleBase.ModuleDefRoles))]
        public virtual PermissionRoleBase Permission { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("ModuleDefRoles")]
        public virtual Role Role { get; set; }
    }
}
