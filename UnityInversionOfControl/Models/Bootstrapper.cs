using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityInversionOfControl.Abstract;
using UnityInversionOfControl.Concrete;
using UnityInversionOfControl.Controllers;

namespace UnityInversionOfControl.Models
{
  public class Bootstrapper : DefaultControllerFactory
  {
    /// <summary>
    /// Initialize the container and resolve using a registration point
    /// </summary>
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainerWithRegistration();
      DependencyResolver.SetResolver(new UnityDependencyResolver(container));
      return container;
    }

    /// <summary>
    /// Build the container without registering types
    /// </summary>
    public static IUnityContainer BuildUnityContainerWithoutRegistration()
    {
      var container = new UnityContainer();
      IocUnityContainer.Container = container;
      return container;
    }

    /// <summary>
    /// Build the container with default manager and register types
    /// </summary>
    private static IUnityContainer BuildUnityContainerWithRegistration()
    {
      var container = new UnityContainer();
      container.RegisterType<IUserRepository, UserRepository>();
      container.RegisterType<IEmailRepository, EmailRepository>();
      container.RegisterType<IDeductibleRepository, DeductibleRepository>();
      container.RegisterType<IInsuranceRepository, InsuranceRepository>();
      IocUnityContainer.Container = container;
      return container;
    }
  }
}