using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityInversionOfControl.Abstract;

namespace UnityInversionOfControl.Concrete
{
  public class UserRepository : IUserRepository
  {
    readonly IEmailRepository emailRepo;

    public string UserName { get; set; }

    /// <summary>
    /// Inject the contructor with email for the user
    /// </summary>
    /// <param name="emailRepo"></param>
    public UserRepository(IEmailRepository emailRepo)
    {
      this.emailRepo = emailRepo;
    }
  }
}