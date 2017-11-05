namespace _19._10._2017evening.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Registereddateadded2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegisteredDateString", c => c.String());
            AlterColumn("dbo.AspNetUsers", "RegisteredDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "RegisteredDate", c => c.String());
            DropColumn("dbo.AspNetUsers", "RegisteredDateString");
        }
    }
}
