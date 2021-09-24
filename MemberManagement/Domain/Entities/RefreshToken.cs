using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime DeathTime { get; set; }
        [ForeignKey(nameof(Email))]
        public Member Member { get; set; }
    }
}
