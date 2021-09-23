using Common.HttpResponse;
using Data.Entities;
using Data.Enums;
using MemberService.Dtos;
using NUnit.Framework;
using System;

namespace NUnitTest
{
    public class MemberTesting:BaseTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Member member = new Member
            {
                Email = "KieuMinhDuc@gmail.com",
                Name = "Kieu Minh Duc",
                Password = "123456789",
                Dob = DateTime.Today,
                MobileNumber = "0377398442",
                Gender = Gender.Male,
                EmailOpt= "KieuMinhDuc@gmail.com",
            };
            _context.Add(member);
            _context.SaveChanges();
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        [Test,Order(1)]
        public void Register_Member_With_Valid_Field()
        {
            MemberCreateVM member = new MemberCreateVM
            {
                Email = "KieuMinhDuc1@gmail.com",
                Name = "Kieu Minh Duc",
                Password="123456789",
                Dob=DateTime.Today,
                MobileNumber="0377398442",
                Gender=Gender.Male,
                EmailOptIn= "KieuMinhDuc02@gmail.com"
            };
            var result = _memberService.Register(member);
            Assert.That(result.ResponseCode, Is.EqualTo(ResponseCode.Created));
            _context.ChangeTracker.Clear();

        }
        [Test, Order(2)]
        public void Register_Member_With_Exist_Email()
        {
            MemberCreateVM member = new MemberCreateVM
            {
                Email = "KieuMinhDuc@gmail.com",
                Name = "Kieu Minh Duc",
                Password = "123456789",
                Dob = DateTime.Today,
                MobileNumber = "0377398442",
                Gender = Gender.Male,
                EmailOptIn = "KieuMinhDuc02@gmail.com"
            };
            var result = _memberService.Register(member);
            Assert.That(result.ResponseCode, Is.EqualTo(ResponseCode.BadRequest));
            _context.ChangeTracker.Clear();

        }
        [Test, Order(3)]
        public void Get_Member_By_Exist_Email()
        {
            string email = "KieuMinhDuc@gmail.com";
            var result = _memberService.GetMemberByEmail(email);
            Assert.That(result.ResponseCode, Is.EqualTo(ResponseCode.OK));
            _context.ChangeTracker.Clear();

        }
        [Test, Order(4)]
        public void Get_Member_By_Not_Exist_Email()
        {
            string email = "KieuMinhDuc11111@gmail.com";
            var result = _memberService.GetMemberByEmail(email);
            Assert.That(result.ResponseCode, Is.EqualTo(ResponseCode.BadRequest));
            _context.ChangeTracker.Clear();

        }
        [Test, Order(5)]
        public void Update_Member_With_Valid_Field()
        {
            MemberUpdateVM member = new MemberUpdateVM
            {
                Email = "KieuMinhDuc@gmail.com",
                Name = "Kieu Duc Minh",
                Dob = DateTime.Today,
                MobileNumber = "0377398987",
                Gender = Gender.Male,
                EmailOptIn = "KieuMinhDuc02@gmail.com"
            };
            var result = _memberService.Update(member);
            Assert.That(result.ResponseCode, Is.EqualTo(ResponseCode.OK));
            _context.ChangeTracker.Clear();

        }
        [Test, Order(6)]
        public void Update_Member_With_Not_Exist_Email()
        {
            MemberUpdateVM member = new MemberUpdateVM
            {
                Email = "duckDuck@gmail.com",
                Name = "Kieu Duc Minh",
                Dob = DateTime.Today,
                MobileNumber = "0377398442",
                Gender = Gender.Male,
                EmailOptIn = "KieuMinhDuc02@gmail.com"
            };
            var result = _memberService.Update(member);
            Assert.That(result.ResponseCode, Is.EqualTo(ResponseCode.BadRequest));
            _context.ChangeTracker.Clear();

        }
    }
}