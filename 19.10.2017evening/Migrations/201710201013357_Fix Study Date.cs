namespace _19._10._2017evening.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixStudyDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "YearStudy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "YearStudy", c => c.String());
        }
    }
}
