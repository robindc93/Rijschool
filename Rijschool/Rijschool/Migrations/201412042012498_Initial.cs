namespace Rijschool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bestelling",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BestelDatum = c.DateTime(nullable: false),
                        Betaald = c.Boolean(nullable: false),
                        Klant_Id = c.String(nullable: false, maxLength: 128),
                        Lespakket_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Klant_Id)
                .ForeignKey("dbo.Lespakket", t => t.Lespakket_Id)
                .Index(t => t.Klant_Id)
                .Index(t => t.Lespakket_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Familienaam = c.String(),
                        Voornaam = c.String(),
                        Adres = c.String(),
                        Gemeente = c.String(),
                        KlantSedert = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Lespakket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(),
                        Kostprijs = c.Int(nullable: false),
                        AantalBlokken = c.Int(nullable: false),
                        TypeRijbewijs_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rijbewijs", t => t.TypeRijbewijs_Id)
                .Index(t => t.TypeRijbewijs_Id);
            
            CreateTable(
                "dbo.Rijbewijs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Les",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        UurStart = c.DateTime(nullable: false),
                        UurEinde = c.DateTime(nullable: false),
                        Bestelling_Id = c.Int(nullable: false),
                        Instructeur_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bestelling", t => t.Bestelling_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Instructeur_Id)
                .Index(t => t.Bestelling_Id)
                .Index(t => t.Instructeur_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.InstructeurRijbewijs",
                c => new
                    {
                        Instructeur_Id = c.String(nullable: false, maxLength: 128),
                        Rijbewijs_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructeur_Id, t.Rijbewijs_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.Instructeur_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rijbewijs", t => t.Rijbewijs_Id, cascadeDelete: true)
                .Index(t => t.Instructeur_Id)
                .Index(t => t.Rijbewijs_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bestelling", "Lespakket_Id", "dbo.Lespakket");
            DropForeignKey("dbo.Lespakket", "TypeRijbewijs_Id", "dbo.Rijbewijs");
            DropForeignKey("dbo.InstructeurRijbewijs", "Rijbewijs_Id", "dbo.Rijbewijs");
            DropForeignKey("dbo.InstructeurRijbewijs", "Instructeur_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Les", "Instructeur_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Les", "Bestelling_Id", "dbo.Bestelling");
            DropForeignKey("dbo.Bestelling", "Klant_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InstructeurRijbewijs", new[] { "Rijbewijs_Id" });
            DropIndex("dbo.InstructeurRijbewijs", new[] { "Instructeur_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Les", new[] { "Instructeur_Id" });
            DropIndex("dbo.Les", new[] { "Bestelling_Id" });
            DropIndex("dbo.Lespakket", new[] { "TypeRijbewijs_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Bestelling", new[] { "Lespakket_Id" });
            DropIndex("dbo.Bestelling", new[] { "Klant_Id" });
            DropTable("dbo.InstructeurRijbewijs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Les");
            DropTable("dbo.Rijbewijs");
            DropTable("dbo.Lespakket");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Bestelling");
        }
    }
}
