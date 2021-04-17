using System.Linq;
using System.Web.Mvc;
using ECommerce.Models;
using System.Web.Security;



namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var modelContext = new modelContext())
            {
                bool isValid = modelContext.Users.Any(x => x.Email == user.Email && x.Password == user.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserNamee, false);
                    User oldUser = (from obj in modelContext.Users
                                    where obj.Email == user.Email && obj.Password == user.Password
                                    select obj).FirstOrDefault();
                    Session["id"] = oldUser.Id;
                    return RedirectToAction("Index", "Users");
                }
                ModelState.AddModelError("", "Invalid email and password");
                return View();
            }

        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            using (var modelContext = new modelContext())
            {
                var count = (from u in modelContext.Users select u).Count() + 1;
                user.Id = count.ToString();
                modelContext.Users.Add(user);
                modelContext.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpPost]
        public ActionResult manageAccount(User newUser)
        {
            using (var modelContext = new modelContext())
            {
                User oldUser = new User();
                oldUser = (from obj in modelContext.Users
                           where obj.Id == newUser.Id
                           select obj).FirstOrDefault();


                newUser.Id = oldUser.Id;
                newUser.UserNamee = oldUser.UserNamee;
                newUser.Address = oldUser.Address;
                newUser.Email = oldUser.Email;
                newUser.Password = oldUser.Password;

                modelContext.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        [HttpGet]
        public ActionResult manageAccount(string id)
        {

            using (var modelContext = new modelContext())
            {
                User oldUser = new User();
                oldUser = (from obj in modelContext.Users
                           where obj.Id == id
                           select obj).FirstOrDefault();
                modelContext.SaveChanges();
                return View(oldUser);
            }

        }

        [HttpPost]
        public ActionResult sendFeedback(FeedBack feedBack)
        {
            var modelContext = new modelContext();
            if (feedBack != null)
            {
                modelContext.feedBacks.Add(feedBack);
                modelContext.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();

        }
        [HttpGet]
        public ActionResult sendFeedback()
        {

            return View();

        }
    }
}