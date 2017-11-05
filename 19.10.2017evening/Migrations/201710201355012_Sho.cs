namespace _19._10._2017evening.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sho : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "StudyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "StudyDate", c => c.DateTime(nullable: false));
        }
    }
}
