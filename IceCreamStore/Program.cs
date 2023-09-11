using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IceCreamDb>(opt => opt.UseInMemoryDatabase("IceCreamList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Welcome to the Ice Cream Store!");

RouteGroupBuilder IceCreamItems = app.MapGroup("/IceCream");

IceCreamItems.MapGet("/", GetAllIceCream);
IceCreamItems.MapGet("/instock", GetInStockIceCream);
IceCreamItems.MapPost("/purchase", PurchaseIceCream);
IceCreamItems.MapGet("/{id}", GetIceCream);
IceCreamItems.MapPost("/", CreateIceCream);
IceCreamItems.MapPut("/{id}", UpdateIceCream);
IceCreamItems.MapDelete("/{id}", DeleteIceCream);

app.Run();

static async Task<IResult> GetAllIceCream(IceCreamDb db)
{
    return TypedResults.Ok(await db.IceCreams.Select(x => new IceCreamDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetInStockIceCream(IceCreamDb db) {
    return TypedResults.Ok(await db.IceCreams.Select(x => new IceCreamDTO(x)).Where(t => t.IsInStock).ToListAsync());
}

static async Task<IResult> GetIceCream(int id, IceCreamDb db)
{
    return await db.IceCreams.FindAsync(id)
        is IceCream IceCream
            ? TypedResults.Ok(new IceCreamDTO(IceCream))
            : TypedResults.NotFound();
}

static async Task<IResult> CreateIceCream(IceCreamDTO IceCreamDTO, IceCreamDb db)
{
    var IceCreamItem = new IceCream
    {
        ScoopsInStock = IceCreamDTO.ScoopsInStock,
        Flavor = IceCreamDTO.Flavor
    };

    db.IceCreams.Add(IceCreamItem);
    await db.SaveChangesAsync();

    IceCreamDTO = new IceCreamDTO(IceCreamItem);

    return TypedResults.Created($"/IceCream/{IceCreamItem.Id}", IceCreamDTO);
}

static async Task<IResult> UpdateIceCream(int id, IceCreamDTO IceCreamDTO, IceCreamDb db)
{
    var IceCream = await db.IceCreams.FindAsync(id);

    if (IceCream is null) return TypedResults.NotFound();

    IceCream.Flavor = IceCreamDTO.Flavor;
    IceCream.ScoopsInStock = IceCreamDTO.ScoopsInStock;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteIceCream(int id, IceCreamDb db)
{
    if (await db.IceCreams.FindAsync(id) is IceCream IceCream)
    {
        db.IceCreams.Remove(IceCream);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

static async Task<IResult> PurchaseIceCream(int id, int scoops, IceCreamDb db) {
    if (await db.IceCreams.FindAsync(id) is IceCream IceCream)
    {
        if (IceCream.ScoopsInStock < scoops)
            return TypedResults.BadRequest("Not enough stock.");
            
        IceCream.ScoopsInStock -= scoops;
        
        await db.SaveChangesAsync();
        return TypedResults.Ok(new IceCreamDTO(IceCream));
    }
    return TypedResults.NotFound();
}