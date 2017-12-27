namespace DataCollection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "WeChatId", c => c.String());
            DropColumn("dbo.AspNetUsers", "WeiXinId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "WeiXinId", c => c.String());
            DropColumn("dbo.AspNetUsers", "WeChatId");
        }
    }
}
