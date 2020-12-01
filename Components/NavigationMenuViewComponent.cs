using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using animated_spoon.Models;
using animated_spoon.Data;

namespace animated_spoon.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly ProductDbContext db;

        public NavigationMenuViewComponent(ProductDbContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(db.ProductsCategories.Select(category => category.Name).Distinct().OrderBy(product => product));
        }
    }
}
