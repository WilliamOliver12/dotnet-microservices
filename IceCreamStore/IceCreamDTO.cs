public class IceCreamDTO
{
    public int Id { get; set; }
    public string? Flavor { get; set; }
    public int ScoopsInStock { get;set; }
    public bool IsInStock => ScoopsInStock > 0;


    public IceCreamDTO() { }

    public IceCreamDTO(IceCream IceCream) =>
        (Id, Flavor, ScoopsInStock) = (IceCream.Id, IceCream.Flavor, IceCream.ScoopsInStock);
}