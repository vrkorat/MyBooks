namespace UniqueBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNameOfGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (1, 'Autobiography')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (2, 'Textbook')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (3, 'Science')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (4, 'Fairytale')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (5, 'Others')");
        }
        
        public override void Down()
        {
        }
    }
}
