using Models;
using System;

namespace BusinessLogicLayer.Services
{
    public interface IOfferService
    {
        void CreateWordOffer(Client client, DateOnly date);
    }
}