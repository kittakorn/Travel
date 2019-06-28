using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelWeb.Helpers;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    [Authorize]
    public class PlacesController : Controller
    {
        private readonly DatabaseEntities _db = new DatabaseEntities();

        public async Task<ActionResult> Index()
        {
            var places = _db.Places.Include(p => p.Category).Include(p => p.Province);
            return View(await places.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Place place = await _db.Places.FindAsync(id);
            if (place == null)
                return HttpNotFound();
            return View(place);
        }

        public ActionResult Create()
        {
            ViewBag.PlaceCategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
            ViewBag.PlaceProvinceId = new SelectList(_db.Provinces, "ProvinceId", "ProvinceName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Place place, HttpPostedFileBase[] placeImageList)
        {
            if (ModelState.IsValid)
            {
                place.PlaceImage = Extension.UploadImage(placeImageList);
                _db.Places.Add(place);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceCategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName", place.PlaceCategoryId);
            ViewBag.PlaceProvinceId = new SelectList(_db.Provinces, "ProvinceId", "ProvinceName", place.PlaceProvinceId);
            return View(place);
        }

        // GET: Places/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Place place = await _db.Places.FindAsync(id);
            if (place == null)
                return HttpNotFound();
            ViewBag.PlaceCategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName", place.PlaceCategoryId);
            ViewBag.PlaceProvinceId = new SelectList(_db.Provinces, "ProvinceId", "ProvinceName", place.PlaceProvinceId);
            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Place place, HttpPostedFileBase[] placeImageList)
        {
            if (ModelState.IsValid)
            {
                place.PlaceImage = placeImageList[0] != null ? Extension.UploadImage(placeImageList, place.PlaceImage) : place.PlaceImage;
                _db.Entry(place).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceCategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName", place.PlaceCategoryId);
            ViewBag.PlaceProvinceId = new SelectList(_db.Provinces, "ProvinceId", "ProvinceName", place.PlaceProvinceId);
            return View(place);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Place place = await _db.Places.FindAsync(id);
            if (place == null)
                return HttpNotFound();
            _db.Places.Remove(place);
            try
            {
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        public async Task<ActionResult> DeleteImage(int id, string imageName)
        {
            Place place = await _db.Places.FindAsync(id);
            if (place != null && place.PlaceImage.Split(',').Count() >= 2)
            {
                place.PlaceImage = Extension.DeleteImage(imageName, place.PlaceImage.Split(',').ToList());
                _db.Entry(place).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Edit", new { id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
