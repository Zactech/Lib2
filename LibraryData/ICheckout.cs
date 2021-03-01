using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public interface ICheckout
    {
        IEnumerable<Checkout1> GetAll();
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);
        IEnumerable<Hold> GetCurrentHolds(int id);

        Checkout1 GetById(int checkoutId);
        Checkout1 GetLatestCheckout(int assetId);

        void Add(Checkout1 newCheckout);
        void CheckOutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId);
        void PlaceHold(int assetId, int libraryCardId);
        void MarkLost(int assetId);
        void MarkFound(int assetId);

        bool isCheckedOut(int id);

        string GetCurrentHoldPatronName(int id);
        string GetCurrentCheckoutPatron(int id);
        DateTime GetCurrentHoldPlaced(int id);

        

    }
}
