
using System;

namespace Portal.Infrustructure.ViewModel
{
    public class UserDto
    {
 
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string UserPass { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationDateTime { get; set; }
        public int? FailedPasswordAttemptCount { get; set; }
        public bool HasChangedPassword { get; set; }
        public DateTime JoinDate { get; set; }
        public string JoinIp { get; set; }
        public string LastIp { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? MobileConfirmed { get; set; }
        public Guid? Picture { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public long? DeleterUserId { get; set; }
        public string SentCode { get; set; }
        public DateTime? SentCodeExpirationDateTime { get; set; }
        public int RoleId { get; set; }
    }
}
