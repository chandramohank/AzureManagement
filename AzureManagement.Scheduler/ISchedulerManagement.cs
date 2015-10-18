using Microsoft.Azure;
using Microsoft.WindowsAzure.Management.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler
{
    public interface ISchedulerManagement
    {
        SchedulerManagementClient CreateClient();
        void CreateCloudService(SubscriptionCloudCredentials credentials, string cloudServiceName);
    }
}
