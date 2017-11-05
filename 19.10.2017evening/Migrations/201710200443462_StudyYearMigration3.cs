namespace _19._10._2017evening.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudyYearMigration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "YearStudy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "YearStudy");
        }
    }
}
