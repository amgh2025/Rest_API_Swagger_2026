WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

List<Book> books = new List<Book>
{
    new Book(1, "Clean Code", "Robert C. Martin"),
    new Book(2, "Design Patterns", "Erich Gamma")
};

app.MapGet("/", () => "Library API is running");

app.MapGet("/books", () =>
{
    return books;
});

app.MapGet("/books/{id}", (int id) =>
{
    Book? book = books.FirstOrDefault(b => b.Id == id);

    if (book == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(book);
});


// Add REST POST
app.MapPost("/books", (Book newBook) =>
{
    books.Add(newBook);
    return Results.Ok(newBook);
});

//

// ADD REST PUT
app.MapPut("/books/{id}", (int id, Book updatedBook) =>
{
    int index = books.FindIndex(b => b.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    books[index] = updatedBook;

    return Results.Ok(updatedBook);
});
//


// ADD REST DELETE
app.MapDelete("/books/{id}", (int id) =>
{
    Book? book = books.FirstOrDefault(b => b.Id == id);

    if (book == null)
    {
        return Results.NotFound();
    }

    books.Remove(book);

    return Results.Ok();
});

//

app.Run();

public record Book(int Id, string Title, string Author);
