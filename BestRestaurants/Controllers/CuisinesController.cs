using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BestRestaurant.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BestRestaurant.Controllers
{
  public class CuisinesController : Controller
  {
    private readonly BestRestaurantContext _db;
    public CuisinesController(BestRestaurantContext db)
    {
      _db = db;
    }
    public ActionResult Details( int id )
    {
      Cuisine thisCuisine = _db.Cuisines.Include(Cuisine => Cuisine.Restaurant).FirstOrDefault(cuisines => cuisines.CuisineId == id);
      return View(thisCuisine);
    }
    public ActionResult Create()
    {
    ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Name");
    return View();
    }
    public ActionResult Edit (int id)
    {
      var thisCuisine = _db.Cuisines.FirstOrDefault(Cuisines => Cuisines.CuisineId == id);
      ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Name");
      return View(thisCuisine);
    }
    
    public ActionResult Delete(int id)
    {
      var thisCuisine = _db.Cuisines.FirstOrDefault(cuisines => cuisines.CuisineId == id);
      return View(thisCuisine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCuisine = _db.Cuisines.FirstOrDefault(cuisines => cuisines.CuisineId == id);
      _db.Cuisines.Remove(thisCuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult Edit(Cuisine cuisine)
    {
      _db.Entry(cuisine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult Create(Cuisine cuisine)
    {
      _db.Cuisines.Add(cuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Index()
    {
      List<Cuisine> model = _db.Cuisines.Include(cuisines => cuisines.Restaurant).ToList();
      return View(model);
    }
  }
}