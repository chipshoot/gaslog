using NoteRepository.Dal.NH.Infrastructure;
using NoteRepository.DomainEntity;

namespace NoteRepository.Dal.NH.Mappings
{
    public class UserMap : EntityMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.AccountName);
            Map(x => x.BirthDay);
            Map(x => x.Password);
            Map(x => x.Salt);
            Map(x => x.IsActivated);
            Map(x => x.Description);
        }
    }
}