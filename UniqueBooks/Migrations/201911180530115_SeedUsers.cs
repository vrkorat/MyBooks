namespace UniqueBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e6b768b2-6d6e-4b0a-80c4-a8a7c8d11802', N'admin@domain.com', 0, N'AIzadrU/OOPa4ZrFCrFSlIVaCR3LbyYZY8NN23GewY+/4NWwB8A+eqnM4CEEUXuG5A==', N'605db280-cf08-4fa3-bd47-2f8a90f8aba2', NULL, 0, 0, NULL, 1, 0, N'admin@domain.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e7afb1fb-ac4e-4995-ba07-6362154d8e15', N'guest@domain.com', 0, N'AMuAB+5Wu5Za3ChkcA7Td9xjGnuEzTW5M03tEqUYN4LB2Y64YQkRdDBttnoz8HDmBA==', N'873bef9e-a421-424a-9fa1-8654d4c71369', NULL, 0, 0, NULL, 1, 0, N'guest@domain.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8363d971-bc00-4445-828d-dee49b8893d9', N'CanManageBook')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e6b768b2-6d6e-4b0a-80c4-a8a7c8d11802', N'8363d971-bc00-4445-828d-dee49b8893d9')

");
        }

        public override void Down()
        {
        }
    }
}
