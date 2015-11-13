﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApplication
{
  class Program
  {
    static void Main(string[] args)
    {
      var thread = new Thread(new ThreadStart(Run));
      
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      thread.Join();
    }

    private static void Run()
    {
      new ApplicationRunner().Run();
    }
  }
}