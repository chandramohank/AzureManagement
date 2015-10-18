using AzureManagement.Scheduler.Common;
using AzureManagement.Scheduler.Configuration;
using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler.Credentials
{
    public class CredentialManager : ICredentialManager
    {
        // private readonly ILog _logger;
        private string _activeSubscriptionForInitialisation;

        private Config.Subscription DefaultSubscription { get; set; }
        public Config.Subscription ActiveSubscription { get; private set; }

        //public ConfigurationManager(ILog logger)
        //{
        //    _logger = logger;
        //}

        public void SetActiveSubscriptionForInitialisation(string subscriptionName)
        {
            Guard.AgainstNullAndEmpty("Active Subscription Name", subscriptionName);
            _activeSubscriptionForInitialisation = subscriptionName;
        }

        public void Initialise()
        {
            SetDefaultSubscription();
            SetActiveSubscription(!String.IsNullOrEmpty(_activeSubscriptionForInitialisation)
                                    ? _activeSubscriptionForInitialisation
                                    : DefaultSubscription.Name);
        }

        private void SetDefaultSubscription()
        {
            var config = ConfigurationManager.Config;
            try
            {
                DefaultSubscription = config.Subscriptions.First();
                //_logger.InfoFormat("Subscription '{0}', SubscriptionId '{1}' set as default Subscription.", DefaultSubscription.Name, DefaultSubscription.SubscriptionId);
            }
            catch (InvalidOperationException)
            {
                const string message = "No Subscriptions could be found in config.";
                //_logger.Error(message);
                throw new CredentialException(message);
            }
        }

        public void SetActiveSubscription(string subscriptionName)
        {
            var config = ConfigurationManager.Config;
            try
            {
                ActiveSubscription = config.Subscriptions.First(s => s.Name.Equals(subscriptionName));
                // _logger.InfoFormat("Subscription '{0}', SubscriptionId '{1}' set as active Subscription.", ActiveSubscription.Name, ActiveSubscription.SubscriptionId);
            }
            catch (InvalidOperationException)
            {
                var message = String.Format("The Subscription for '{0}' could not be found in config.", subscriptionName);
                //_logger.ErrorFormat(message);
                throw new CredentialException(message);
            }
        }

        public SubscriptionCloudCredentials GetManagementCredentials()
        {
            var subscription = ActiveSubscription;           

            var credentials = CertificateCloudCredentialsFactory.FromPublishSettingsFile(Config.PublishSettingsFilePath, subscription.Name);
            if (credentials == null)
            {
                var message = String.Format("The Subscription for '{0}' did not have valid subscription details configured.", subscription.Name);
                // _logger.ErrorFormat(message);
                throw new CredentialException(message);
            }

            return credentials;
        }
    }
}
