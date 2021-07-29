using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("ModuleSetting")]
    public partial class ModuleSetting
    {
        [Key]
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int SettingId { get; set; }
        [Required]
        public string SettingValue { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty(nameof(PageModule.ModuleSettings))]
        public virtual PageModule Module { get; set; }
        [ForeignKey(nameof(SettingId))]
        [InverseProperty(nameof(ModuleDefSetting.ModuleSettings))]
        public virtual ModuleDefSetting Setting { get; set; }
    }
}
