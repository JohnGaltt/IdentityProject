namespace _19._10._2017evening.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixloginbug : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "RegisteredDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RegisteredDate", c => c.DateTime(nullable: false));
        }
    }
}
