using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("PageRole")]
    public partial class PageRole
    {
        [Key]
        public int Id { get; set; }
        public int PageId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(PageId))]
        [InverseProperty("PageRoles")]
        public virtual Page Page { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("PageRoles")]
        public virtual Role Role { get; set; }
    }
}
