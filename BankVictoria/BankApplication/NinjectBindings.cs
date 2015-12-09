using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankUnitOfWork.Interfaces;
using BankUnitOfWork.UnitsOfWork;

using BankDAL.Interfaces;
using BankDAL.Repositories;

using BankBL.BusinessComponents;
using BankBL.Interfaces;

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

      Bind<IUserUnitOfWork>().To<UserUnitOfWork>();
      Bind<IUserBusinessComponent>().To<UserBusinessComponent>();
      Bind<IClientUnitOfWork>().To<ClientUnitOfWork>();
      Bind<IClientBusinessComponent>().To<ClientBusinessComponent>();
      //ADD ALLL BINDING HERE
    }
  }
}
