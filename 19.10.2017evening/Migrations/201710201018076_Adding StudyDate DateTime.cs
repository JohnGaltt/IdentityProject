namespace _19._10._2017evening.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStudyDateDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StudyDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "StudyDate");
        }
    }
}
