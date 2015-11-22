using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankDAL.Interfaces;
using BankDAL.Repositories;

using BankPresentation;

using Ninject.Modules;
using Ninject;

namespace BankApplication
{
  public class NinjectBindings : NinjectModule
  {
    public override void Load()
    {
      Bind<IAccountRepository>().To<AccountRepository>();
      Bind<IClientRepository>().To<ClientRepository>();
      Bind<ICreditRepository>().To<CreditRepository>();
      Bind<ICreditTypeRepository>().To<CreditTypeRepository>();
      Bind<IPaymentRepository>().To<PaymentRepository>();
      Bind<IRequestRepository>().To<RequestRepository>();
      Bind<IRequestStatusRepository>().To<RequestStatusRepository>();
      Bind<IUserRepository>().To<UserRepository>();
      Bind<IAccountRepository>().To<AccountRepository>();

      //ADD ALLL BINDING HERE
    }
  }
}
