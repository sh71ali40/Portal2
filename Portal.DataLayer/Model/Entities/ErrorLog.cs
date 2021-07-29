using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("ErrorLog")]
    public partial class ErrorLog
    {
        [Key]
        public int EventId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LogDateTime { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
        public string UserMessage { get; set; }
        public int? UserId { get; set; }
    }
}
