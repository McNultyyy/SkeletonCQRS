namespace SkeletonCQRS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Guid(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        AggregateId = c.Guid(nullable: false),
                        EventName = c.String(),
                        EventData = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Event");
        }
    }
}
