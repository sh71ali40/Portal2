using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("PermissionRoleModuleDef")]
    public partial class PermissionRoleModuleDef
    {
        [Key]
        public int Id { get; set; }
        public int ModuleDefId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey(nameof(ModuleDefId))]
        [InverseProperty("PermissionRoleModuleDefs")]
        public virtual ModuleDef ModuleDef { get; set; }
        [ForeignKey(nameof(PermissionId))]
        [InverseProperty(nameof(PermissionRoleBase.PermissionRoleModuleDefs))]
        public virtual PermissionRoleBase Permission { get; set; }
    }
}
