using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityInversionOfControl.Abstract;

namespace UnityInversionOfControl.Concrete
{
  public class EmailRepository : IEmailRepository
  {
    public string Email { get; set; }
  }
}