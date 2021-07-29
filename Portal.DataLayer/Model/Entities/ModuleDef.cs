using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("ModuleDef")]
    public partial class ModuleDef
    {
        public ModuleDef()
        {
            ModuleDefRoles = new HashSet<ModuleDefRole>();
            ModuleDefSettings = new HashSet<ModuleDefSetting>();
            PageModules = new HashSet<PageModule>();
            PermissionRoleModuleDefs = new HashSet<PermissionRoleModuleDef>();
        }

        [Key]
        public int ModuleDefId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string HomeController { get; set; }
        public bool Enabled { get; set; }

        [InverseProperty(nameof(ModuleDefRole.ModuleDef))]
        public virtual ICollection<ModuleDefRole> ModuleDefRoles { get; set; }
        [InverseProperty(nameof(ModuleDefSetting.ModuleDef))]
        public virtual ICollection<ModuleDefSetting> ModuleDefSettings { get; set; }
        [InverseProperty(nameof(PageModule.ModuleDef))]
        public virtual ICollection<PageModule> PageModules { get; set; }
        [InverseProperty(nameof(PermissionRoleModuleDef.ModuleDef))]
        public virtual ICollection<PermissionRoleModuleDef> PermissionRoleModuleDefs { get; set; }
    }
}
