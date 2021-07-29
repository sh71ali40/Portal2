using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("ObjLog")]
    public partial class ObjLog
    {
        [Key]
        [Column("objLogId")]
        public int ObjLogId { get; set; }
        [Column("objId")]
        public int ObjId { get; set; }
        [Column("objType")]
        [StringLength(50)]
        public string ObjType { get; set; }
        [Column("logType")]
        public byte LogType { get; set; }
        [Column("logDate", TypeName = "datetime")]
        public DateTime LogDate { get; set; }
        [Column("logUser")]
        public int LogUser { get; set; }
        [Column("logComment")]
        [StringLength(255)]
        public string LogComment { get; set; }
        [Column("logModuleId")]
        public int LogModuleId { get; set; }
        [Column("logIP")]
        [StringLength(50)]
        public string LogIp { get; set; }
        [Column("logHash")]
        public string LogHash { get; set; }
    }
}
