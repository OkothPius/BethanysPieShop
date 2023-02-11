using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository, ShoppingCart shoppingCart)
        {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            Pie pie = _pieRepository.AllPies.SingleOrDefault(p => p.PieId == pieId);
            if (pie != null)
            {
                _shoppingCart.AddToCart(pie);
            }
            return RedirectToAction(nameof(Index));
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            Pie pie = _pieRepository.AllPies.SingleOrDefault(p => p.PieId == pieId);
            if (pie != null)
            {
                _shoppingCart.RemoveFromCart(pie);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
