namespace _19._10._2017evening.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bindforstudydate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StudyDateString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "StudyDateString");
        }
    }
}
