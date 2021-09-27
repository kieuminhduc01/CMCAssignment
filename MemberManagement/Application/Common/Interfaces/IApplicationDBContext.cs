using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
