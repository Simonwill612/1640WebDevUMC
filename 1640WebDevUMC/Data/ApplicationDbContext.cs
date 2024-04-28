using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using File = _1640WebDevUMC.Models.File;

namespace _1640WebDevUMC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.AcademicYears)
                .WithOne(a => a.Faculty)
                .HasForeignKey(a => a.FacultyID);

            // Configure foreign key relationship between Comment and ApplicationUser
            modelBuilder.Entity<Comment>()
                   .HasOne(c => c.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(c => c.Email)
                   .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction depending on your requirements

            // Configure foreign key relationship between Comment and Contribution
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Contribution)
                .WithMany(con => con.Comments) // Assuming Contribution has a navigation property 'Comments'
                .HasForeignKey(c => c.ContributionID)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction depending on your requirements

            // Configure foreign key relationship between Comment and File
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.File)
                .WithMany(file => file.Comments) // Assuming File has a navigation property 'Comments'
                .HasForeignKey(c => c.FileID)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction depending on your requirements
                                                    // Seed data for role table
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "0a29fc34-cf49-477d-8e45-0199d8a8ca14", Name = "Marketing Coordinator", NormalizedName = "MARKETING COORDINATOR", ConcurrencyStamp = null },
                new IdentityRole { Id = "0bd06470-6120-4334-86c5-2d1fcd75f514", Name = "Guest", NormalizedName = "GUEST", ConcurrencyStamp = null },
                new IdentityRole { Id = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", Name = "Student", NormalizedName = "STUDENT", ConcurrencyStamp = null },
                new IdentityRole { Id = "cb316555-3aec-4234-aea9-1b4e22add4fb", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = null },
                new IdentityRole { Id = "ec5e815d-2ae2-4a7d-a687-fc7502e05560", Name = "Marketing Managerment", NormalizedName = "MARKETING MANAGERMENT", ConcurrencyStamp = null }
            );

            // Seed data for user table
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = "a94809be-838f-4f8a-bccc-003f5f6025ec", FacultyName = null, UserName = "Admin@gmail.com", NormalizedUserName = "ADMIN@GMAIL.COM", Email = "Admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEPPDS07tfI8yMRn0KWAt1BEIhlaXdIjBsUm+B8eeQcge5DDWNBtVpODMtLxP09ZB8g==", SecurityStamp = "YQIY43F2NSBTAZ2ZAHBAXJ6H5KBHEPZZ", ConcurrencyStamp = "d7e504e8-0099-4861-9194-c58c338f539c" },
                new ApplicationUser { Id = "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", FacultyName = null, UserName = "MarketingManagerment@gmail.com", NormalizedUserName = "MARKETINGMANAGERMENT@GMAIL.COM", Email = "MarketingManagerment@gmail.com", NormalizedEmail = "MARKETINGMANAGERMENT@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAENTiXM2ltFsSFDX0sjUqbC/wWNB1ODDl/MvgaelowVco1YlgccPz/nd41Gdphyb1Kg==", SecurityStamp = "VNOO7SFU6U733OQYSFORZPA3CCYCTQZS", ConcurrencyStamp = "266f40e1-1c1e-4172-9804-d715cf77848d" },
                new ApplicationUser { Id = "92de70df-e818-41d9-9d24-4782eb838fe8", FacultyName = null, UserName = "MarketingManagerment1@gmail.com", NormalizedUserName = "MARKETINGMANAGERMENT1@GMAIL.COM", Email = "MarketingManagerment1@gmail.com", NormalizedEmail = "MARKETINGMANAGERMENT1@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEHzq4s10lDXIFitL95HxIswckADNGO0SfULZIq04hzvQDdst0KvHrPt6Gfn8liwF7A==", SecurityStamp = "7P4MNUPRM53XPCKTRIUXVDMXNYEXZHLG", ConcurrencyStamp = "749850b3-0b12-4d39-9682-7469bf66658d" },
                new ApplicationUser { Id = "216ca0d2-6672-4cc9-8760-752f58c9d318", FacultyName = "COMP1640", UserName = "MarketingCoordinator@gmail.com", NormalizedUserName = "MARKETINGCOORDINATOR@GMAIL.COM", Email = "MarketingCoordinator@gmail.com", NormalizedEmail = "MARKETINGCOORDINATOR@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEPy2FFy18VUAIyausYLfWaeEC+6ELFbcdPO4ka2uLvzMs0vXElcDEY/GjBsa2LxY6A==", SecurityStamp = "X6LBPU6MHZ67CDKRT3SGQABIYKKYETTA", ConcurrencyStamp = "77f6fc2b-fc5b-42cf-b773-ed710778d8f6" },
                new ApplicationUser { Id = "47d2b6a5-9b9c-4703-9d06-7505ac14d08f", FacultyName = null, UserName = "MarketingCoodinator1@gmail.com", NormalizedUserName = "MARKETINGCOODINATOR1@GMAIL.COM", Email = "MarketingCoodinator1@gmail.com", NormalizedEmail = "MARKETINGCOODINATOR1@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEG+b/+1sW/W6N2fhLzqSdHA9Ar00EQkhEcCbR66pGJG1ac8R3ladiMMEflGI86AK8g==", SecurityStamp = "OEV77TI2UFUTNBO74U33KYEPMBIJPJYX", ConcurrencyStamp = "a8ea837c-34e3-4c06-95b5-0b8b0cf75ee0" },
                new ApplicationUser { Id = "e56ba4dd-0143-4221-84f3-c34e34e0e14f", FacultyName = "COMP1640", UserName = "Guest@gmail.com", NormalizedUserName = "GUEST@GMAIL.COM", Email = "Guest@gmail.com", NormalizedEmail = "GUEST@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEO7FHWTjZ+Mnk1IXD2EDF8/cRY+OK9NAX+V3DafLtsEHN+Put1O+/Su81iE94jN3Ig==", SecurityStamp = "724ZAKU2VBDDWZEQLCGCJQ3WJG3P6LLE", ConcurrencyStamp = "dd8d2571-9ae9-44ea-9af5-1d038dd4cb6a" },
                new ApplicationUser { Id = "c2512fdf-cab0-423e-87a4-2d33ab8a5a74", FacultyName = "COMP1640", UserName = "Guest1@gmail.com", NormalizedUserName = "GUEST1@GMAIL.COM", Email = "Guest1@gmail.com", NormalizedEmail = "GUEST1@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEOtce99JMkRpyi6jWF7KFfhmbQxvjzMDX1Eiu/mxx7gTw4P5SJJn+9A5XAhoQdrnkw==", SecurityStamp = "OZUAZZN2PSOSV3UO5CC6YDFB433CBFDF", ConcurrencyStamp = "7d5bbb78-7d32-4867-9296-bbbe201bd491" },
                new ApplicationUser { Id = "90316e26-012d-4c0c-939c-2dee25cf8183", FacultyName = null, UserName = "Student1@gmail.com", NormalizedUserName = "STUDENT1@GMAIL.COM", Email = "Student1@gmail.com", NormalizedEmail = "STUDENT1@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEKDejowos2MamEJWU+9ieQdEmHvzZMfezMkE1TfEUknawtfZ9WrWI7A7PQ/XLI1+Tg==", SecurityStamp = "5KILRK7QTV6GWCSXGNTNRCSQAHD2MCFV", ConcurrencyStamp = "ec30a8dd-3a71-4ff0-bbab-f9409cf36d48" },
                new ApplicationUser { Id = "e33f9319-c5b6-4e98-893e-565327621838", FacultyName = null, UserName = "Student@gmail.com", NormalizedUserName = "STUDENT@GMAIL.COM", Email = "Student@gmail.com", NormalizedEmail = "STUDENT@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEAKOaX32P4nURuTPOG+R1de+qQo5Spk5ODUs75m1SOjjxo2JOnJSfcEcVEK10SAUkQ==", SecurityStamp = "5RWONCWIC23E6Q53ZHCHZLPUUI456NUB", ConcurrencyStamp = "6366b2d1-e424-4a06-964a-333eeb8fcf99" },
                new ApplicationUser { Id = "f5719d35-67c3-4458-8b3f-dc5c410bee91", FacultyName = null, UserName = "Admin1@gmail.com", NormalizedUserName = "ADMIN1@GMAIL.COM", Email = "Admin1@gmail.com", NormalizedEmail = "ADMIN1@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEM3HvIpr8pPqbVL/iJGPpZ3is5dw5O2C3TAra0IOzGVST/UR6Qn27Ai7DJSOTZKP8w==", SecurityStamp = "BQ6KJD5FXTGZPPDW7IOMSAJVIWVWDOJI", ConcurrencyStamp = "f3d35431-eef4-49fa-a6be-bf39886c5f33" },
                new ApplicationUser { Id = "d67405f3-aecc-4873-a19e-ced3fcbeac1d", FacultyName = null, UserName = "student2@gmail.com", NormalizedUserName = "STUDENT2@GMAIL.COM", Email = "student2@gmail.com", NormalizedEmail = "STUDENT2@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEObW6aLAhR0p4ngWx+NiXiAoWSIpXUUP99JTB1LQQNmjXxAFoAi6/xyhI3RkqEvDvw==", SecurityStamp = "SLZUSVP34PBUKPVY63UFZX2RB7JDFMZN", ConcurrencyStamp = "e258c7e1-5d38-4a40-962b-a25aefd3a5c2" },
                new ApplicationUser { Id = "e1c5de51-1cf3-41fb-809f-734ca787c863", FacultyName = null, UserName = "student3@gmail.com", NormalizedUserName = "STUDENT3@GMAIL.COM", Email = "student3@gmail.com", NormalizedEmail = "STUDENT3@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAECfqYcOiXuHmNJR4H+FUa7VJ5NEotGfsMIozDZViePH1E3HMVx4Be3OCXOWLWdslkA==", SecurityStamp = "UJ5GTX2CQD6IDK5XGDSUIVAGXF6UWYE2", ConcurrencyStamp = "ca6e85b9-13d8-45c6-b93e-66e725564fdc" },
                new ApplicationUser { Id = "2f3c0a4c-c42e-44e3-b08f-fd446ada9e4b", FacultyName = null, UserName = "student4@gmail.com", NormalizedUserName = "STUDENT4@GMAIL.COM", Email = "student4@gmail.com", NormalizedEmail = "STUDENT4@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEF/3/wUBT9NOgzMP7Z9XY6oSDH7okOb8W1LQwc11/HC+EPzZ2iqty2mbmQiNver6vQ==", SecurityStamp = "QECAUXQTD26FV5XKZAGN6YO4OQRPB6XR", ConcurrencyStamp = "89e42fa8-e776-4138-bb59-ab5e114690eb" },
                new ApplicationUser { Id = "54bba5fb-57f9-49f5-9d92-5a06eedb8fc4", FacultyName = null, UserName = "student5@gmail.com", NormalizedUserName = "STUDENT5@GMAIL.COM", Email = "student5@gmail.com", NormalizedEmail = "STUDENT5@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEN2DkDMlhW2AWxuSl+DPXiifpRpBbEQx7GYEwrjsCuQvZL+rIYR3I9kPcD4jf1gumg==", SecurityStamp = "T27XLM3SSZNLKJEJQZOBYE5EY5PB2QZD", ConcurrencyStamp = "224b0c6e-cd81-4ab2-ae3f-fdcbfddd258e" },
                new ApplicationUser { Id = "b7244936-fb02-432a-8bc6-e27d4a8a9ab0", FacultyName = null, UserName = "student6@gmail.com", NormalizedUserName = "STUDENT6@GMAIL.COM", Email = "student6@gmail.com", NormalizedEmail = "STUDENT6@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAENLVgxNB7Du0qI4+4Wbd/d9vNbxRCWRlhwM6jzMnJnmnvn7/rfwMBtEG/4nSjK7PGA==", SecurityStamp = "CWS5AFJYTDC4C3EN3ESLHSB7BETJALKF", ConcurrencyStamp = "c6b3372f-eb1d-4e2d-bcd7-1852eff09a2d" },
                new ApplicationUser { Id = "e0323333-b90c-48ab-b4ca-17cbf63231d5", FacultyName = null, UserName = "student7@gmail.com", NormalizedUserName = "STUDENT7@GMAIL.COM", Email = "student7@gmail.com", NormalizedEmail = "STUDENT7@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEIlYMV/4VsL1MMlEWtVvr9T0IIAezit7N9NuWcnsGQtd/kXfYx6dFYZLLtqzAh27yg==", SecurityStamp = "O5YNXENF2PICVCPCS2IBTAZU7JZZPUXS", ConcurrencyStamp = "a7954e4d-e6dd-49c8-80dd-5903b80fab9f" },
                new ApplicationUser { Id = "3ee3094c-e0ce-44d8-8746-e28245b63d7b", FacultyName = null, UserName = "student8@gmail.com", NormalizedUserName = "STUDENT8@GMAIL.COM", Email = "student8@gmail.com", NormalizedEmail = "STUDENT8@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEM9Ijsnz7H+/DJbWWWzVmFaZLRf6VoY970Gb4cxfTgbZ+YqntRyPdURiZGz7oSYQZw==", SecurityStamp = "3VF7XMHZ2WO6NCFQCCTWOWXCVT7U33NV", ConcurrencyStamp = "cabf1014-7e04-4a49-bac7-014e235628c6" },
                new ApplicationUser { Id = "8b3b94cf-0a9d-4e1b-a3fd-263a6474ada2", FacultyName = null, UserName = "student9@gmail.com", NormalizedUserName = "STUDENT9@GMAIL.COM", Email = "student9@gmail.com", NormalizedEmail = "STUDENT9@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEOu+jaX31jxxTTwAZHglNHQrpURqatIjB8jpO+AH0g1OftXGFwanZwrj2vmCAdbBBQ==", SecurityStamp = "RWAIPM2JLLYWSSQNKCZ6OKRKRMLVQFIJ", ConcurrencyStamp = "dc2eb935-ed26-4ac0-825c-76c5e4cde5fb" }
                );

            // Seed data for faculties table
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { FacultyID = "BUSI1632", FacultyName = "Business", CodeSubject = "BUSI1632" },
                new Faculty { FacultyID = "BUSI1633", FacultyName = "Business", CodeSubject = "BUSI1633" },
                new Faculty { FacultyID = "BUSI1702", FacultyName = "Business", CodeSubject = "BUSI1702" },
                new Faculty { FacultyID = "DESI1219", FacultyName = "Graphic Design", CodeSubject = "DESI1219" },
                new Faculty { FacultyID = "DESI1240", FacultyName = "Graphic Design", CodeSubject = "DESI1240" },
                new Faculty { FacultyID = "DESI1237", FacultyName = "Graphic Design", CodeSubject = "DESI1237" },
                new Faculty { FacultyID = "COMP1787", FacultyName = "IT", CodeSubject = "COMP1787" },
                new Faculty { FacultyID = "COMP1770", FacultyName = "IT", CodeSubject = "COMP1770" },
                new Faculty { FacultyID = "COMP1640", FacultyName = "IT", CodeSubject = "COMP1640" }
            );

            // Seed data for the academic year table
            modelBuilder.Entity<AcademicYear>().HasData(
                new AcademicYear { AcademicYearID = "BUSI1632", UploadDate = DateTime.Parse("2024-04-24 00:35:00.0000000"), ClosureDate = DateTime.Parse("2024-05-01 00:35:00.0000000"), FinalClosureDate = DateTime.Parse("2024-05-15 00:35:00.0000000"), FacultyID = "BUSI1632" },
                new AcademicYear { AcademicYearID = "BUSI1633", UploadDate = DateTime.Parse("2024-04-25 00:35:00.0000000"), ClosureDate = DateTime.Parse("2024-05-02 00:35:00.0000000"), FinalClosureDate = DateTime.Parse("2024-05-16 00:35:00.0000000"), FacultyID = "BUSI1633" },
                new AcademicYear { AcademicYearID = "BUSI1702", UploadDate = DateTime.Parse("2024-04-27 00:36:00.0000000"), ClosureDate = DateTime.Parse("2024-05-04 00:36:00.0000000"), FinalClosureDate = DateTime.Parse("2024-05-18 00:36:00.0000000"), FacultyID = "BUSI1702" },
                new AcademicYear { AcademicYearID = "COMP1640", UploadDate = DateTime.Parse("2024-05-15 00:37:00.0000000"), ClosureDate = DateTime.Parse("2024-05-22 00:37:00.0000000"), FinalClosureDate = DateTime.Parse("2024-06-05 00:37:00.0000000"), FacultyID = "COMP1640" },
                new AcademicYear { AcademicYearID = "COMP1770", UploadDate = DateTime.Parse("2024-05-11 00:37:00.0000000"), ClosureDate = DateTime.Parse("2024-05-18 00:37:00.0000000"), FinalClosureDate = DateTime.Parse("2024-06-01 00:37:00.0000000"), FacultyID = "COMP1770" },
                new AcademicYear { AcademicYearID = "COMP1787", UploadDate = DateTime.Parse("2024-05-04 00:37:00.0000000"), ClosureDate = DateTime.Parse("2024-05-11 00:37:00.0000000"), FinalClosureDate = DateTime.Parse("2024-05-25 00:37:00.0000000"), FacultyID = "COMP1787" },
                new AcademicYear { AcademicYearID = "DESI1219", UploadDate = DateTime.Parse("2024-04-28 00:36:00.0000000"), ClosureDate = DateTime.Parse("2024-05-05 00:36:00.0000000"), FinalClosureDate = DateTime.Parse("2024-05-19 00:36:00.0000000"), FacultyID = "DESI1219" },
                new AcademicYear { AcademicYearID = "DESI1237", UploadDate = DateTime.Parse("2024-05-01 00:36:00.0000000"), ClosureDate = DateTime.Parse("2024-05-08 00:36:00.0000000"), FinalClosureDate = DateTime.Parse("2024-05-22 00:37:00.0000000"), FacultyID = "DESI1237" },
                new AcademicYear { AcademicYearID = "DESI1240", UploadDate = DateTime.Parse("2024-04-29 00:36:00.0000000"), ClosureDate = DateTime.Parse("2024-05-06 00:36:00.0000000"), FinalClosureDate = DateTime.Parse("2024-05-20 00:36:00.0000000"), FacultyID = "DESI1240" }
            );

            // Seed data for contribution table
            modelBuilder.Entity<Contribution>().HasData(
                new Contribution { ContributionID = "Design Research Project", Title = " Design Research Project ", Content = "Create plans and methods to collect and analyze information to shape and develop new products, services or experiences.", Email = "216ca0d2-6672-4cc9-8760-752f58c9d318", StudentEmail = "", AcademicYearID = "DESI1219", IsPublic = false, },
                new Contribution { ContributionID = "Enterprise Web Software Development", Title = "Enterprise Web Software Development", Content = "Building a secure web-enabled role-based system to collect innovative ideas from staff in a large University.", Email = "216ca0d2-6672-4cc9-8760-752f58c9d318", StudentEmail = "", AcademicYearID = "COMP1640", IsPublic = false },
                new Contribution { ContributionID = "Interdisciplinary Design", Title = "Interdisciplinary Design", Content = "combines methods, knowledge and resources from many different fields to create innovative solutions to complex challenges.", Email = "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", StudentEmail = "", AcademicYearID = "DESI1237", IsPublic = false },
                new Contribution { ContributionID = "Negotiations", Title = "Negotiations", Content = "The process of negotiating between parties to reach an agreement or resolve a problem in a harmonious manner that benefits all parties involved.", Email = "216ca0d2-6672-4cc9-8760-752f58c9d318", StudentEmail = "", AcademicYearID = "BUSI1632", IsPublic = false },
                new Contribution { ContributionID = "Organisational Decision Making", Title = "Organisational Decision Making", Content = "The process of choosing between options to achieve specific organizational goals, based on information, solutions, and internal and external factors.", Email = "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", StudentEmail = "", AcademicYearID = "BUSI1702", IsPublic = false },
                new Contribution { ContributionID = "Professional Practice & Portfolio ", Title = "Professional Practice & Portfolio ", Content = "Build and manage a collection of projects, skills and personal achievements to demonstrate competence and experience in the area of expertise.", Email = "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", StudentEmail = "", AcademicYearID = "DESI1240", IsPublic = false },
                new Contribution { ContributionID = "Professional Project Management ", Title = "Professional Project Management ", Content = "Organize and direct resources to achieve specific project goals.", Email = "216ca0d2-6672-4cc9-8760-752f58c9d318", StudentEmail = "", AcademicYearID = "COMP1770", IsPublic = false },
                new Contribution { ContributionID = "Requirements Management", Title = "Requirements Management", Content = "Identify, monitor and control project requirements to ensure that the product or service developed meets those requirements.", Email = "216ca0d2-6672-4cc9-8760-752f58c9d318", StudentEmail = "", AcademicYearID = "COMP1787", IsPublic = false },
                new Contribution { ContributionID = "Strategy for Managers", Title = "Strategy for Managers", Content = "Long-term planning to shape and achieve organizational goals through analysis of the organization's environment, resources, and capabilities.", Email = "216ca0d2-6672-4cc9-8760-752f58c9d318", StudentEmail = "", AcademicYearID = "BUSI1633", IsPublic = false }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "216ca0d2-6672-4cc9-8760-752f58c9d318",
                    RoleId = "0a29fc34-cf49-477d-8e45-0199d8a8ca14"
                },
                new IdentityUserRole<string>
                {
                    UserId = "47d2b6a5-9b9c-4703-9d06-7505ac14d08f",
                    RoleId = "0a29fc34-cf49-477d-8e45-0199d8a8ca14"
                },
                new IdentityUserRole<string>
                {
                    UserId = "e56ba4dd-0143-4221-84f3-c34e34e0e14f",
                    RoleId = "0bd06470-6120-4334-86c5-2d1fcd75f514"
                },
                new IdentityUserRole<string>
                {
                    UserId = "c2512fdf-cab0-423e-87a4-2d33ab8a5a74",
                    RoleId = "0bd06470-6120-4334-86c5-2d1fcd75f514"
                },
                new IdentityUserRole<string>
                {
                    UserId = "90316e26-012d-4c0c-939c-2dee25cf8183",
                    RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                },
                new IdentityUserRole<string>
                {
                    UserId = "e33f9319-c5b6-4e98-893e-565327621838",
                    RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                },
                new IdentityUserRole<string>
                {
                    UserId = "a94809be-838f-4f8a-bccc-003f5f6025ec",
                    RoleId = "cb316555-3aec-4234-aea9-1b4e22add4fb"
                },
                new IdentityUserRole<string>
                {
                    UserId = "f5719d35-67c3-4458-8b3f-dc5c410bee91",
                    RoleId = "cb316555-3aec-4234-aea9-1b4e22add4fb"
                },
                  new IdentityUserRole<string>
                  {
                      UserId = "03bc6e64-e6bc-4bfb-93ec-d8380795c88d",
                      RoleId = "ec5e815d-2ae2-4a7d-a687-fc7502e05560"
                  },
                   new IdentityUserRole<string>
                   {
                       UserId = "92de70df-e818-41d9-9d24-4782eb838fe8",
                       RoleId = "ec5e815d-2ae2-4a7d-a687-fc7502e05560"
                   },



                   new IdentityUserRole<string>
                   {
                       UserId = "d67405f3-aecc-4873-a19e-ced3fcbeac1d",
                       RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                   },
                     new IdentityUserRole<string>
                     {
                         UserId = "e1c5de51-1cf3-41fb-809f-734ca787c863",
                         RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                     },
                       new IdentityUserRole<string>
                       {
                           UserId = "2f3c0a4c-c42e-44e3-b08f-fd446ada9e4b",
                           RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                       },
                         new IdentityUserRole<string>
                         {
                             UserId = "54bba5fb-57f9-49f5-9d92-5a06eedb8fc4",
                             RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                         },
                           new IdentityUserRole<string>
                           {
                               UserId = "b7244936-fb02-432a-8bc6-e27d4a8a9ab0",
                               RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                           },
                             new IdentityUserRole<string>
                             {
                                 UserId = "e0323333-b90c-48ab-b4ca-17cbf63231d5",
                                 RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                             },
                               new IdentityUserRole<string>
                               {
                                   UserId = "3ee3094c-e0ce-44d8-8746-e28245b63d7b",
                                   RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                               },
                                 new IdentityUserRole<string>
                                 {
                                     UserId = "8b3b94cf-0a9d-4e1b-a3fd-263a6474ada2",
                                     RoleId = "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2"
                                 }




            );


        }

    }
}
