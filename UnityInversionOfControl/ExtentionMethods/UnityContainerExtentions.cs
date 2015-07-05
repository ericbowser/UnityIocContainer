using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityInversionOfControl.Models;

namespace UnityInversionOfControl.ExtentionMethods
{
  public static class UnityContainerExtensions
  {
    /// <summary>
    /// Find out if a type is registered or not
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <returns></returns>
    public static bool IsRegistered<T>(this IUnityContainer container)
    {
      try
      {
        container.Resolve<T>();
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Register using generic type from and to
    /// </summary>
    public static IUnityContainer RegisterExt<F, T>(this IUnityContainer container, LifeStyles lifeStyle = LifeStyles.Transient)
    {
      try
      {
        switch (lifeStyle.ToString())
        {
          case "Transient":
            return container.RegisterType<T, T>();
          case "Singleton": //return a reference to same instance
            return container.RegisterType<T, T>(new ContainerControlledLifetimeManager());
          case "PerThread": //Life time manager accross threads
            return container.RegisterType<T, T>(new PerThreadLifetimeManager());
          default: //default just use transient lifestyle
            return container.RegisterType<T, T>();
        }
      }

      catch (InvalidOperationException i)
      {
        throw new InvalidOperationException("Could not register type =>", i);
      }
    }

    /// <summary>
    /// Resolve the type by using a container extention
    /// </summary>
    public static T ResolveExt<T>(this IUnityContainer container)
    {
      try
      {
        return container.Resolve<T>();
      }

      catch (Exception e)
      {
        throw new InvalidOperationException(string.Format("Could not resolve type => {0}", e.InnerException.Message.ToString()));
      }
    }
  }
}