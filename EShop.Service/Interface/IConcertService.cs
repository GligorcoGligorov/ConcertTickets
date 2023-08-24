using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Interface
{
    public interface IConcertService
    {
        List<Concert> GetAllConcerts();
        Concert GetDetailsForConcert(Guid? id);
        void CreateNewConcert(Concert p);
        void UpdeteExistingConcert(Concert p);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);
        void DeleteConcert(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
