using Microsoft.Practices.Unity;
using System.Web.Mvc;
using UnityInversionOfControl.Abstract;

namespace UnityInversionOfControl.Controllers
{
  public class UserController : Controller
  {
    public IUserRepository userRepo;
    public IEmailRepository emailRepo;

    /// <summary>
    /// Property controller contructor to use contructor injection based on interface type
    /// </summary>
    public UserController(IUserRepository userRepo, IEmailRepository emailRepo)
    {
      this.userRepo = userRepo;
      this.emailRepo = emailRepo;
    }

    public ViewResult Index()
    {
      //dynamic expression -> Will be resolved at runtime
      userRepo.UserName = "AUserName";
      emailRepo.Email = "AnEmailAddress";
      ViewBag.userName = userRepo.UserName;
      ViewBag.email = emailRepo.Email;
      return View();
    }
  }
}
