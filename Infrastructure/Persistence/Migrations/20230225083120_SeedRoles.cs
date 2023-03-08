using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringService.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [BirthDay], [FirstName], [IdentificationNumber], [IsDeleted], [LastLogin], [LastName], [RegistrationDate]) VALUES (N'000ae0e8-5ce5-4b8d-8344-e461ad358b25', N'cook@cateringservice.com', N'COOK@CATERINGSERVICE.COM', N'cook@cateringservice.com', N'COOK@CATERINGSERVICE.COM', 0, N'AQAAAAEAACcQAAAAEFD1UaxdiRpqLDancfaqVw0QHKPtjk+ASiyfUhuv95Je37E1ZQ9J2p3oS9DoBUKCkQ==', N'5RDUEGAH5YRK4E6K7ZQC3HXSDFWYYOKJ', N'0beeb01e-83a9-4000-a1f5-b9058731bdb0', N'+355 68 411 8356', 0, 0, NULL, 1, 0, N'1975-02-19 00:00:00', N'Cook', N'K293854332O', 0, N'2023-02-23 18:59:48', N'Chef', N'2023-02-23 18:59:48')
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [BirthDay], [FirstName], [IdentificationNumber], [IsDeleted], [LastLogin], [LastName], [RegistrationDate]) VALUES (N'2223137b-8770-4528-a2d8-36620f6999ba', N'employee@cateringservice.com', N'EMPLOYEE@CATERINGSERVICE.COM', N'employee@cateringservice.com', N'EMPLOYEE@CATERINGSERVICE.COM', 0, N'AQAAAAEAACcQAAAAEK6pZ9sbPSsB+RshZKYgULYA0omFkvAou283cczWKc6OdjBt5mtkcavl3P5tUzIDvw==', N'2E74FX24RKJRFS4SA4FC52ZROVWBUWCY', N'b1cb8be6-95f2-4fc7-983d-dcee05dab059', N'+355 68 411 9037', 0, 0, NULL, 1, 0, N'1999-07-23 00:00:00', N'Employee', N'K293854332O', 0, N'2023-02-23 19:02:26', N'Employee', N'2023-02-23 19:02:26')
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [BirthDay], [FirstName], [IdentificationNumber], [IsDeleted], [LastLogin], [LastName], [RegistrationDate]) VALUES (N'b357acb3-5eba-4583-bc27-935f9c6605fb', N'admin@cateringservice.com', N'ADMIN@CATERINGSERVICE.COM', N'admin@cateringservice.com', N'ADMIN@CATERINGSERVICE.COM', 0, N'AQAAAAEAACcQAAAAECgz5+eX7r6aUh5I+kjweV1otuKpdqF5XthQlCe3P3bFqOmu1YChBLLTgaX6ExbbdQ==', N'SH44MF2QY3AQV2YOZS6BOVSOSSQGUZAY', N'c38ce7f9-5360-45a2-8fb7-01b4492e24ce', N'+000000000000', 0, 0, NULL, 1, 0, N'2023-02-23 00:00:00', N'Admin', N'adminId', 0, N'2023-02-23 19:01:12', N'Admin', N'2023-02-23 19:01:12')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'316a7cce-d8ea-4528-a134-807734cbca0a', N'employee', N'EMPLOYEE', N'dafe28ee-e171-4720-bc1e-213e71dbd069')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'573287e5-3d71-455a-8aa5-916f6985b732', N'admin', N'ADMIN', N'3e7ae574-78cf-4e87-b208-03139e0d9b63')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6b888015-5140-4d65-9fc2-28988e9e962e', N'cook', N'COOK', N'ffccc24a-2e6d-4bcf-9980-7cc3ee5516d0')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2223137b-8770-4528-a2d8-36620f6999ba', N'316a7cce-d8ea-4528-a134-807734cbca0a')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b357acb3-5eba-4583-bc27-935f9c6605fb', N'573287e5-3d71-455a-8aa5-916f6985b732')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'000ae0e8-5ce5-4b8d-8344-e461ad358b25', N'6b888015-5140-4d65-9fc2-28988e9e962e')

");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
