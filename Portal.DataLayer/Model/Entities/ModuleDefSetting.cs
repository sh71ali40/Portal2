using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("ModuleDefSetting")]
    public partial class ModuleDefSetting
    {
        public ModuleDefSetting()
        {
            ModuleSettings = new HashSet<ModuleSetting>();
        }

        [Key]
        public int SettingId { get; set; }
        public int ModuleDefId { get; set; }
        [Required]
        [StringLength(250)]
        public string SettingName { get; set; }
        public string SettingValues { get; set; }
        public string DefaultValue { get; set; }
        public string SettingHelp { get; set; }
        public string NonModularValue { get; set; }

        [ForeignKey(nameof(ModuleDefId))]
        [InverseProperty("ModuleDefSettings")]
        public virtual ModuleDef ModuleDef { get; set; }
        [InverseProperty(nameof(ModuleSetting.Setting))]
        public virtual ICollection<ModuleSetting> ModuleSettings { get; set; }
    }
}
