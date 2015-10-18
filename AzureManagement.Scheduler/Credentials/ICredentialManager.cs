using AzureManagement.Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;

namespace AzureManagement.Scheduler.Credentials
{
    public interface ICredentialManager
    {
        void SetActiveSubscriptionForInitialisation(string subscriptionName);
        void Initialise();

        Config.Subscription ActiveSubscription { get; }
        void SetActiveSubscription(string subscriptionName);
        SubscriptionCloudCredentials GetManagementCredentials();
    }
}
