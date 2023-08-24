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
    public class ConcertService : IConcertService
    {
        private readonly IRepository<Concert> _concertRepository;
        private readonly IRepository<ConcertInShoppingCart> _concertInShoppingCartRepository;

        private readonly IUserRepository _userRepository;


        public ConcertService(IRepository<Concert> concertRepository, IUserRepository userRepository)
        {
            _concertRepository = concertRepository;
            _userRepository = userRepository;
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);
            var userShoppingCard = user.UserCart;

            if (item.ConcertId != null && userShoppingCard != null)
            {
                var concert = this.GetDetailsForConcert(item.ConcertId);
                if (concert != null)
                {
                    ConcertInShoppingCart itemToAdd = new ConcertInShoppingCart
                    {
                        Concert = concert,
                        ConcertId = concert.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._concertInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;

            }

            return false;
        }

        public void CreateNewConcert(Concert c)
        {
            this._concertRepository.Insert(c);
        }

        public void DeleteConcert(Guid id)
        {
            var concert = this.GetDetailsForConcert(id);
            this._concertRepository.Delete(concert);
        }

        public List<Concert> GetAllConcerts()
        {
            return this._concertRepository.GetAll().ToList();
        }

        public Concert GetDetailsForConcert(Guid? id)
        {
            return this._concertRepository.Get(id);
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var concert = this.GetDetailsForConcert(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedConcert = concert,
                ConcertId = concert.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingConcert(Concert c)
        {
            this._concertRepository.Update(c);
        }
    }
}
