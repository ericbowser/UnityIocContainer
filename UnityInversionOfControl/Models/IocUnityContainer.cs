using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityInversionOfControl.Models
{
  /// <summary>
  /// Get the IOC Container
  /// </summary>
  public static class IocUnityContainer
  {
    public static IUnityContainer Container { get; set; }
  }
}