namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNameColumnInMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Pay as you go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE DurationInMonths = 2");
            Sql("UPDATE MembershipTypes SET Name = 'Quarterly' WHERE DurationInMonths = 3");
            Sql("UPDATE MembershipTypes SET Name = 'Yearly' WHERE DurationInMonths = 4");
        }                                                                                                                  
        
        public override void Down()
        {
        }
    }
}
