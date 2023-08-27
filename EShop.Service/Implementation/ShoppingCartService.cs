using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderCartRepository;
        private readonly IRepository<ConcertInOrder> _concertInOrderRepository;
        private readonly IUserRepository _userRepository;



        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderCartRepository, IRepository<ConcertInOrder> concertInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderCartRepository = orderCartRepository;
            _concertInOrderRepository = concertInOrderRepository;
        }

        public bool deleteProductFromSoppingCart(string userId, Guid concertId)
        {
            if (!string.IsNullOrEmpty(userId) && concertId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.ConcertInShoppingCarts.Where(z => z.ConcertId.Equals(concertId)).FirstOrDefault();
                userShoppingCart.ConcertInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;
            var AllConcerts = userShoppingCart.ConcertInShoppingCarts.ToList();
            var AllConcertPrice = AllConcerts.Select(z => new
            {
                ProductPrice = z.Concert.ConcertPrice,
                Quantity = z.Quantity
            }).ToList();

            var totalPrice = 0;
            foreach (var item in AllConcertPrice)
            {
                totalPrice += item.Quantity * item.ProductPrice;
            }

            ShoppingCartDto scDto = new ShoppingCartDto
            {
                Concerts = AllConcerts,
                TotalPrice = totalPrice
            };

            return scDto;
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderCartRepository.Insert(order);

                List<ConcertInOrder> concertInOrders = new List<ConcertInOrder>();

                var result = userShoppingCart.ConcertInShoppingCarts.Select(z => new ConcertInOrder
                {
                    ConcertId = z.Concert.Id,
                    OrderedConcert = z.Concert,
                    OrderId = order.Id,
                    UserOrder = order
                }).ToList();

                concertInOrders.AddRange(result);

                foreach (var item in concertInOrders)
                {
                    this._concertInOrderRepository.Insert(item);
                }


                loggedInUser.UserCart.ConcertInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                return true;

            }
            return false;
        }
    }
}
