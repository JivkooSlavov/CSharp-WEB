using _01.MVC_Intro.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _01.MVC_Intro.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> _products
        = new List<ProductViewModel>()
        {
                new ProductViewModel
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00
                },
                new ProductViewModel
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 5.50
                },
                new ProductViewModel
                {
                    Id = 3,
                    Name = "Bread",
                    Price = 1.50
                }
        };

        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var foundProducts = this._products
                    .Where(p => p.Name.ToLower()
                        .Contains(keyword.ToLower()));
                return View(foundProducts);
            }
            return View(this._products);
        }
        public IActionResult ById(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }

        public IActionResult AllAsJson()
        {
            return Json(this._products, new JsonSerializerOptions { WriteIndented = true });
        }

        public IActionResult AllAsText()
        {
            var text = string.Empty;
            foreach (var item in _products)
            {
                text += $"Product {item.Id} {item.Name} - {item.Price} lv.";
                text += "\r\n";

            }
             return Content(text);
        }

        public IActionResult AllAsTextFile()
        {
            StringBuilder sb = new();
            foreach (var p in this._products)
            {
                sb.AppendLine($"Product {p.Id}: {p.Name} - {p.Price:f2}lv.");
            }
            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment; filename=products.txt");
            byte[] fileContents = Encoding.UTF8.GetBytes(sb.ToString().TrimEnd());
            string contentType = "text/plain";
            return File(fileContents, contentType);
        }
    }
}
