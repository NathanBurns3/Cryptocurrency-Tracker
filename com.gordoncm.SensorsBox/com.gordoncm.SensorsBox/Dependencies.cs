using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using com.gordoncm.SensorsBox.Database; 
using Microsoft.Extensions.DependencyInjection; 

namespace com.gordoncm.SensorsBox
{
    public class Dependencies 
    {

        private IServiceCollection services = new ServiceCollection();

        public Dependencies Load()
        {
  

            return this; 
        }



        public IServiceProvider GetServiceProvider()
    => services.BuildServiceProvider();
    }
}
