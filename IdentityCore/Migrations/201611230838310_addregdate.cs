namespace IdentityCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addregdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
        }
    }
}
