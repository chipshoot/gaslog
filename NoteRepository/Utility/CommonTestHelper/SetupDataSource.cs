using Moq;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Misc;
using NoteRepository.Common.Utility.Validation;
using NoteRepository.Core.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteRepository.Common.TestHelper
{
    public static class SetupDataSource
    {
        #region constructor

        static SetupDataSource()
        {
            CreateLookup();
            CreateRepositories();
            CreateLookupRepo();
        }

        #endregion constructor

        #region public properties

        public static List<Note> Notes { get; set; }

        public static List<NoteCatalog> NoteCatalogs { get; set; }

        public static List<User> Users { get; set; }

        public static List<Email> Emails { get; set; }

        public static List<Phone> Phones { get; set; }

        public static List<ContactInfoCatalog> ContactCatalogs { get; set; }

        public static List<Tag> Tags { get; set; }

        public static List<NoteRender> Renders { get; set; }

        public static List<AutoMobile> Cars { get; set; }

        public static IRepository<Note> NoteRepo { get; set; }

        public static IRepository<User> UserRepo { get; set; }

        public static IRepository<Email> EmailRepo { get; set; }

        public static IRepository<Phone> PhoneRepo { get; set; }

        public static IRepository<Tag> TagRepo { get; set; }

        public static IRepository<NoteCatalog> NoteCatalogRepo { get; set; }

        public static IRepository<NoteRender> RenderRepo { get; set; }

        public static IRepository<ContactInfoCatalog> ContactCatalogRepo { get; set; }

        public static ILookupRepository LookupRepository { get; set; }

        #endregion public properties

        #region public methods

        public static IDateTimeAdapter GetDateTimeAdapter(string currentDateTime)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(currentDateTime), currentDateTime);

            var today = Convert.ToDateTime(currentDateTime);
            var adapterMock = new Mock<IDateTimeAdapter>();
            adapterMock.Setup(t => t.UtcNow).Returns(today.ToUniversalTime());
            adapterMock.Setup(t => t.GetNextDateForDay(It.IsAny<DateTime>(), It.IsAny<DayOfWeek>(), It.IsAny<int>()))
                .Returns(
                    (DateTime startDate, DayOfWeek desired, int numberToSkip) =>
                    {
                        var c = (int)startDate.DayOfWeek;
                        var d = (int)desired;
                        var n = (7 - c + d);

                        var dayToAdd = ((n > 7) ? n % 7 : n) + (7 * numberToSkip);
                        return startDate.AddDays(dayToAdd);
                    });

            adapterMock.Setup(t => t.AddBusinessDays(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(

                (DateTime startDate, int days) =>
                {
                    {
                        if (days == 0)
                        {
                            return startDate;
                        }

                        if (startDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            startDate = startDate.AddDays(2);
                            days -= 1;
                        }
                        else if (startDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            startDate = startDate.AddDays(1);
                            days -= 1;
                        }

                        startDate = startDate.AddDays(days / 5 * 7);
                        var extraDays = days % 5;

                        if ((int)startDate.DayOfWeek + extraDays > 5)
                        {
                            extraDays += 2;
                        }

                        return startDate.AddDays(extraDays);
                    }
                });

            adapterMock.Setup(t => t.GetCurrentWeekDateForDay(It.IsAny<DateTime>(), It.IsAny<DayOfWeek>())).Returns(
                (DateTime startDate, DayOfWeek desiredDay) =>
                {
                    var daysToDesiredDay = (int)desiredDay - (int)startDate.DayOfWeek;
                    var result = startDate.AddDays(daysToDesiredDay);

                    return result;
                });
            return adapterMock.Object;
        }

        #endregion public methods

        #region private methods

        private static void CreateLookup()
        {
            ContactCatalogs = new List<ContactInfoCatalog>
            {
                new ContactInfoCatalog {Id = 1, Name = "Work Email", Description = "Email used for working"},
                new ContactInfoCatalog {Id = 2, Name = "Work Phone", Description = "Phone used for working"},
                new ContactInfoCatalog {Id = 3, Name = "Home Email", Description = "Email used for home"},
                new ContactInfoCatalog {Id = 4, Name = "Home Phone", Description = "Phone used for home"},
            };

            Users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Jack",
                    LastName = "Fang",
                    AccountName = "jfang",
                    BirthDay = new DateTime(1967, 3, 13),
                    IsActivated = true,
                    Password = "password",
                    Salt = "password",
                    Description = "testing user"
                }
            };

            Emails = new List<Email>
            {
                new Email
                {
                    Id = 1,
                    Address = "jfang@hadrian-inc.com",
                    Catalog = ContactCatalogs.FirstOrDefault(c => c.Id == 1),
                    Owner = Users.FirstOrDefault(u => u.Id == 1),
                    IsPrimary = false,
                    IsActivated = true,
                    Description = "working email"
                }
            };

            Phones = new List<Phone>
            {
                new Phone
                {
                    Id = 1,
                    AreaCode = 1,
                    Number = "905-333-0300",
                    Extension = 2058,
                    Catalog = ContactCatalogs.FirstOrDefault(c => c.Id == 2),
                    Owner = Users.FirstOrDefault(u => u.Id == 1),
                    IsActivated = true,
                    Description = "working phone"
                },

                new Phone
                {
                    Id = 2,
                    AreaCode = 1,
                    Number = "905-206-9886",
                    Extension = null,
                    Catalog = ContactCatalogs.FirstOrDefault(c => c.Id == 4),
                    Owner = Users.FirstOrDefault(u => u.Id == 1),
                    IsActivated = true,
                    Description = "working phone"
                }
            };

            Tags = new List<Tag>
            {
                new Tag {Id = 1, Name = "Normal note", IsActivated = true, Description = "normal note without any special"},
                new Tag {Id = 2, Name = "Special note", IsActivated = true, Description = "normal note without some special"}
            };

            NoteCatalogs = new List<NoteCatalog>
            {
                new NoteCatalog {Id = 1, Name = "Normal Note", Description = "Normal note without any special meaning"},
                new NoteCatalog {Id = 2, Name = "Gas Log", Description = "Gas log information of the car"}
            };

            Renders = new List<NoteRender>
            {
                new NoteRender {Id = 1, Name = "DefaultRender", NameSpace = "NoteRepository.Render"},
                new NoteRender {Id = 2, Name = "GasLogRender", NameSpace = "NoteRepository.Render"}
            };

            Notes = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Subject = "Test note of normal note",
                    Content = "This note is just a normal note without any meaning",
                    Author = Users.FirstOrDefault(u => u.Id == 1),
                    Catalog = NoteCatalogs.FirstOrDefault(c => c.Id == 1),
                    Render = Renders.FirstOrDefault(r => r.Id == 1),
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                },
                new Note
                {
                    Id = 2,
                    Subject = "Test note of Gas log",
                    Content = "<GasLog><Date>2016-05-17</Date><Distance><Unit>KM</Unit><Value>400</Value></Distance><Gas><Unit>LT</Unit><Value>40</Value></Gas><Price><Currency>CAD</Currency><Value>1.09</Value></Price><Total><Currency>CAD</Currency><Value>43.6</Value></Total><Discounts><Discount><Value><Currency>CAD</Currency><Value>0.8</Value></Value><Program>Credit card</Program></Discount></Discounts></GasLog>",
                    Author = Users.FirstOrDefault(u => u.Id == 1),
                    Catalog = NoteCatalogs.FirstOrDefault(c => c.Id == 2),
                    Render = Renders.FirstOrDefault(r => r.Id == 2),
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                }
            };
        }

        private static void CreateRepositories()
        {
            // note
            var noteRepoMock = GetMockRepo(Notes);
            NoteRepo = noteRepoMock.Object;

            // User
            var userRepoMock = GetMockRepo(Users);
            UserRepo = userRepoMock.Object;

            // Email
            var emailRepoMock = GetMockRepo(Emails);
            EmailRepo = emailRepoMock.Object;

            // Phone
            var phoneRepoMock = GetMockRepo(Phones);
            PhoneRepo = phoneRepoMock.Object;

            // NoteCatalog
            var ncRepoMock = GetMockRepo(NoteCatalogs);
            NoteCatalogRepo = ncRepoMock.Object;

            // contact information catalog
            var ccRepoMock = GetMockRepo(ContactCatalogs);
            ContactCatalogRepo = ccRepoMock.Object;

            // note render
            var nrRepoMock = GetMockRepo(Renders);
            RenderRepo = nrRepoMock.Object;

            // tags
            var tagRepoMock = GetMockRepo(Tags);
            TagRepo = tagRepoMock.Object;

            //
        }

        private static Mock<IRepository<T>> GetMockRepo<T>(ICollection<T> list) where T : Entity, new()
        {
            var repoMock = new Mock<IRepository<T>>();
            repoMock.Setup(r => r.FindEntities()).Returns(() => list.Clone().AsQueryable());
            repoMock.Setup(r => r.FindEntityById(It.IsAny<int>())).Returns((int id) => list.Clone().FirstOrDefault(t => t.Id == id));
            repoMock.Setup(r => r.Update(It.IsAny<T>()))
                                    .Returns((T u) => u)
                                    .Callback((T u) =>
                                    {
                                        var oldRec = list.FirstOrDefault(a => a.Id == u.Id);
                                        if (oldRec == null)
                                        {
                                            return;
                                        }

                                        list.Remove(oldRec);
                                        list.Add(u);
                                    });
            repoMock.Setup(r => r.Add(It.IsAny<T>()))
                                   .Returns((T u) => u)
                                   .Callback((T u) =>
                                   {
                                       u.Id = list.Count + 1;
                                       list.Add(u);
                                   });

            return repoMock;
        }

        private static void CreateLookupRepo()
        {
            var lookupMock = new Mock<ILookupRepository>();
            lookupMock.Setup(l => l.FindEntities<Note>()).Returns(() => Notes.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<User>()).Returns(() => Users.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<Email>()).Returns(() => Emails.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<Phone>()).Returns(() => Phones.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<NoteRender>()).Returns(() => Renders.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<NoteCatalog>()).Returns(() => NoteCatalogs.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<ContactInfoCatalog>()).Returns(() => ContactCatalogs.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<Tag>()).Returns(() => Tags.Clone().AsQueryable());

            LookupRepository = lookupMock.Object;
        }

        #endregion private methods
    }
}