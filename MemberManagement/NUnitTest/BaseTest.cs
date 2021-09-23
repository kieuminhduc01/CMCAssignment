using Data.DBContext;
using MemberRepository.Repository;
using MemberService.Services;
using Microsoft.EntityFrameworkCore;

namespace NUnitTest
{
    public class BaseTest
    {
        protected DataContext _context;
        protected IMemberService _memberService;
        private IMemberRepository _memberRepository;
        public BaseTest()
        {
            DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                                    .UseInMemoryDatabase(databaseName: "Assignment1")
                                    .Options;
            _context = new DataContext(dbContextOptions);
            _memberRepository = new MemberRepositoryImp(_context);
            _memberService = new MemberServiceImp(_memberRepository);
        }
    }
}
