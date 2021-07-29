using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("PageModule")]
    public partial class PageModule
    {
        public PageModule()
        {
            ModuleRoles = new HashSet<ModuleRole>();
            ModuleSettings = new HashSet<ModuleSetting>();
        }

        [Key]
        public int ModuleId { get; set; }
        public int PageId { get; set; }
        public int ModuleDefId { get; set; }
        [StringLength(50)]
        public string PaneName { get; set; }
        public bool IsVisible { get; set; }

        [ForeignKey(nameof(ModuleDefId))]
        [InverseProperty("PageModules")]
        public virtual ModuleDef ModuleDef { get; set; }
        [ForeignKey(nameof(PageId))]
        [InverseProperty("PageModules")]
        public virtual Page Page { get; set; }
        [InverseProperty(nameof(ModuleRole.Module))]
        public virtual ICollection<ModuleRole> ModuleRoles { get; set; }
        [InverseProperty(nameof(ModuleSetting.Module))]
        public virtual ICollection<ModuleSetting> ModuleSettings { get; set; }
    }
}
