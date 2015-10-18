using AzureManagement.Scheduler.Configuration;
using AzureManagement.Scheduler.Credentials;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Management.Scheduler;
using Microsoft.WindowsAzure.Management.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler
{
    public class SchedulerManagement:ISchedulerManagement
    {
        //
        //TODO:Add Logger, Dependency Injection
        //

        //private readonly ILog _logger;
        private readonly ICredentialManager _credentialManager;
        public Config.CloudServiceParameters cloudServiceParameters { get; set; }
        public SchedulerManagement()
        {
            //_logger = managementContext.Logger;
            _credentialManager = new CredentialManager();
        }
        
        public SchedulerManagementClient CreateClient()
        {
            _credentialManager.Initialise();
            var credentials = _credentialManager.GetManagementCredentials();
            CreateCloudService(credentials,Config.cloudServiceName);
            var schedulerServiceClient = new SchedulerManagementClient(credentials);
            schedulerServiceClient.RegisterResourceProvider();
            return schedulerServiceClient;
        }

        public void CreateCloudService(SubscriptionCloudCredentials credentials,string cloudServiceName)
        {
            var cloudServiceClient = new CloudServiceManagementClient(credentials);
            var cloudServiceCreateParameters = new CloudServiceCreateParameters
            {
                Description = cloudServiceParameters.Description,
                GeoRegion = cloudServiceParameters.GeoRegion,
                Label = cloudServiceParameters.Label
            };
            cloudServiceClient.CloudServices.Create(cloudServiceName, cloudServiceCreateParameters);
        }
    }
}
