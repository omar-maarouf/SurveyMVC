namespace SurveyMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userslinking : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Surveys", "AdminId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Responses", "EmployeeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Surveys", "AdminId");
            CreateIndex("dbo.Responses", "EmployeeId");
            AddForeignKey("dbo.Surveys", "AdminId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Responses", "EmployeeId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Surveys", "AdminId", "dbo.AspNetUsers");
            DropIndex("dbo.Responses", new[] { "EmployeeId" });
            DropIndex("dbo.Surveys", new[] { "AdminId" });
            AlterColumn("dbo.Responses", "EmployeeId", c => c.String());
            AlterColumn("dbo.Surveys", "AdminId", c => c.String());
        }
    }
}
