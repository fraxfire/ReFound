using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReFound.Models;
using Object = ReFound.Models.Object;
using RefoundObject = ReFound.Models.Object;
using Microsoft.EntityFrameworkCore;

namespace ReFound.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ReFoundDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ReFoundDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        private string isAuthenticated = "OK";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cart()
        {
            //string? isAuthenticated = HttpContext.Session.GetString("isAuthenticated");
            if (isAuthenticated == "OK")
                return View();
            return Redirect("\\Identity\\Account\\Login");
        }

          public IActionResult AddToCart()
          {
              return View();
          }

        //   [HttpPost]
        //   public IActionResult AddToCart(int ObjectId)
        //   {
        //       var userId = _userManager.GetUserId(User);

        //     //   var cartItem = _context.Cart.FirstOrDefault(cart.ObjectId == ObjectId );

        //       if (cartItem != null)
        //       {
        //           cartItem.Quantity++;
        //       }
        //       else
        //       {
        //           cartItem = new Cart
        //          {
        //               ObjectId = ObjectId,
        //               Quantity = 1,
        //           };
        //           _context.Cart.Add(cartItem);
        //       }

        //       _context.SaveChanges();

        //       return RedirectToAction("Cart", "Home");
        //   }

        public IActionResult Objects()
        {
            var objects = _context.Objects.ToList();
            return View(objects);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Sell()
        {
            return View();
        }

        [HttpPost]
public async Task<IActionResult> Sell(string Name, double Price, string Description, List<IFormFile> Images)
{
    var userId = _userManager.GetUserId(User);

    if (ModelState.IsValid)
    {
        var obj = new Object
        {
            Name = Name,
            Price = Price,
            Description = Description,
            UserId = userId // Imposta il UserId dell'oggetto
        };

        _context.Objects.Add(obj);
        await _context.SaveChangesAsync();

        // Processa le immagini se necessario
        if (Images != null && Images.Count > 0)
        {
            foreach (var image in Images)
            {
                if (image.Length > 0)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }
        }

        // Salva le modifiche al database
        await _context.SaveChangesAsync();

        // Reindirizza l'utente alla pagina "Objects"
        return RedirectToAction("Objects", "Home");
    }

    return View();
}


        [HttpPost]
public IActionResult RemoveObject(int ObjectId)
{
    var userId = _userManager.GetUserId(User);

    var obj = _context.Objects.FirstOrDefault(o => o.ObjectId == ObjectId && o.UserId == userId);

    if (obj != null)
    {
        _context.Objects.Remove(obj);
        _context.SaveChanges();
    }

    return RedirectToAction("Objects", "Home");
}

    public class CartController : Controller
{
    private readonly ReFoundDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ReFoundDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Cart()
    {
        var userId = _userManager.GetUserId(User);

        var cartItems = await _context.Cart
            .Where(c => c.UserId == userId)
            .Select(c => new CartViewModel
            {
                ObjectId = c.ObjectId,
                Name = c.Name ?? "Unknown", // Handling potential null reference
                Price = c.Price,
                Quantity = c.Quantity
            })
            .ToListAsync();

        return View(Cart);
    }
}
[HttpPost]


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
