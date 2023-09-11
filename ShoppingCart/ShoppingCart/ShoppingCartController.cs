using Microsoft.AspNetCore.Mvc;
using ShoppingCart;

namespace ShoppingCart.ShoppingCart
{
    [Route("/shoppingcart")]
    public class ShoppingCartController: ControllerBase
    {
        private readonly IShoppingCartStore _shoppingCartStore;
        private readonly IProductCatalogClient _productCatalogClient;
        private readonly IEventStore _eventStore;

        public ShoppingCartController(IShoppingCartStore shoppingCartStore, IProductCatalogClient productCatalogClient, IEventStore eventStore) {
            _shoppingCartStore = shoppingCartStore;
            _productCatalogClient = productCatalogClient;
            _eventStore = eventStore;
        }

        [HttpGet("{userId:int}")]
        public ShoppingCart Get(int userId) => this.shoppingCartStore.Get(userId);

        [HttpPost]
        public ShoppingCart Post([FromBody]int[] productIds)

    }    
}