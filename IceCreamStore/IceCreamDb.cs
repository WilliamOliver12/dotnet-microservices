using Microsoft.EntityFrameworkCore;

class IceCreamDb : DbContext
{
    public IceCreamDb(DbContextOptions<IceCreamDb> options)
        : base(options) { }

    public DbSet<IceCream> IceCreams => Set<IceCream>();
}