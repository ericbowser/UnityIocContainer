using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using UnityInversionOfControl.Models;

namespace UnityInversionOfControl
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);

      //Initialize IOC Container and add registrations
      Bootstrapper.Initialise();
      //Register custom controller factory
      ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));
    }
  }
}