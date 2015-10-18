using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler.Configuration
{
    public class Config
    {
        public bool HttpTraceEnabled { get; set; }
        public IList<Subscription> Subscriptions { get; set; }
        public static string PublishSettingsFilePath { get; set; }
        public static string cloudServiceName { get; set; }
        public class Subscription
        {
            public string Name { get; set; }
            public string SubscriptionId { get; set; }
            public ManagementCertificate ManagementCertificate { get; set; }
        }

        public class ManagementCertificate
        {
            public string Thumbprint { get; set; }
            public string Base64Data { get; set; }
        }

        public class CloudServiceParameters
        {
            public string Description { get; set; }
            public string GeoRegion { get; set; }
            public string Label { get; set; }
        }
        
    }                                          
}
