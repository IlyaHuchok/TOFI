using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml;

using BankPresentation;

using Ninject;

namespace BankApplication
{
  public class ApplicationRunner
  {
    public void Run()
    {
      this.Configure();

      var app = new BankApp();
      using (var kernel = new StandardKernel(new NinjectBindings()))
      {
        var loginWindow = kernel.Get<LoginWindow>();
        app.Run(loginWindow); // KOSTYLLL!1
      }
    }

    private void Configure()
    {
      
    }
  }
}
