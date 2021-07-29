using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("ModuleRole")]
    public partial class ModuleRole
    {
        [Key]
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty(nameof(PageModule.ModuleRoles))]
        public virtual PageModule Module { get; set; }
        [ForeignKey(nameof(PermissionId))]
        [InverseProperty(nameof(PermissionRoleBase.ModuleRoles))]
        public virtual PermissionRoleBase Permission { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("ModuleRoles")]
        public virtual Role Role { get; set; }
    }
}
