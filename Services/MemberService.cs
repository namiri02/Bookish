using Bookish.Models.Member;

namespace Bookish.Services;

public static class MemberService
{
    public static IList<Member> GetMembers()
    {
        using (var context = new LibraryContext())
        {
            var members = context.Member;
            return members.ToList();
        }
    }

    public static void AddMember(string name, string email)
    {
        using (var context = new LibraryContext())
        {
            var std = new Member()
            {
                Name = name,
                EmailAddress = email
            };
            context.Member.Add(std);

            context.SaveChanges();
        }
    }


    public static void EditMember(int memberId, string name, string email)
    {
        using (var context = new LibraryContext())
        {
            var member = context.Member.First(m => m.MemberId == memberId);

            member.Name = name;
            member.EmailAddress = email;

            context.SaveChanges();
        }
    }

    public static void DeleteMember(int memberId)
    {
        using (var context = new LibraryContext())
        {
            var member = context.Member.First(m => m.MemberId == memberId);
            context.Remove(member);

            context.SaveChanges();
        }
        
    }

}