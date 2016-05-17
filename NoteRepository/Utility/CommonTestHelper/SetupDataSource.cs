using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.MeasureUnit;
using NoteRepository.Common.Utility.Misc;
using NoteRepository.Common.Utility.Validation;
using NoteRepository.Core.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace NoteRepository.Common.TestHelper
{
    public static class SetupDataSource
    {
        #region constructor

        static SetupDataSource()
        {
            CreateLookup();
            CreateNotes();
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

        private static void CreateNotes()
        {
            // material
            var metal = Materials.FirstOrDefault(m => m.Name == "Metal");
            var plastic = Materials.FirstOrDefault(m => m.Name == "Solid Plastic");
            var locker = Materials.FirstOrDefault(m => m.Name == "Locker");
            var arch = Materials.FirstOrDefault(m => m.Name == "Arch");

            // stock
            var stockBoth = Branches;
            var stockNone = new List<BranchPlant>();

            // color
            var customMetal = ProductColors.FirstOrDefault(c => c.Code == "800");
            var notPainted = ProductColors.FirstOrDefault(c => c.Id == 2);
            var almondColor = ProductColors.FirstOrDefault(c => c.Id == 59);
            var graniteColor = ProductColors.FirstOrDefault(c => c.Id == 76);
            var whiteColor = ProductColors.FirstOrDefault(c => c.Id == 3);
            var stainlessSteel = ProductColors.FirstOrDefault(c => c.Id == 86);
            var rigidizedSteel = ProductColors.FirstOrDefault(c => c.Id == 87);
            var tricornBlackColor = ProductColors.FirstOrDefault(c => c.Id == 84);
            var bronzeMetallic = ProductColors.FirstOrDefault(c => c.Id == 74);
            var whiteMetalColor = ProductColors.FirstOrDefault(c => c.Id == 42);
            var linenColor = ProductColors.FirstOrDefault(c => c.Id == 43);
            var almondPlasticColor = ProductColors.FirstOrDefault(c => c.Id == 4);
            var oysterSpeckle = ProductColors.FirstOrDefault(c => c.Id == 12);
            var lightGreyPlasticColor = ProductColors.FirstOrDefault(c => c.Id == 8);
            var lightGreyMetalColor = ProductColors.FirstOrDefault(c => c.Code == "535");

            // lead time
            var quickShip = LeadTimeSchedules.FirstOrDefault(l => l.Id == 1);
            var noStock45Week = LeadTimeSchedules.FirstOrDefault(l => l.Name == "NoStock(4.5Week)");
            var noStock25Week = LeadTimeSchedules.FirstOrDefault(l => l.Name == "NoStock(2.5Week)");
            var quickShipUs = LeadTimeSchedules.FirstOrDefault(l => l.Name == "QuickShipUs");
            var noStock35Week = LeadTimeSchedules.FirstOrDefault(l => l.Name == "NoStock(3.5Week)");
            var noStock75Week = LeadTimeSchedules.FirstOrDefault(l => l.Name == "NoStock(7.5Week)");
            var noStock65Week = LeadTimeSchedules.FirstOrDefault(l => l.Name == "NoStock(6.5Week)");
            var noStock30Week = LeadTimeSchedules.FirstOrDefault(l => l.Name == "NoStock(3.0Week)");

            // market
            var sellBoth = ProductMarketSchedules.FirstOrDefault(m => m.Id == 1);
            var discontinued = ProductMarketSchedules.FirstOrDefault(m => m.Id == 4);

            // series
            var empLockerSeries = ProductSeries.FirstOrDefault(s => s.Name == "Emperor");

            // designType
            var empLockerDesign = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "EmperorLocker");
            var divDesign = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "Divided");

            Products = new List<Product>
            {
                new Product { Id = 49, PartNumber = "510023-535", PartFamily = "510023", Description = "Door 23\" x 58\"", Description2 = "Light Grey", Width = Dimension.FromInch(23), Height = Dimension.FromInch(58), Weight = Weight.FromPounds(27), IsPainted = true, Color = lightGreyMetalColor, Price = 104.00M, Cost = 30.37M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Door 23\" x 58\"", StockBranches = stockBoth },
                new Product { Id = 83, PartNumber = "510023-8SP", PartFamily = "510023", Description = "Door 23\" x 58\"", Description2 = "Door 23\" x 58\"", Width = Dimension.FromInch(23), Height = Dimension.FromInch(58), Weight = Weight.FromPounds(27), IsPainted = true, Color = customMetal, Price = 104.00M, Cost = 30.37M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Door 23\" x 58\"", StockBranches = stockNone },
                new Product { Id = 218, PartNumber = "510023-SPL", PartFamily = "510023", Description = "Door 23\" x 58\"", Description2 = "Door 23\" x 58\"", Width = Dimension.FromInch(23), Height = Dimension.FromInch(58), Weight = Weight.FromPounds(27), IsPainted = true, Color = notPainted, Price = 104.00M, Cost = 30.37M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 65536, LeadTime = noStock25Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Door 23\" x 58\"", StockBranches = stockNone },
                new Product { Id = 236, PartNumber = "510024-603", PartFamily = "510024", Description = "Door 24\" x 58\"", Description2 = "Door 24\" x 58\"", Width = Dimension.FromInch(24), Height = Dimension.FromInch(58), Weight = Weight.FromPounds(28.001), IsPainted = true, Color = almondColor, Price = 104.00M, Cost = 31.06M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = quickShipUs, MarketInfo = sellBoth, IsActivated = true, Comment = "Door 24\" x 58\"", StockBranches = stockBoth },
                new Product { Id = 733, PartNumber = "510032-S18", PartFamily = "510032", Description = "Door 32\" x 58\"", Description2 = "Door 32\" x 58\"", Width = Dimension.FromInch(32), Height = Dimension.FromInch(58), Weight = Weight.FromPounds(35.999), IsPainted = true, Color = graniteColor, Price = 127.05M, Cost = 45.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 65536, LeadTime = noStock25Week, MarketInfo = discontinued, IsActivated = false, Comment = "Door 32\" x 58\"", StockBranches = stockNone },
                new Product { Id = 10525, PartNumber = "520357-S00", PartFamily = "520357", Description = "SP Bracket One Ear x 55\"", Description2 = "SP Bracket One Ear x 55\"", Width = Dimension.FromInch(0), Height = Dimension.FromInch(55), Weight = Weight.FromPounds(3.999), IsPainted = false, Color = whiteColor, Price = 52.00M, Cost = 18.45M, IsSeparateSale = true, ProductMaterial = plastic, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock25Week, MarketInfo = discontinued, IsActivated = false, Comment = "SP Bracket One Ear x 55\"", StockBranches = stockNone },
                new Product { Id = 20898, PartNumber = "541003-80D", PartFamily = "541003-80D", Description = "HR DH Pilaster 3\" x 82\"", Description2 = "Pil 3\" x 82\" H/R D/H", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(10), IsPainted = true, Color = customMetal, Price = 40.00M, Cost = 12.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 4, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pil 3\" x 82\" H/R D/H", StockBranches = stockNone },
                new Product { Id = 20899, PartNumber = "541003-80N", PartFamily = "541003-80N", Description = "HR NH Pilaster 3\" x 82\"", Description2 = "Pil 3\" x 82\" H/R N/H", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(10), IsPainted = true, Color = customMetal, Price = 40.00M, Cost = 12.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 2, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pil 3\" x 82\" H/R N/H", StockBranches = stockNone },
                new Product { Id = 20917, PartNumber = "541003-902", PartFamily = "541003", Description = "HR DH SS Pilaster 3\" x 82\"", Description2 = "Pil 3\" x 82\" H/R D/H", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(10), IsPainted = true, Color = stainlessSteel, Price = 140.00M, Cost = 12.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 4, LeadTime = noStock35Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pil 3\" x 82\" H/R D/H", StockBranches = stockNone },
                new Product { Id = 20918, PartNumber = "541003-90R", PartFamily = "541003", Description = "Pilaster 3\" x 82\" H/R", Description2 = "Pilaster 3\" x 82\" H/R", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(10), IsPainted = true, Color = rigidizedSteel, Price = 175.00M, Cost = 12.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pilaster 3\" x 82\"", StockBranches = stockBoth },
                new Product { Id = 20920, PartNumber = "541003-9R2", PartFamily = "541003", Description = "HR DH Emb Pilaster 3\" x 82\"", Description2 = "Pil 3\" x 82\" H/R D/H", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(10), IsPainted = true, Color = rigidizedSteel, Price = 175.00M, Cost = 12.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 4, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pil 3\" x 82\" H/R D/H", StockBranches = stockNone },
                new Product { Id = 20921, PartNumber = "541003-9SP", PartFamily = "541003", Description = "Pilaster 3\" x 82\" H/R", Description2 = "Pilaster 3\" x 82\" H/R", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(10), IsPainted = true, Color = stainlessSteel, Price = 140.00M, Cost = 12.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock35Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pilaster 3\" x 82\" H/R", StockBranches = stockNone },
                new Product { Id = 20922, PartNumber = "541003-9SR", PartFamily = "541003", Description = "Pilaster 3\" x 82\" H/R", Description2 = "Pilaster 3\" x 82\" H/R", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(10), IsPainted = true, Color = rigidizedSteel, Price = 175.00M, Cost = 12.77M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pilaster 3\" x 82\" H/R", StockBranches = stockNone },
                new Product { Id = 21000, PartNumber = "541003M-901", PartFamily = "541003M", Description = "HR NH SS Mas Pilaster 3\" x 82\"", Description2 = "Pilaster 3\" x 82\" H/R No Hinge", Width = Dimension.FromInch(3), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(13.001), IsPainted = true, Color = stainlessSteel, Price = 161.00M, Cost = 17.39M, IsSeparateSale = true, HardwareMaterial = null, OptionValue = 3, IsActivated = true, Comment = "Pilaster 3\" x 82\" H/R No Hinge", ProductMaterial = metal, LeadTime = noStock45Week, MarketInfo = sellBoth, StockBranches = stockNone },
                new Product { Id = 22482, PartNumber = "541008-D33", PartFamily = "541008-D", Description = "Pil 8\" x 82\" H/R D/H", Description2 = "Pil 8\" x 82\" H/R D/H", Width = Dimension.FromInch(8), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(18.001), IsPainted = true, Color = tricornBlackColor, Price = 55.65M, Cost = 18.75M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 4, LeadTime = noStock25Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pil 8\" x 82\" H/R D/H", StockBranches = stockNone },
                new Product { Id = 22654, PartNumber = "541008-N15", PartFamily = "541008-N", Description = "Pil 8\" x 82\" H/R N/H", Description2 = "Pil 8\" x 82\" H/R N/H", Width = Dimension.FromInch(8), Height = Dimension.FromInch(82), Weight = Weight.FromPounds(18.001), IsPainted = true, Color = bronzeMetallic, Price = 55.65M, Cost = 18.75M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 2, LeadTime = noStock25Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Pil 8\" x 82\" H/R N/H", StockBranches = stockNone },
                new Product { Id = 73375, PartNumber = "560357-S00", PartFamily = "560357", Description = "Channel 1.75\"x56.75\" Pilaster", Description2 = "Channel 1.75\"x56.75\" Pilaster", Width = Dimension.FromInch(1.75), Height = Dimension.FromInch(56.75), Weight = Weight.FromPounds(3.999), IsPainted = true, Color = whiteMetalColor, Price = 0M, Cost = 4.47M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock25Week, MarketInfo = discontinued, IsActivated = false, Comment = "Channel 1.75\"x56.75\" Pilaster", StockBranches = stockNone },
                new Product { Id = 77796, PartNumber = "739976-S04", PartFamily = "739976", Description = "Emp Dr S/T 24\" Div Rt", Description2 = "Emp Dr S/T 24\" Div Rt", Width = Dimension.FromInch(24), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = false, Color = linenColor, Price = 0M, Cost = 0M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = null, MarketInfo = discontinued, IsActivated = false, Comment = "Emp Dr S/T 24\" Div Rt", StockBranches = stockNone },
                new Product { Id = 142907, PartNumber = "D10023-S03", PartFamily = "D10023", Description = "AR Door 23\" x 59\"", Description2 = "AR Door 23\" x 59\"", Width = Dimension.FromInch(23), Height = Dimension.FromInch(59), Weight = Weight.FromPounds(42.999), IsPainted = true, Color = almondPlasticColor, Price = 249M, Cost = 71M, IsSeparateSale = true, ProductMaterial = arch, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock75Week, MarketInfo = discontinued, IsActivated = false, Comment = "AR Door 23\" x 59\"", StockBranches = stockNone },
                new Product { Id = 144281, PartNumber = "D41003-185J", PartFamily = "D41003", Description = "AR Pilaster 3\" x 79\"", Description2 = "AR Pilaster 3\" x 79\"", Width = Dimension.FromInch(3), Height = Dimension.FromInch(79), Weight = Weight.FromPounds(7), IsPainted = true, Color = oysterSpeckle, Price = 64M, Cost = 18.05M, IsSeparateSale = true, ProductMaterial = arch, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock75Week, MarketInfo = discontinued, IsActivated = false, Comment = "AR Pilaster 3\" x 79\"", StockBranches = stockNone },
                new Product { Id = 146653, PartNumber = "X10036-S35", PartFamily = "X10036", Description = "SP Door 36\" x 55\"", Description2 = "SP Door 36\" x 55\"", Width = Dimension.FromInch(36), Height = Dimension.FromInch(55), Weight = Weight.FromPounds(81), IsPainted = true, Color = lightGreyPlasticColor, Price = 302M, Cost = 86.17M, IsSeparateSale = true, ProductMaterial = plastic, HardwareMaterial = null, OptionValue = 65536, LeadTime = noStock25Week, MarketInfo = discontinued, IsActivated = false, Comment = "SP Door 36\" x 55\"", StockBranches = stockNone },

                // locker
                new Product { Id = 1, PartNumber = "307500-1", PartFamily = "307500-1", Description = "Touch-up Paint - White" , Description2 = "White", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = false, Color = whiteMetalColor, Price = 0M, Cost = 1.71M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = quickShip, MarketInfo = sellBoth, IsActivated = true, Comment = ".6 Fl Oz Brush In Cap", StockBranches = stockNone },
                new Product { Id = 11, PartNumber = "307535-1", PartFamily = "307535-1", Description = "Touch-up Paint - Light Grey", Description2 = "Light Grey", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = false, Color = lightGreyMetalColor, Price = 0M, Cost = 1.70M, IsSeparateSale = true, ProductMaterial = metal, HardwareMaterial = null, OptionValue = 0, LeadTime = quickShip, MarketInfo = sellBoth, IsActivated = true, Comment = ".6 Fl Oz Brush In Cap", StockBranches = stockNone },
                new Product { Id = 76803, PartNumber = "709542", PartFamily = "709542", Description = "Stationary Kit", Description2 = "", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = false, Color = null, Price = 5M, Cost = 1.65M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock30Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Stationary Bench Kit", StockBranches = stockNone },
                new Product { Id = 124421, PartNumber = "744013-500", PartFamily = "744013", Description = "Emp Starter Trim 2\" x 48\"", Description2 = "White", Width = Dimension.FromInch(2), Height = Dimension.FromInch(48), Weight = Weight.FromPounds(5), IsPainted = true, Color = whiteMetalColor, Price = 19M, Cost = 9.13M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock30Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Emp Starter Trim 2\" x 48\"", StockBranches = stockNone },
                new Product { Id = 106626, PartNumber = "742012-535", PartFamily = "742012", Description = "Emp Side 12\" x 72\"", Description2 = "Light Grey", Width = Dimension.FromInch(12), Height = Dimension.FromInch(72), Weight = Weight.FromPounds(6), IsPainted = true, Color = lightGreyMetalColor, Price = 15M, Cost = 6.7M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock30Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Emp Side 12\" x 72\"", StockBranches = stockNone },
                new Product { Id = 142832, PartNumber = "780056", PartFamily = "780056", Description = "Master 1710-Dead Bolt Cylinder", Description2 = "", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = false, Color = null, Price = 27M, Cost = 10.46M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Master 1710-Dead Bolt Cylinder", StockBranches = stockNone },
                new Product { Id = 142835, PartNumber = "780058-500", PartFamily = "780058", Description = "Locker Bench Stand", Description2 = "White", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = true, Color = whiteMetalColor, Price = 55M, Cost = 9.78M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock30Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Locker Bench Stand", StockBranches = stockNone },
                new Product { Id = 142836, PartNumber = "780058-504", PartFamily = "780058", Description = "Locker Bench Stand", Description2 = "Linen", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = true, Color = whiteMetalColor, Price = 55M, Cost = 9.78M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock30Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Locker Bench Stand", StockBranches = stockNone },
                new Product { Id = 142886, PartNumber = "780081", PartFamily = "780081", Description = "Laminate 3' Locker Bench Only", Description2 = "White", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(23), IsPainted = false, Color = null, Price = 153M, Cost = 42.25M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Laminate 3' Locker Bench Only", StockBranches = stockNone },
                new Product { Id = 143008, PartNumber = "7999EM", PartFamily = "143008", Description = "LK Emperor Installation Instructions", Description2 = "White", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = false, Color = null, Price = 0M, Cost = 0M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = quickShip, MarketInfo = sellBoth, IsActivated = true, Comment = "LK Emperor", StockBranches = stockNone },
                new Product { Id = 146308, PartNumber = "DRAWINGLOCKER", PartFamily = "DRAWINGLOCKER", Description = "Project Drawing Locker", Description2 = "White", Width = Dimension.FromInch(0), Height = Dimension.FromInch(0), Weight = Weight.FromPounds(0), IsPainted = false, Color = null, Price = 0M, Cost = 0M, IsSeparateSale = true, ProductMaterial = locker, HardwareMaterial = null, OptionValue = 0, LeadTime = quickShip, MarketInfo = sellBoth, IsActivated = true, Comment = "Project Drawing Locker", StockBranches = stockNone },
                new Product { Id = 146653, PartNumber = "780038", PartFamily = "780038", Description = "Solid Maple 7' Locker Bench", Description2 = "Solid Maple 7' Locker Bench", Width = Dimension.FromInch(36), Height = Dimension.FromInch(55), Weight = Weight.FromPounds(81), IsPainted = true, Color = lightGreyPlasticColor, Price = 302M, Cost = 86.17M, IsSeparateSale = true, ProductMaterial = plastic, HardwareMaterial = null, OptionValue = 65536, LeadTime = noStock25Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Solid Maple 7' Locker Bench", StockBranches = stockNone },
            };

            ConfiguredLockerProducts = new List<ConfiguredLockerProduct>
            {
                new ConfiguredLockerProduct{Id = 114, PartNumber = "702040", Description = "12\" x 12\" x 72\" - 2 Tier Locker Emperor", Tiers = 2, Width = Dimension.FromInch(12), Depth = Dimension.FromInch(12), Height = Dimension.FromInch(72), IsPainted = true, Gauge = LockerSeriesOptionType.Standard.GetString(), Price = 153m, Price800 = 160.65m, Cost=69.49m, Cost800 = 69.49m, Series = empLockerSeries, DesignType = empLockerDesign, IsValidForLep = true, LeadTime = noStock45Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Locker-Emperor"},
                new ConfiguredLockerProduct{Id = 264, PartNumber = "707030", Description = "21\" x 18\" x 72\" - 20GA Locker Divided", Tiers = 1, Width = Dimension.FromInch(21), Depth = Dimension.FromInch(18), Height = Dimension.FromInch(72), IsPainted = true, Gauge = LockerSeriesOptionType.Standard.GetString(), Price = 332m, Price800 = 348.60m, Cost=109.56m, Cost800 = 109.56m, Series = empLockerSeries, DesignType = divDesign, IsValidForLep = false, LeadTime = noStock65Week, MarketInfo = sellBoth, IsActivated = true, Comment = "Locker-Divided"},
            };
        }

        private static void CreateLookup()
        {
            Materials = new List<ProductMaterial>
            {
                new ProductMaterial { Id = 1, Name = "Metal", JdeCode = "TP", IsActivated = true},
                new ProductMaterial { Id = 2, Name = "Solid Plastic", JdeCode = "SP", IsActivated = true},
                new ProductMaterial { Id = 3, Name = "Locker", JdeCode = "LK", IsActivated = true},
                new ProductMaterial { Id = 4, Name = "Arch", JdeCode = "AR", IsActivated = false},
            };

            Branches = new List<BranchPlant>
            {
                new BranchPlant {Id = 1, Code = "1000", Description = "Canada"},
                new BranchPlant {Id = 2, Code = "2000", Description = "USA Mentor Plant"},
            };

            ProductCatalogs = new List<ProductCatalog>
            {
                new ProductCatalog { Id = 1, Name = "ToiletPartition", IsActivated = true, Description = "ToiletPartition" },
                new ProductCatalog { Id = 2, Name = "Locker", IsActivated = true, Description = "Locker" },
                new ProductCatalog { Id = 3, Name = "TouchUpPaint", IsActivated = true, Description = "Touch-up Paint" },
            };

            ProductDesignTypes = new List<ProductDesignType>
            {
                new ProductDesignType {Id = 1, TypeName = "Headrail Braced", Description = "Toilet partition type"},
                new ProductDesignType {Id = 2, TypeName = "Ceiling Hung", Description = "Toilet partition type"},
                new ProductDesignType {Id = 3, TypeName = "Floor Mounted", Description = "Toilet partition type"},
                new ProductDesignType {Id = 4, TypeName = "Floor To Ceiling", Description = "Toilet partition type"},
                new ProductDesignType {Id = 5, TypeName = "Standard", Description = "Screen type"},
                new ProductDesignType {Id = 6, TypeName = "Flange Mounted", Description = "Screen type"},
                new ProductDesignType {Id = 7, TypeName = "IPC", Description = "Screen type"},
                new ProductDesignType {Id = 8, TypeName = "EmperorLocker", Description = "Locker type"},
                new ProductDesignType {Id = 9, TypeName = "GladiatorLocker", Description = "Locker type"},
                new ProductDesignType {Id = 10, TypeName = "ReplacementFront", Description = "Locker type"},
                new ProductDesignType {Id = 11, TypeName = "Divided", Description = "Locker type"},
                new ProductDesignType {Id = 12, TypeName = "OpenFront", Description = "Locker type"},
            };

            ProductSeries = new List<ProductSeries>
            {
                new ProductSeries {Id = 1, Name = "Standard", Description = "Toilet partition series"},
                new ProductSeries {Id = 2, Name = "Elite", Description = "Toilet partition series"},
                new ProductSeries {Id = 3, Name = "ElitePlus", Description = "Toilet partition series"},
                new ProductSeries {Id = 4, Name = "Emperor", Description = "Locker series"},
                new ProductSeries {Id = 5, Name = "Gladiator", Description = "Locker series"},
            };

            LeadTimeSchedules = new List<LeadTimeSchedule>
            {
                new LeadTimeSchedule {Id = 1, Name = "QuickShip", BranchUsQuickShip = 2, BranchUsStockShip = 5, BranchUsStandardShip = 7, BranchCanadaQuickShip = 3, BranchCanadaStockShip = 3, BranchCanadaStandardShip = 7, Comment = "Metal product which is stocked in both Canada and USA"},
                new LeadTimeSchedule {Id = 2, Name = "StockShip", BranchUsQuickShip = 5, BranchUsStockShip = 5, BranchUsStandardShip = 7, BranchCanadaQuickShip = 5, BranchCanadaStockShip = 5, BranchCanadaStandardShip = 7, Comment = "Plastic product which is stocked in both Canada and USA"},
                new LeadTimeSchedule {Id = 3, Name = "QuickShipUs", BranchUsQuickShip = 2, BranchUsStockShip = 5, BranchUsStandardShip = 7, BranchCanadaQuickShip = 7, BranchCanadaStockShip = 7, BranchCanadaStandardShip = 7, Comment = "Metal product which is stocked only in USA"},
                new LeadTimeSchedule {Id = 4, Name = "QuickShipCanada", BranchUsQuickShip = 7, BranchUsStockShip = 7, BranchUsStandardShip = 7, BranchCanadaQuickShip = 3, BranchCanadaStockShip = 3, BranchCanadaStandardShip = 7, Comment = "Product which is stocked only in Canada"},
                new LeadTimeSchedule {Id = 5, Name = "NoStock(2.5Week)", BranchUsQuickShip = 7, BranchUsStockShip = 7, BranchUsStandardShip = 7, BranchCanadaQuickShip = 7, BranchCanadaStockShip = 7, BranchCanadaStandardShip = 7, Comment = "Normal no stock for both USA and Canada"},
                new LeadTimeSchedule {Id = 6, Name = "NoStock(3.5Week)", BranchUsQuickShip = 14, BranchUsStockShip = 14, BranchUsStandardShip = 14, BranchCanadaQuickShip = 14, BranchCanadaStockShip = 14, BranchCanadaStandardShip = 14, Comment = "Elite and Elite plus product for both Canada and USA"},
                new LeadTimeSchedule {Id = 7, Name = "NoStock(4.5Week)", BranchUsQuickShip = 21, BranchUsStockShip = 21, BranchUsStandardShip = 21, BranchCanadaQuickShip = 21, BranchCanadaStockShip = 21, BranchCanadaStandardShip = 21, Comment = "Mensonite"},
                new LeadTimeSchedule {Id = 8, Name = "NoStock(5.5Week)", BranchUsQuickShip = 28, BranchUsStockShip = 28, BranchUsStandardShip = 28, BranchCanadaQuickShip = 28, BranchCanadaStockShip = 28, BranchCanadaStandardShip = 28, Comment = "SSP Ceiling Hung up to 96 (max)"},
                new LeadTimeSchedule {Id = 9, Name = "HardwareQuickShip", BranchUsQuickShip = 5, BranchUsStockShip = 5, BranchUsStandardShip = 7, BranchCanadaQuickShip = 3, BranchCanadaStockShip = 3, BranchCanadaStandardShip = 7, Comment = "Stocked hardware for both Usa and Canada"},
                new LeadTimeSchedule {Id = 10, Name = "SSQuickShipUs", BranchUsQuickShip = 2, BranchUsStockShip = 5, BranchUsStandardShip = 7, BranchCanadaQuickShip = 14, BranchCanadaStockShip = 14, BranchCanadaStandardShip = 14, Comment = "Stainless Steel which stock in Us and not stock in Canada"},
                new LeadTimeSchedule {Id = 11, Name = "NoStock(6.5Week)", BranchUsQuickShip = 35, BranchUsStockShip = 35, BranchUsStandardShip = 35, BranchCanadaQuickShip = 35, BranchCanadaStockShip = 35, BranchCanadaStandardShip = 35, Comment = "Locker Open Front, Arch Divided Locker, Special Locker"},
                new LeadTimeSchedule {Id = 12, Name = "NoStock(7.5Week)", BranchUsQuickShip = 42, BranchUsStockShip = 42, BranchUsStandardShip = 42, BranchCanadaQuickShip = 42, BranchCanadaStockShip = 42, BranchCanadaStandardShip = 42, Comment = "Discontinued Arch product line"},
                new LeadTimeSchedule {Id = 13, Name = "NoStock(3.0Week)", BranchUsQuickShip = 12, BranchUsStockShip = 12, BranchUsStandardShip = 12, BranchCanadaQuickShip = 12, BranchCanadaStockShip = 12, BranchCanadaStandardShip = 12, Comment = "Locker Part not bench and lock"},
            };

            var week35Time = LeadTimeSchedules.FirstOrDefault(t => t.Name == "NoStock(3.5Week)");
            var week45Time = LeadTimeSchedules.FirstOrDefault(t => t.Name == "NoStock(4.5Week)");
            SpecialLeadTimeSchedules = new List<SpecialLeadTimeSchedule>
            {
                new SpecialLeadTimeSchedule { Id = 1, Name = "PanelCutOut", MinNumberOfSpecial = 1, MaxNumberOfSpecial = 30, LeadTimeSchedule = week35Time, IsActivated = true, Comment = "Powder Coated project that contains 30 or less cut out panel" },
                new SpecialLeadTimeSchedule { Id = 2, Name = "PanelCutOutSS", MinNumberOfSpecial = 1, MaxNumberOfSpecial = 30, LeadTimeSchedule = week45Time, IsActivated = true, Comment = "Stainless Steel project that contains cut out panel" },
                new SpecialLeadTimeSchedule { Id = 3, Name = "JrHeight", MinNumberOfSpecial = 1, MaxNumberOfSpecial = null, LeadTimeSchedule = week45Time, IsActivated = true, Comment = "Jr. Height Room, No number limit" },
                new SpecialLeadTimeSchedule { Id = 4, Name = "PanelCutOut", MinNumberOfSpecial = 31, MaxNumberOfSpecial = null, LeadTimeSchedule = week45Time, IsActivated = true, Comment = "Jr. Height Room, No number limit" },
                new SpecialLeadTimeSchedule { Id = 5, Name = "LEP", MinNumberOfSpecial = 1, MaxNumberOfSpecial = null, LeadTimeSchedule = week35Time, IsActivated = true, Comment = "Locker Express Program" },
            };

            ProductMarketSchedules = new List<ProductMarketSchedule>
            {
                new ProductMarketSchedule{Id = 1, Name = "Default", IsSellInCanada = true, IsSellInUsa = true, Description = "Product sell in all markets"},
                new ProductMarketSchedule{Id = 2, Name = "CanadaProduct", IsSellInCanada = true, IsSellInUsa = false, Description = "Product only sell in Canada"},
                new ProductMarketSchedule{Id = 3, Name = "UsaProduct", IsSellInCanada = false, IsSellInUsa = true, Description = "Product only sell in all USA"},
                new ProductMarketSchedule{Id = 4, Name = "Discontinued", IsSellInCanada = false, IsSellInUsa = false, Description = "Product is discontinued"},
            };

            var metal = Materials.FirstOrDefault(m => m.Name == "Metal");
            var plastic = Materials.FirstOrDefault(m => m.Name == "Solid Plastic");

            #region setup product color

            var stockUsa = Branches.Where(b => b.Code == "2000").Select(b => b).ToList();
            var stockBoth = Branches;
            var stockNone = new List<BranchPlant>();
            ProductColors = new List<ProductColor>
            {
                new ProductColor {Id = 1, Name = "No Color", Code = "###", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 2, Name = "Not Painted", Code = "***", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 3, Name = "White", Code = "100", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 4, Name = "Almond", Code = "103", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 5, Name = "Tempest Blue", Code = "109", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 6, Name = "Black", Code = "110", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 7, Name = "Evergreen", Code = "121", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 8, Name = "Light Grey", Code = "135", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 9, Name = "Sand Rift", Code = "181", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 10, Name = "Sand Stone", Code = "182", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 11, Name = "Ebony Speckle", Code = "183", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 12, Name = "Oyster Speckle", Code = "185", Material = plastic, IsActivated = false , StockBranches = stockNone},
                new ProductColor {Id = 13, Name = "Black", Code = "211", Material = plastic, IsActivated = true, StockBranches = stockBoth},
                new ProductColor {Id = 14, Name = "Bone", Code = "213", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 15, Name = "Bronze", Code = "214", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 16, Name = "Burgundy", Code = "215", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 17, Name = "Blueberry", Code = "212", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 18, Name = "Butterscotch", Code = "216", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 19, Name = "Canyon Granite", Code = "217", Material = plastic, IsActivated = true , StockBranches = stockNone},
                new ProductColor {Id = 20, Name = "Fossil", Code = "218", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 21, Name = "Frost Granite", Code = "219", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 22, Name = "Grape", Code = "220", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 23, Name = "Hunter", Code = "222", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 24, Name = "Linen", Code = "223", Material = plastic, IsActivated = true, StockBranches = stockBoth},
                new ProductColor {Id = 25, Name = "Mint Granite", Code = "224", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 26, Name = "Mocha", Code = "225", Material = plastic, IsActivated = true, StockBranches = stockBoth},
                new ProductColor {Id = 27, Name = "Moonlight Granite", Code = "226", Material = plastic, IsActivated = false, StockBranches = stockNone },
                new ProductColor {Id = 28, Name = "Paisley", Code = "227", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 29, Name = "Plum", Code = "228", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 30, Name = "Rosewood Granite", Code = "229", Material = plastic, IsActivated = false, StockBranches = stockNone },
                new ProductColor {Id = 31, Name = "Sky Blue", Code = "230", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 32, Name = "Slate", Code = "231", Material = plastic, IsActivated = true, StockBranches = stockBoth},
                new ProductColor {Id = 33, Name = "Spice", Code = "232", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 34, Name = "Star Blue Granite", Code = "233", Material = plastic, IsActivated = false, StockBranches = stockNone },
                new ProductColor {Id = 35, Name = "White", Code = "234", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 36, Name = "Beige", Code = "236", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 37, Name = "Military Grey", Code = "237", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 38, Name = "Almond", Code = "238", Material = plastic, IsActivated = true, StockBranches = stockBoth},
                new ProductColor {Id = 39, Name = "Gray", Code = "239", Material = plastic, IsActivated = true, StockBranches = stockBoth},
                new ProductColor {Id = 40, Name = "Ebony", Code = "241", Material = plastic, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 41, Name = "Fog", Code = "242", Material = plastic, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 42, Name = "White", Code = "500", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 43, Name = "Linen", Code = "504", Material = metal, IsActivated = true, StockBranches = stockUsa},
                new ProductColor {Id = 44, Name = "Taupe", Code = "505", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 45, Name = "Spruce", Code = "506", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 46, Name = "Forest", Code = "508", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 47, Name = "Black", Code = "510", Material = metal, IsActivated = true, StockBranches = stockUsa},
                new ProductColor {Id = 48, Name = "Dark Green", Code = "520", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 49, Name = "Celadon", Code = "523", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 50, Name = "Latte", Code = "532", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 51, Name = "Silver Grey", Code = "534", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 52, Name = "Light Grey", Code = "535", Material = metal, IsActivated = true, StockBranches = stockBoth},
                new ProductColor {Id = 53, Name = "Rose Grey", Code = "540", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 54, Name = "Extra White", Code = "541", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 55, Name = "Charcoal", Code = "545", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 56, Name = "Ruby Red", Code = "576", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 57, Name = "Moss Green", Code = "581", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 58, Name = "Sahara", Code = "585", Material = metal, IsActivated = true, StockBranches = stockUsa},
                new ProductColor {Id = 59, Name = "Almond", Code = "603", Material = metal, IsActivated = true, StockBranches = stockUsa},
                new ProductColor {Id = 60, Name = "Bordeaux", Code = "607", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 61, Name = "Colonial Blue", Code = "609", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 62, Name = "Rose Mist", Code = "611", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 63, Name = "Silk Blue", Code = "613", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 64, Name = "MIdnight Blue", Code = "617", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 65, Name = "Slate", Code = "621", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 66, Name = "Tile", Code = "624", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 67, Name = "Cream", Code = "625", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 68, Name = "Hardware", Code = "630", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 69, Name = "Sapphire Blue", Code = "639", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 70, Name = "Custom Metal", Code = "800", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 71, Name = "Pewter Metallic", Code = "802", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 72, Name = "Pebble Beach", Code = "812", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 73, Name = "Cast Iron Metallic", Code = "814", Material = metal, IsActivated = true, StockBranches = stockNone },
                new ProductColor {Id = 74, Name = "Bronze Metallic", Code = "815", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 75, Name = "Antique Sage", Code = "816", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 76, Name = "Granite", Code = "818", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 77, Name = "Sienna", Code = "819", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 78, Name = "Silver Metallic", Code = "822", Material = metal, IsActivated = false, StockBranches = stockNone},
                new ProductColor {Id = 79, Name = "Kilim Beige", Code = "826", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 80, Name = "Dover White", Code = "827", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 81, Name = "Dovetail", Code = "828", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 82, Name = "Desert", Code = "829", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 83, Name = "Nickel Metallic", Code = "831", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 84, Name = "Tricorn Black", Code = "833", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 85, Name = "Black Fox", Code = "837", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 86, Name = "Stainless Steel", Code = "900", Material = metal, IsActivated = true, StockBranches = stockUsa},
                new ProductColor {Id = 87, Name = "RigIdized Steel", Code = "90R", Material = metal, IsActivated = true, StockBranches = stockNone},
                new ProductColor {Id = 88, Name = "Custom Plastic", Code = "CSP", Material = metal, IsActivated = true, StockBranches = stockNone},
            };

            #endregion setup product color

            var locker = Materials.FirstOrDefault(m => m.Name == "Locker");
            var tp = ProductCatalogs.FirstOrDefault(c => c.Name == "ToiletPartition");
            var standard = ProductSeries.FirstOrDefault(s => s.Name == "Standard");
            var elt = ProductSeries.FirstOrDefault(s => s.Name == "Elite");
            var elp = ProductSeries.FirstOrDefault(s => s.Name == "ElitePlus");
            var emperorS = ProductSeries.FirstOrDefault(s => s.Name == "Emperor");
            var gladS = ProductSeries.FirstOrDefault(s => s.Name == "Gladiator");
            var arch = Materials.FirstOrDefault(c => c.Name == "Arch");
            var hr = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "Headrail Braced");
            var ch = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "Ceiling Hung");
            var fm = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "Floor Mounted");
            var fc = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "Floor To Ceiling");
            var std = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "Standard");
            var fl = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "Flange Mounted");
            var ipc = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "IPC");
            var epLk = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "EmperorLocker");
            var gdLk = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "GladiatorLocker");
            var repLk = ProductDesignTypes.FirstOrDefault(d => d.TypeName == "ReplacementFront");

            #region setup product part type

            ProductPartTypes = new List<ProductPartType>
            {
                new ProductPartType { Id = 1, Catalog = tp, PartType = "Door", SubPartType = "STD.Metal", Material = metal, Series = standard, DesignType = null, JdeCode = "DOR", IsActivated = true },
                new ProductPartType { Id = 2, Catalog = tp, PartType = "Door", SubPartType = "ELT.Metal", Material = metal, Series = elt, DesignType = null, JdeCode = "DOR", IsActivated = true },
                new ProductPartType { Id = 3, Catalog = tp, PartType = "Door", SubPartType = "ELP.Metal", Material = metal, Series = elp, DesignType = null, JdeCode = "DOR", IsActivated = true },
                new ProductPartType { Id = 4, Catalog = tp, PartType = "Door", SubPartType = "STD.SP", Material = plastic, Series = standard, DesignType = null, JdeCode = "SDR", IsActivated = true },
                new ProductPartType { Id = 5, Catalog = tp, PartType = "Door", SubPartType = "Arch", Material = arch, Series = null, DesignType = null, JdeCode = "ADR", IsActivated = true },
                new ProductPartType { Id = 6, Catalog = tp, PartType = "Panel", SubPartType = "STD.Metal", Material = metal, Series = standard, DesignType = null, JdeCode = "PAN", IsActivated = true },
                new ProductPartType { Id = 7, Catalog = tp, PartType = "Panel", SubPartType = "ELT.Metal", Material = metal, Series = elt, DesignType = null, JdeCode = "PAN", IsActivated = true },
                new ProductPartType { Id = 8, Catalog = tp, PartType = "Panel", SubPartType = "ELP.Metal", Material = metal, Series = elp, DesignType = null, JdeCode = "PAN", IsActivated = true },
                new ProductPartType { Id = 9, Catalog = tp, PartType = "Panel", SubPartType = "STD.SP", Material = plastic, Series = standard, DesignType = null, JdeCode = "PAN", IsActivated = true },
                new ProductPartType { Id = 10, Catalog = tp, PartType = "Panel", SubPartType = "Arch", Material = arch, Series = null, DesignType = null, JdeCode = "PAN", IsActivated = true },
                new ProductPartType { Id = 11, Catalog = tp, PartType = "Pilaster", SubPartType = "HR.STD.Metal", Material = metal, Series = standard, DesignType = hr, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 12, Catalog = tp, PartType = "Pilaster", SubPartType = "CH.STD.Metal", Material = metal, Series = standard, DesignType = ch, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 13, Catalog = tp, PartType = "Pilaster", SubPartType = "FM.STD.Metal", Material = metal, Series = standard, DesignType = fm, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 14, Catalog = tp, PartType = "Pilaster", SubPartType = "FC.STD.Metal", Material = metal, Series = elt, DesignType = fc, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 15, Catalog = tp, PartType = "Pilaster", SubPartType = "HR.ELT.Metal", Material = metal, Series = elt, DesignType = hr, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 16, Catalog = tp, PartType = "Pilaster", SubPartType = "CH.ELT.Metal", Material = metal, Series = elt, DesignType = ch, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 17, Catalog = tp, PartType = "Pilaster", SubPartType = "FM.ELT.Metal", Material = metal, Series = elt, DesignType = fm, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 18, Catalog = tp, PartType = "Pilaster", SubPartType = "FC.ELT.Metal", Material = metal, Series = elt, DesignType = fc, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 19, Catalog = tp, PartType = "Pilaster", SubPartType = "HR.ELP.Metal", Material = metal, Series = elp, DesignType = hr, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 20, Catalog = tp, PartType = "Pilaster", SubPartType = "CH.ELP.Metal", Material = metal, Series = elp, DesignType = ch, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 21, Catalog = tp, PartType = "Pilaster", SubPartType = "FC.ELP.Metal", Material = metal, Series = elp, DesignType = fc, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 22, Catalog = tp, PartType = "Pilaster", SubPartType = "HR.STD.SP", Material = plastic, Series = standard, DesignType = hr, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 23, Catalog = tp, PartType = "Pilaster", SubPartType = "CH.STD.SP", Material = plastic, Series = standard, DesignType = ch, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 24, Catalog = tp, PartType = "Pilaster", SubPartType = "FC.STD.SP", Material = plastic, Series = standard, DesignType = fc, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 25, Catalog = tp, PartType = "Pilaster", SubPartType = "Arch", Material = arch, Series = null, DesignType = null, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 26, Catalog = tp, PartType = "Screen", SubPartType = "STD.Metal", Material = metal, Series = standard, DesignType = std, JdeCode = "SCR", IsActivated = true },
                new ProductPartType { Id = 27, Catalog = tp, PartType = "Screen", SubPartType = "FL.Metal", Material = metal, Series = standard, DesignType = fl, JdeCode = "SCR", IsActivated = true },
                new ProductPartType { Id = 28, Catalog = tp, PartType = "Screen", SubPartType = "IPC.Metal", Material = metal, Series = standard, DesignType = ipc, JdeCode = "SCR", IsActivated = true },
                new ProductPartType { Id = 29, Catalog = tp, PartType = "Screen", SubPartType = "PIL.Metal", Material = metal, Series = standard, DesignType = fm, JdeCode = "SCR", IsActivated = true },
                new ProductPartType { Id = 30, Catalog = tp, PartType = "Screen", SubPartType = "STD.SP", Material = plastic, Series = standard, DesignType = std, JdeCode = "SCR", IsActivated = true },
                new ProductPartType { Id = 31, Catalog = tp, PartType = "Screen", SubPartType = "PIL.SP", Material = plastic, Series = standard, DesignType = fm, JdeCode = "SCR", IsActivated = true },
                new ProductPartType { Id = 32, Catalog = tp, PartType = "Screen", SubPartType = "Arch", Material = arch, Series = null, DesignType = null, JdeCode = "SCR", IsActivated = true },
                new ProductPartType { Id = 33, Catalog = tp, PartType = "Channel", SubPartType = "STD.Alcove", Material = metal, Series = standard, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 34, Catalog = tp, PartType = "Channel", SubPartType = "STD.Wall", Material = metal, Series = standard, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 35, Catalog = tp, PartType = "Channel", SubPartType = "STD.Pilaster", Material = metal, Series = standard, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 36, Catalog = tp, PartType = "Channel", SubPartType = "ELT.Alcove", Material = metal, Series = elt, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 37, Catalog = tp, PartType = "Channel", SubPartType = "ELT.Wall", Material = metal, Series = elt, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 38, Catalog = tp, PartType = "Channel", SubPartType = "ELT.Pilaster", Material = metal, Series = elt, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 39, Catalog = tp, PartType = "Channel", SubPartType = "ELP.Alcove", Material = metal, Series = elp, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 40, Catalog = tp, PartType = "Channel", SubPartType = "ELP.Wall", Material = metal, Series = elp, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 41, Catalog = tp, PartType = "Channel", SubPartType = "ELP.Pilaster", Material = metal, Series = elp, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 42, Catalog = tp, PartType = "Channel", SubPartType = "TwoEar", Material = plastic, Series = null, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 43, Catalog = tp, PartType = "Channel", SubPartType = "UChannel", Material = plastic, Series = null, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 44, Catalog = tp, PartType = "Channel", SubPartType = "Alcove.SP", Material = plastic, Series = null, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 45, Catalog = tp, PartType = "Channel", SubPartType = "Arch", Material = arch, Series = null, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 46, Catalog = tp, PartType = "Channel", SubPartType = "Alcove.Arch", Material = arch, Series = null, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 47, Catalog = tp, PartType = "Post", SubPartType = "Square", Material = metal, Series = null, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 48, Catalog = tp, PartType = "Channel", SubPartType = "OneEar", Material = plastic, Series = null, DesignType = null, JdeCode = "CHA", IsActivated = true },
                new ProductPartType { Id = 49, Catalog = tp, PartType = "Post", SubPartType = "Wall", Material = metal, Series = null, DesignType = null, JdeCode = "PIL", IsActivated = true },
                new ProductPartType { Id = 50, Catalog = tp, PartType = "ContinuousHinge", SubPartType = "STD", Material = metal, Series = standard, DesignType = null, JdeCode = "HGE", IsActivated = true },
                new ProductPartType { Id = 51, Catalog = tp, PartType = "ContinuousHinge", SubPartType = "ELT", Material = metal, Series = elt, DesignType = null, JdeCode = "HGE", IsActivated = true },
                new ProductPartType { Id = 52, Catalog = tp, PartType = "ContinuousHinge", SubPartType = "Hinge.SP", Material = plastic, Series = null, DesignType = null, JdeCode = "HGE", IsActivated = true },
                new ProductPartType { Id = 53, Catalog = tp, PartType = "ContinuousStop", SubPartType = "STOP.Metal", Material = metal, Series = null, DesignType = null, JdeCode = "STP", IsActivated = true },
                new ProductPartType { Id = 54, Catalog = tp, PartType = "ContinuousStop", SubPartType = "STOP.SP", Material = plastic, Series = null, DesignType = null, JdeCode = "STP", IsActivated = true },
                new ProductPartType { Id = 55, Catalog = tp, PartType = "HingeSightLine", SubPartType = "STD", Material = metal, Series = standard, DesignType = null, JdeCode = "STP", IsActivated = true },
                new ProductPartType { Id = 56, Catalog = tp, PartType = "HingeSightLine", SubPartType = "ELT", Material = metal, Series = elt, DesignType = null, JdeCode = "STP", IsActivated = true },
                new ProductPartType { Id = 57, Catalog = tp, PartType = "HingeSightLine", SubPartType = "ELP", Material = metal, Series = elp, DesignType = null, JdeCode = "STP", IsActivated = true },
                new ProductPartType { Id = 58, Catalog = tp, PartType = "Headrail", SubPartType = "Metal", Material = metal, Series = null, DesignType = null, JdeCode = "HDR", IsActivated = true },
                new ProductPartType { Id = 59, Catalog = tp, PartType = "ShowerCurtain", SubPartType = "ShowerCurtain", Material = null, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 60, Catalog = tp, PartType = "Headrail", SubPartType = "Brushed.Metal", Material = metal, Series = null, DesignType = null, JdeCode = "HDR", IsActivated = true },
                new ProductPartType { Id = 61, Catalog = tp, PartType = "Headrail", SubPartType = "SP", Material = metal, Series = null, DesignType = null, JdeCode = "HDR", IsActivated = true },
                new ProductPartType { Id = 62, Catalog = tp, PartType = "Headrail", SubPartType = "Arch", Material = metal, Series = null, DesignType = null, JdeCode = "HDR", IsActivated = true },
                new ProductPartType { Id = 63, Catalog = tp, PartType = "PilasterShoe", SubPartType = "SP", Material = metal, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 64, Catalog = tp, PartType = "PilasterShoe", SubPartType = "Arch", Material = metal, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 65, Catalog = tp, PartType = "PilasterShoe", SubPartType = "Metal", Material = metal, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 66, Catalog = tp, PartType = "StabilizerBar", SubPartType = "SP", Material = metal, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 67, Catalog = tp, PartType = "Plate", SubPartType = "PilasterWallPlate", Material = null, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 68, Catalog = tp, PartType = "Hardware", SubPartType = "TP.Hardware", Material = null, Series = null, DesignType = null, JdeCode = "THW", IsActivated = true },
                new ProductPartType { Id = 69, Catalog = tp, PartType = "Drawing", SubPartType = "Metal", Material = metal, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 70, Catalog = tp, PartType = "Drawing", SubPartType = "SP", Material = plastic, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 71, Catalog = tp, PartType = "Drawing", SubPartType = "Project", Material = null, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 62, Catalog = tp, PartType = "Instruction", SubPartType = "Metal", Material = metal, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 73, Catalog = tp, PartType = "Instruction", SubPartType = "SP", Material = plastic, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 74, Catalog = tp, PartType = "Instruction", SubPartType = "Arch", Material = arch, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 75, Catalog = tp, PartType = "Charge", SubPartType = "Metal", Material = metal, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 76, Catalog = tp, PartType = "Charge", SubPartType = "SP", Material = plastic, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 77, Catalog = tp, PartType = "Charge", SubPartType = "Arch", Material = arch, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 78, Catalog = tp, PartType = "TouchupPaint", SubPartType = "TouchupPaint", Material = null, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 79, Catalog = tp, PartType = "TouchupPaint", SubPartType = "SprayCan", Material = null, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 80, Catalog = tp, PartType = "Shelf", SubPartType = "Emperor.Shelf", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 81, Catalog = tp, PartType = "Shelf", SubPartType = "Gladiator.Shelf", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 82, Catalog = tp, PartType = "DressEnd", SubPartType = "Emperor.DressEnd", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 83, Catalog = tp, PartType = "DressEnd", SubPartType = "Gladiator.DressEnd", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 84, Catalog = tp, PartType = "SlopeTop", SubPartType = "Emperor.SlopeTop", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 85, Catalog = tp, PartType = "SlopeTop", SubPartType = "Gladiator.SlopeTop", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 86, Catalog = tp, PartType = "Back", SubPartType = "Emperor.Back", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 87, Catalog = tp, PartType = "Back", SubPartType = "Gladiator.Back", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 88, Catalog = tp, PartType = "Bench", SubPartType = "Bench", Material = locker, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 89, Catalog = tp, PartType = "Trim", SubPartType = "Emperor.Trim", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 90, Catalog = tp, PartType = "Trim", SubPartType = "Emperor.Trim", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 91, Catalog = tp, PartType = "BoxBases", SubPartType = "Emperor.BoxBases", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 92, Catalog = tp, PartType = "BoxBases", SubPartType = "Gladiator.BoxBases", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 93, Catalog = tp, PartType = "ZBase", SubPartType = "Emperor.ZBase", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 94, Catalog = tp, PartType = "ZBase", SubPartType = "Gladiator.ZBase", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 95, Catalog = tp, PartType = "Side", SubPartType = "Emperor.Side", Material = locker, Series = emperorS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 96, Catalog = tp, PartType = "Side", SubPartType = "Gladiator.Side", Material = locker, Series = gladS, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 97, Catalog = tp, PartType = "Door", SubPartType = "Locker", Material = locker, Series = null, DesignType = null, JdeCode = "LDR", IsActivated = true },
                new ProductPartType { Id = 98, Catalog = tp, PartType = "Lock", SubPartType = "Combination", Material = locker, Series = null, DesignType = null, JdeCode = "LDR", IsActivated = true },
                new ProductPartType { Id = 99, Catalog = tp, PartType = "Lock", SubPartType = "PadLock", Material = locker, Series = null, DesignType = null, JdeCode = "LDR", IsActivated = true },
                new ProductPartType { Id = 100, Catalog = tp, PartType = "Frame", SubPartType = "Emperor", Material = locker, Series = emperorS, DesignType = epLk, JdeCode = "LDR", IsActivated = true },
                new ProductPartType { Id = 101, Catalog = tp, PartType = "Frame", SubPartType = "Retrofit", Material = locker, Series = emperorS, DesignType = gdLk, JdeCode = "LDR", IsActivated = true },
                new ProductPartType { Id = 102, Catalog = tp, PartType = "Frame", SubPartType = "Gladiator", Material = locker, Series = gladS, DesignType = repLk, JdeCode = "LDR", IsActivated = true },
                new ProductPartType { Id = 103, Catalog = tp, PartType = "Hardware", SubPartType = "Locker.Hardware", Material = locker, Series = null, DesignType = null, JdeCode = "LHW", IsActivated = true },
                new ProductPartType { Id = 104, Catalog = tp, PartType = "Drawing", SubPartType = "Locker", Material = locker, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 105, Catalog = tp, PartType = "Instruction", SubPartType = "Locker", Material = locker, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
                new ProductPartType { Id = 106, Catalog = tp, PartType = "Charge", SubPartType = "Locker", Material = locker, Series = null, DesignType = null, JdeCode = "NULL", IsActivated = true },
            };

            #endregion setup product part type
        }

        private static void CreateRepositories()
        {
            // product
            var prodRepoMock = new Mock<IRepository<Note>>();
            prodRepoMock.Setup(r => r.FindEntities()).Returns(() => Notes.Clone().AsQueryable());
            prodRepoMock.Setup(r => r.FindEntityById(It.IsAny<int>())).Returns((int id) => Notes.Clone().FirstOrDefault(t => t.Id == id));
            prodRepoMock.Setup(r => r.Update(It.IsAny<Note>()))
                                    .Returns((Note n) => n)
                                    .Callback((Note n) =>
                                    {
                                        var oldRec = Notes.FirstOrDefault(a => a.Id == n.Id);
                                        if (oldRec == null)
                                        {
                                            return;
                                        }

                                        Notes.Remove(oldRec);
                                        Notes.Add(n);
                                    });
            prodRepoMock.Setup(r => r.Add(It.IsAny<Note>()))
                                   .Returns((Note n) => n)
                                   .Callback((Note p) =>
                                   {
                                       p.Id = Notes.Count + 1;
                                       Notes.Add(p);
                                   });
            NoteRepo = prodRepoMock.Object;

            // locker configured product
            var lockerAdderRepoMock = new Mock<IRepository<ConfiguredLockerProduct>>();
            lockerAdderRepoMock.Setup(r => r.FindEntities()).Returns(() => ConfiguredLockerProducts.Clone().AsQueryable());
            lockerAdderRepoMock.Setup(r => r.FindEntityById(It.IsAny<int>())).Returns((int id) => ConfiguredLockerProducts.Clone().FirstOrDefault(t => t.Id == id));
            lockerAdderRepoMock.Setup(r => r.Update(It.IsAny<ConfiguredLockerProduct>()))
                                    .Returns((ConfiguredLockerProduct p) => p)
                                    .Callback((ConfiguredLockerProduct p) =>
                                    {
                                        var oldRec = ConfiguredLockerProducts.FirstOrDefault(a => a.Id == p.Id);
                                        if (oldRec == null)
                                        {
                                            return;
                                        }

                                        ConfiguredLockerProducts.Remove(oldRec);
                                        ConfiguredLockerProducts.Add(p);
                                    });
            lockerAdderRepoMock.Setup(r => r.Add(It.IsAny<ConfiguredLockerProduct>()))
                                   .Returns((ConfiguredLockerProduct p) => p)
                                   .Callback((ConfiguredLockerProduct p) =>
                                   {
                                       p.Id = ConfiguredLockerProducts.Count + 1;
                                       ConfiguredLockerProducts.Add(p);
                                   });
            ConfiguredLockeProductRepository = lockerAdderRepoMock.Object;

            // product color
            var colorRepoMock = new Mock<IRepository<ProductColor>>();

            colorRepoMock.Setup(r => r.FindEntities()).Returns(() => ProductColors.Clone().AsQueryable());
            colorRepoMock.Setup(r => r.FindEntityById(It.IsAny<int>())).Returns((int id) => ProductColors.Clone().FirstOrDefault(t => t.Id == id));
            colorRepoMock.Setup(r => r.Update(It.IsAny<ProductColor>()))
                                    .Returns((ProductColor pc) => pc)
                                    .Callback((ProductColor pc) =>
                                    {
                                        var oldRec = ProductColors.FirstOrDefault(a => a.Id == pc.Id);
                                        if (oldRec == null)
                                        {
                                            return;
                                        }

                                        ProductColors.Remove(oldRec);
                                        ProductColors.Add(pc);
                                    });
            colorRepoMock.Setup(r => r.Add(It.IsAny<ProductColor>()))
                                   .Returns((ProductColor pc) => pc)
                                   .Callback((ProductColor pc) =>
                                   {
                                       pc.Id = ProductColors.Count + 1;
                                       ProductColors.Add(pc);
                                   });
            ProductColorRepository = colorRepoMock.Object;

            // Lead Time Schedule
            var leadTimeRepoMock = new Mock<IRepository<LeadTimeSchedule>>();
            leadTimeRepoMock.Setup(r => r.FindEntities()).Returns(() => LeadTimeSchedules.Clone().AsQueryable());
            leadTimeRepoMock.Setup(r => r.FindEntityById(It.IsAny<int>())).Returns((int id) => LeadTimeSchedules.Clone().FirstOrDefault(t => t.Id == id));
            leadTimeRepoMock.Setup(r => r.Update(It.IsAny<LeadTimeSchedule>()))
                   .Returns((LeadTimeSchedule lts) => lts)
                   .Callback((LeadTimeSchedule lts) =>
                   {
                       var oldRec = LeadTimeSchedules.FirstOrDefault(a => a.Id == lts.Id);
                       if (oldRec == null)
                           return;

                       LeadTimeSchedules.Remove(oldRec);
                       LeadTimeSchedules.Add(lts);
                   });
            leadTimeRepoMock.Setup(r => r.Add(It.IsAny<LeadTimeSchedule>()))
                               .Returns((LeadTimeSchedule lts) => lts)

                               .Callback((LeadTimeSchedule lts) =>
                               {
                                   lts.Id = LeadTimeSchedules.Count + 1;
                                   LeadTimeSchedules.Add(lts);
                               });

            LeadTimeScheduleRepository = leadTimeRepoMock.Object;

            // Special Lead Time Schedule
            var specialLeadTimeRepoMock = new Mock<IRepository<SpecialLeadTimeSchedule>>();
            specialLeadTimeRepoMock.Setup(r => r.FindEntities()).Returns(() => SpecialLeadTimeSchedules.Clone().AsQueryable());
            specialLeadTimeRepoMock.Setup(r => r.FindEntityById(It.IsAny<int>())).Returns((int id) => SpecialLeadTimeSchedules.Clone().FirstOrDefault(t => t.Id == id));
            specialLeadTimeRepoMock.Setup(r => r.Update(It.IsAny<SpecialLeadTimeSchedule>()))
                   .Returns((SpecialLeadTimeSchedule lts) => lts)
                   .Callback((SpecialLeadTimeSchedule lts) =>
                   {
                       var oldRec = SpecialLeadTimeSchedules.FirstOrDefault(a => a.Id == lts.Id);
                       if (oldRec == null)
                           return;

                       SpecialLeadTimeSchedules.Remove(oldRec);
                       SpecialLeadTimeSchedules.Add(lts);
                   });
            specialLeadTimeRepoMock.Setup(r => r.Add(It.IsAny<SpecialLeadTimeSchedule>()))
                               .Returns((SpecialLeadTimeSchedule lts) => lts)

                               .Callback((SpecialLeadTimeSchedule lts) =>
                               {
                                   lts.Id = SpecialLeadTimeSchedules.Count + 1;
                                   SpecialLeadTimeSchedules.Add(lts);
                               });

            SpecialLeadTimeScheduleRepository = specialLeadTimeRepoMock.Object;
        }

        private static void CreateLookupRepo()
        {
            var lookupMock = new Mock<ILookupRepository>();
            lookupMock.Setup(l => l.FindEntities<Note>()).Returns(() => Notes.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<User>()).Returns(() => Users.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<Email>()).Returns(() => Emails.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<Phone>()).Returns(() => Phones.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<NoteRender>()).Returns(() => NoteRenders.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<LeadTimeSchedule>()).Returns(() => LeadTimeSchedules.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<SpecialLeadTimeSchedule>()).Returns(() => SpecialLeadTimeSchedules.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<ProductColor>()).Returns(() => ProductColors.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<ProductPartType>()).Returns(() => ProductPartTypes.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<ProductMarketSchedule>()).Returns(() => ProductMarketSchedules.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<Product>()).Returns(() => Products.Clone().AsQueryable());
            lookupMock.Setup(l => l.FindEntities<ConfiguredLockerProduct>()).Returns(() => ConfiguredLockerProducts.Clone().AsQueryable());

            LookupRepository = lookupMock.Object;

            var multiSrcLookMoc = new Mock<IMultiSourceLookupRepository>();
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ProductMaterial>()).Returns(() => LookupRepository.FindEntities<ProductMaterial>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<BranchPlant>()).Returns(() => LookupRepository.FindEntities<BranchPlant>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ProductCatalog>()).Returns(() => LookupRepository.FindEntities<ProductCatalog>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ProductDesignType>()).Returns(() => LookupRepository.FindEntities<ProductDesignType>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ProductSeries>()).Returns(() => LookupRepository.FindEntities<ProductSeries>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<LeadTimeSchedule>()).Returns(() => LookupRepository.FindEntities<LeadTimeSchedule>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<SpecialLeadTimeSchedule>()).Returns(() => LookupRepository.FindEntities<SpecialLeadTimeSchedule>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ProductColor>()).Returns(() => LookupRepository.FindEntities<ProductColor>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ProductPartType>()).Returns(() => LookupRepository.FindEntities<ProductPartType>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ProductMarketSchedule>()).Returns(() => LookupRepository.FindEntities<ProductMarketSchedule>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<Product>()).Returns(() => LookupRepository.FindEntities<Product>());
            multiSrcLookMoc.Setup(lk => lk.FindEntities<ConfiguredLockerProduct>()).Returns(() => LookupRepository.FindEntities<ConfiguredLockerProduct>());
            MultiSourceLookupRepository = multiSrcLookMoc.Object;
        }

        #endregion private methods
    }
}