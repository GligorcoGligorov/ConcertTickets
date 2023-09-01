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
        private readonly IRepository<EmailMessage> _mailRepository;

        private readonly IRepository<ConcertInOrder> _concertInOrderRepository;
        private readonly IUserRepository _userRepository;



        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<EmailMessage> mailRepository, IUserRepository userRepository, IRepository<Order> orderCartRepository, IRepository<ConcertInOrder> concertInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderCartRepository = orderCartRepository;
            _concertInOrderRepository = concertInOrderRepository;
            _mailRepository = mailRepository;
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

                EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Successfully created order";
                message.Status = false;

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
                    UserOrder = order,
                    Quantity = z.Quantity
               
                }).ToList();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Your order is completed. The order contains: ");

                var totalPrice = 0.0;

                for(int i = 1;i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    totalPrice += item.Quantity * item.OrderedConcert.ConcertPrice;
                    sb.AppendLine(i.ToString() + ". " + item.OrderedConcert.ConcertName + " with price of: " + item.OrderedConcert.ConcertPrice + " and quantity of: " + item.Quantity);
                }

                sb.AppendLine("Total Price: " + totalPrice.ToString());

                message.Content = sb.ToString();

                concertInOrders.AddRange(result);

                foreach (var item in concertInOrders)
                {
                    this._concertInOrderRepository.Insert(item);
                }


                loggedInUser.UserCart.ConcertInShoppingCarts.Clear();

                this._mailRepository.Insert(message);

                this._userRepository.Update(loggedInUser);
                return true;

            }
            return false;
        }
    }
}
