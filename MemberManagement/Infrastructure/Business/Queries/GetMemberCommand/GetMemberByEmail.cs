using MediatR;

namespace Infrastructure.Business.Queries.GetMemberCommand
{
    public static class GetMemberByEmail
    {
        public record Query(string email):IRequest<>
    }
}
