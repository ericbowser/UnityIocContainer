using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityInversionOfControl.Abstract;

namespace UnityInversionOfControl.Concrete
{
  public class InsuranceRepository : IInsuranceRepository
  {
    readonly IEmailRepository emailRepo;
    readonly IUserRepository userRepo;
    readonly IDeductibleRepository deductibleRepo;

    public string Insurance { get; set; }

    /// <summary>
    /// Inject the contructor with insurance dependencies 
    /// </summary>
    public InsuranceRepository(IEmailRepository emailRepo, IUserRepository userRepo, IDeductibleRepository deductibleRepo)
    {
      this.userRepo = userRepo;
      this.emailRepo = emailRepo;
      this.deductibleRepo = deductibleRepo;
    }
  }
}