using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Portal.DataLayer.Model.Entities
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string UserPass { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsLocked { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastPasswordChangeDate { get; set; }
        public long? LastModifierUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastModificationDateTime { get; set; }
        public int? FailedPasswordAttemptCount { get; set; }
        [StringLength(12)]
        public string Mobile { get; set; }
        public bool HasChangedPassword { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime JoinDate { get; set; }
        [Required]
        [Column("JoinIP")]
        [StringLength(15)]
        public string JoinIp { get; set; }
        [Column("LastIP")]
        [StringLength(15)]
        public string LastIp { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? MobileConfirmed { get; set; }
        public Guid? Picture { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedDateTime { get; set; }
        public long? DeleterUserId { get; set; }
        [StringLength(10)]
        public string SentCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SentCodeExpirationDateTime { get; set; }

        [InverseProperty(nameof(UserRole.User))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
