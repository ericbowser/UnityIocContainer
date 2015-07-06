using UnityInversionOfControl.Models;
using UnityInversionOfControl.Abstract;
using UnityInversionOfControl.ExtentionMethods;
using Xunit;
using UnityInversionOfControl.Concrete;
using System;
using UnityInversionOfControl.Controllers;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace UnityInversionOfControl.IocControllerTests
{
  /// <summary>
  /// Test IOC Container and functionality
  /// </summary>
  public class IocControllerTests : DefaultControllerFactory
  {
    IInsuranceRepository fakeInsRepo;
    IUnknownRepository fakeUnknownRepo;

    /// <summary>
    /// Initialize the container once for tests
    /// </summary>
    public IocControllerTests()
    {
      Bootstrapper.Initialise();
    }

    /// <summary>
    /// Test the registrations using an extention
    /// </summary>
    [Fact]
    public void IocTestRegistrations()
    {
      bool actual = false;

      actual = IocUnityContainer.Container.IsRegisteredExt<IInsuranceRepository>();
      Assert.Equal(true, actual);

      actual = IocUnityContainer.Container.IsRegisteredExt<IEmailRepository>();
      Assert.Equal(true, actual);

      actual = IocUnityContainer.Container.IsRegisteredExt<IUserRepository>();
      Assert.Equal(true, actual);

      actual = IocUnityContainer.Container.IsRegisteredExt<IDeductibleRepository>();
      Assert.Equal(true, actual);

      actual = IocUnityContainer.Container.IsRegisteredExt<IUnknownRepository>();
      Assert.Equal(false, actual);
    }

    /// <summary>
    /// Test to inject the User controller
    /// </summary>
    [Fact]
    public void IocTestInjectUserController()
    {
      var controller = IocUnityContainer.Container.Resolve<UserController>();
      var result = controller.Index() as ViewResult;
      Assert.NotNull(result);
      Assert.Contains("AUserName", result.ViewBag.userName);
    }

    /// <summary>
    /// Test to inject the Insurance controller
    /// </summary>
    [Fact]
    public void IocTestInjectInsuranceController()
    {
      var controller = IocUnityContainer.Container.Resolve<InsuranceController>();
      var result = controller.Index() as ViewResult;
      Assert.NotNull(result);
      Assert.Contains("Insurance", result.ViewBag.insurance);
    }

    /// <summary>
    /// Test to inject the Property controller
    /// </summary>
    [Fact]
    public void IocTestInjectDeductibleController()
    {
      var controller = IocUnityContainer.Container.Resolve<DeductibleController>();
      var result = controller.Index() as ViewResult;
      Assert.NotNull(result);
      Assert.Contains("SomeDeductible", result.ViewBag.deductible);
    }

    /// <summary>
    /// Default un-named mapping or transient lifestyle
    /// </summary>
    [Fact]
    public void IocTestDefaultRegistration()
    {
      IocUnityContainer.Container.RegisterExt<IInsuranceRepository, InsuranceRepository>(LifeStyles.Transient);
      fakeInsRepo = IocUnityContainer.Container.ResolveExt<IInsuranceRepository>();
      fakeInsRepo.Insurance = "TestTransient";
      fakeInsRepo = IocUnityContainer.Container.ResolveExt<IInsuranceRepository>();
      Assert.Equal(null, fakeInsRepo.Insurance);
    }

    /// <summary>
    /// Default un-named mapping or singleton lifestyle
    /// </summary>
    [Fact]
    public void IocTestSingletonRegistration()
    {
      IocUnityContainer.Container.RegisterExt<IInsuranceRepository, InsuranceRepository>(LifeStyles.Singleton);
      fakeInsRepo = IocUnityContainer.Container.ResolveExt<IInsuranceRepository>();
      fakeInsRepo.Insurance = "TestSingleton";
      fakeInsRepo = IocUnityContainer.Container.ResolveExt<IInsuranceRepository>();
      Assert.Equal("TestSingleton", fakeInsRepo.Insurance);
    }

    /// <summary>
    /// Test to try an resolve an unknown or un-registered type
    /// </summary>
    [Fact]
    public void UnknownInterfaceType()
    {
      try
      {
        fakeUnknownRepo = IocUnityContainer.Container.ResolveExt<IUnknownRepository>();
      }
      catch (InvalidOperationException e)
      {
        Assert.Contains("Are you missing a type mapping?", e.Message.ToString());
      }
    }

    /// <summary>
    /// Try to test thread static lifestyle
    /// </summary>
    [Fact]
    public void StaticThread()
    {
      IocUnityContainer.Container.RegisterExt<IInsuranceRepository, InsuranceRepository>(LifeStyles.PerThread);
      fakeInsRepo = IocUnityContainer.Container.ResolveExt<IInsuranceRepository>();
      fakeInsRepo.Insurance = "TestThreadStatic";
      fakeInsRepo = IocUnityContainer.Container.ResolveExt<IInsuranceRepository>();
      Assert.Equal("TestThreadStatic", fakeInsRepo.Insurance);
    }
  }
}