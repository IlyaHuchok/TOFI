using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // NOT A PART OF PROJECT
            // ADD ANY CODE FOR TESTING HERE

            var creditRepository = DEPENCDECY_INJECTION_FRAMEWORK.CREATE<ICreditRepository>();
            Console.ReadLine();
        }

        public void Configure()
        {
            DEPENCDECY_INJECTION_FRAMEWORK.BINDING_MODULE.Bind<>(CreditRepository).To<ICreditRepository>();
        }
    }
}
