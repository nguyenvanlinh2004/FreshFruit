using Microsoft.AspNetCore.Mvc;
using FreshFruit.Services;

public class CartCountViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartCountViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IViewComponentResult Invoke()
    {
        int cartCount =(int) _cartService.GetTotalQuantity();
        return View(cartCount);
    }
}
