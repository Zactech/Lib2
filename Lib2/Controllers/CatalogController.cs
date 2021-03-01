using Lib2.Models.Catalog;
using Lib2.Models.Checkout;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace Lib2.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAssest _assets;
        private ICheckout _checkouts;
        public CatalogController(ILibraryAssest asset, ICheckout checkout)
        {
            _assets = asset;
            _checkouts = checkout;
        }

        public IActionResult Index()
        {
            var assetModels = _assets.GetAll();

            var listingResult = assetModels.Select(result => new AssetIndexLisitingModel {
                Id = result.Id, ImageUrl = result.ImageUrl, AuthorOrDirector = _assets.GetAuthorOrDirector(result.Id), DeweyCallNumber = _assets.GetDeweyIndex(result.Id), Title = result.Title,
                Type = _assets.GetType(result.Id) });

            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };

            return View(model);
            
        }

        public IActionResult Detail(int id)
        {
            var asset = _assets.GetById(id);

            var currentHolds = _checkouts.GetCurrentHolds(id).Select(a => new AssetHoldModel
            {
                HoldPlaced = _checkouts.GetCurrentHoldPlaced(a.Id).ToString("d"),
                PatronName = _checkouts.GetCurrentHoldPatronName(a.Id)
            });

            var model = new AssetDetailModel
            {
                AssetId = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost.ToString(),
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _assets.GetAuthorOrDirector(id),
                CurrentLocation = _assets.GetCurrentLocation(id).Name,
                DeweyCallNumber = _assets.GetDeweyIndex(id),
                ISBN = _assets.GetIsbn(id),
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                PatronName = _checkouts.GetCurrentCheckoutPatron(id),
                CurrentHolds = currentHolds

            };

            return View(model);
        }
        public IActionResult Checkout(int id)
        {
            var assest = _assets.GetById(id);

            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = assest.ImageUrl,
                Title = assest.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.isCheckedOut(id),
                HoldCount = _checkouts.GetCurrentHolds(id).Count()
            };

            return View(model); 
        }

        public IActionResult CheckIn (int id)
        { 
            _checkouts.CheckInItem(id);
            return RedirectToAction("Detail", new { id = id });
        }

        public IActionResult Hold (int id)
        {
            var asset = _assets.GetById(id);

            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.isCheckedOut(id),
                HoldCount=_checkouts.GetCurrentHolds(id).Count()
            };

            return View(model);
        }
        
        public IActionResult MarkLost(int assetId)
        {
            _checkouts.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

         
        public IActionResult MarkFound (int assetId)
        {
            _checkouts.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int librarycardId)
        {
            _checkouts.CheckOutItem(assetId, librarycardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int librarycardId)
        {
            _checkouts.PlaceHold(assetId, librarycardId);
            return RedirectToAction("Detail", new { id = assetId });
        }
    }
}
