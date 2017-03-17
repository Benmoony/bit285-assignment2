using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2.Models;
using Assignment2.ViewModels;
using System.Text;

namespace Assignment2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private VisitorLogContext db = new VisitorLogContext();

        //Get's List of Users
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Activities.ToList());
        }

        //Get's the Create User Page
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Programs = db.Programs.ToList();

            return View();
        } 


        //Get's the login page
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public ActionResult PasswordGenerator()
        {
            List<string> passdefault = new List<string>();
            passdefault.Add("Suggest A Password");

            ViewBag.PasswordOptions = passdefault.ToList();
            return View();
        }

        //Stores User info into Session Data and opens the password generator page
        [HttpPost]
        public ActionResult Create(CreateAccount user)
        {
            var Current = GetTempUser();

            Current.FirstName = user.FirstName;
            Current.LastName = user.LastName;
            Current.Email = user.EmailAddress;
            Current.ProgramID = user.ProgramID;
            Current.EmailUpdates = user.emailCheck;

            return RedirectToAction("PasswordGenerator");
        }

        [HttpPost]
        public ActionResult PasswordGenerator(PasswordGen user)
        {
            if (user.SelectedPassword != "Suggest A Password")
            {
                var Current = GetTempUser();

                Current.Password = user.SelectedPassword;
                db.Users.Add(Current);
                db.SaveChanges();
                return View("Login");
            }

            //Insert Code here that generates a list and displays it in the PasswordGenerator View
            List<string> passwordList = new List<string>();
            passwordList = sugPass(user);

            

            ViewBag.PasswordOptions = passwordList;
            return View();
        }


        //Navigates the User to Home after they login
        [HttpPost]
        public ActionResult Login(Login login)
        {
            Activity logged = new Activity();

            //lamda to check for validity

           bool Query = db.Users.ToList().Any(m => m.Email == login.UserName && m.Password == login.Password);

            if (Query)
            {
                
                logged.ActivityName = login.UserName;
                logged.ActivityDate = DateTime.Now;
                logged.IpAddress = Request.UserHostAddress;

                db.Activities.Add(logged);
                db.SaveChanges();

                //RedirectToAction passing the action to occur
                return View("../Home/Index", db.Activities.ToList()); //Navigates to the Home Controller
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("Error", "!*PLEASE ENTER VALID CREDENTIALS*!");
                return View("Login");
            }
            
        }

        private List<string> sugPass(PasswordGen pass)
        {
            string Name = pass.LastName;
            string Year = pass.BirthYear;
            string Color = pass.FavColor;

            //builds new instance of the string builder called passbuilder and sets the maximum capacity of the string to 8
            StringBuilder passBuilder = new StringBuilder();
            passBuilder.Capacity = 8;

            //builds password with lastname and color
            passBuilder.Append(Name);
            passBuilder.Append(Color);
            string pass1 = passBuilder.ToString();

            //builds password with color and year
            passBuilder.Remove(0, 8);
            passBuilder.Append(Color);
            passBuilder.Append(Year);
            string pass2 = passBuilder.ToString();

            //password with lastname and color inserted at the fourth letter
            passBuilder.Remove(0, 8);
            passBuilder.Append(Name);
            passBuilder.Insert(4, Color);
            string pass3 = passBuilder.ToString();

            //builds password with year and name
            passBuilder.Remove(0, 8);
            passBuilder.Append(Year);
            passBuilder.Append(Name);
            string pass4 = passBuilder.ToString();

            //builds password with the year, the name inserted at the third character, and the color at the sixth character
            passBuilder.Remove(0, 8);
            passBuilder.Append(Year);
            passBuilder.Insert(2, Name);
            passBuilder.Insert(6, Color);
            string pass5 = passBuilder.ToString();

            //creates a list of the passwords generated, sorts the passwords and then sets that variable ist as the datasource of the passDD and then binds that data to the passDD
            var passwords = new List<string> { pass1, pass2, pass3, pass4, pass5 };

            return (passwords);
        }


 /**
  * Store temporary user in Session during account creation
  */
        private User GetTempUser()
        {
            if (Session["tempUser"] == null)
            {
                Session["tempUser"] = new User();
            }
            return (User)Session["tempUser"];
        }

        private void FlushTempUser()
        {
            Session.Remove("tempUser");
        }
    }
}