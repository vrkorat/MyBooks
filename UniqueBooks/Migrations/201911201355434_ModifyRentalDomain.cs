namespace UniqueBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyRentalDomain : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rentals", name: "Book_Id", newName: "BookId");
            RenameColumn(table: "dbo.Rentals", name: "Customer_Id", newName: "CustomerId");
            RenameIndex(table: "dbo.Rentals", name: "IX_Book_Id", newName: "IX_BookId");
            RenameIndex(table: "dbo.Rentals", name: "IX_Customer_Id", newName: "IX_CustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rentals", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_BookId", newName: "IX_Book_Id");
            RenameColumn(table: "dbo.Rentals", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Rentals", name: "BookId", newName: "Book_Id");
        }
    }
}
