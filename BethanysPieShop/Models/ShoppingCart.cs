using BethanysPieShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class ShoppingCart
    {

        // Passing the Shopping cart via the constructor injector
        private readonly AppDbContext _appDbContext;

        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

       // private IEnumerable<ShoppingCartItem> _shoppingCartItems;
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        // Get Shopping CartItem
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? 
                (ShoppingCartItems = 
                    _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                        .Include(s => s.Pie)
                        .ToList());
        }

       // public decimal CartTotal => ShoppingCartItems.Sum(item => item.Pie.Price * item.Amount);

        // Shopping cart total
        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }



        // Get Cart
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

            AppDbContext context = services.GetService<AppDbContext>();

            var cartId = string.IsNullOrEmpty(session.GetString("CartId")) ? Guid.NewGuid().ToString() : session.GetString("CartId");

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        // Add to Cart
        public void AddToCart(Pie pie)
        {
            ShoppingCartItem shoppingCartItem = _appDbContext.ShoppingCartItems
                .SingleOrDefault(item => item.Pie.PieId == pie.PieId && item.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _appDbContext.SaveChanges();
        }

        // Remove From Cart
        public void RemoveFromCart(Pie pie)
        {
            ShoppingCartItem shoppingCartItem = _appDbContext.ShoppingCartItems.SingleOrDefault(item => item.Pie.PieId == pie.PieId);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 0)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
                _appDbContext.SaveChanges();
            }
        }

        //Clear Shopping Cart
      //  public void ClearCart()
      //  {
        //    _appDbContext.ShoppingCartItems.RemoveRange(ShoppingCartItems);
        //    _appDbContext.SaveChanges();
       // }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _appDbContext.SaveChanges();
        }
    }
}
