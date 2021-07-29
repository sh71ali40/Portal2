using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("Page")]
    public partial class Page
    {
        public Page()
        {
            InverseParent = new HashSet<Page>();
            PageModules = new HashSet<PageModule>();
            PageRoles = new HashSet<PageRole>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [Required]
        [StringLength(50)]
        public string TemplateName { get; set; }

        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(Page.InverseParent))]
        public virtual Page Parent { get; set; }
        [InverseProperty(nameof(Page.Parent))]
        public virtual ICollection<Page> InverseParent { get; set; }
        [InverseProperty(nameof(PageModule.Page))]
        public virtual ICollection<PageModule> PageModules { get; set; }
        [InverseProperty(nameof(PageRole.Page))]
        public virtual ICollection<PageRole> PageRoles { get; set; }
    }
}
