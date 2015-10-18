using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler.Configuration
{
    public interface IConfigurationManager
    {
        void AddProvider(IConfigurationProvider configurationProvider);
        void Initialise();
    }
}
