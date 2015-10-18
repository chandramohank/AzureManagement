using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AzureManagement.Scheduler.Credentials
{
    public static class CertificateCloudCredentialsFactory
    {
        public static CertificateCloudCredentials FromPublishSettingsFile(string path, string subscriptionName)
        {
            var profile = XDocument.Load(path);
            var subscriptionId = profile.Descendants("Subscription")
                .First(element => element.Attribute("Name").Value == subscriptionName)
                .Attribute("Id").Value;
            var certificate = new X509Certificate2(
                Convert.FromBase64String(profile.Descendants("PublishProfile").Descendants("Subscription").Single().Attribute("ManagementCertificate").Value));
            return new CertificateCloudCredentials(subscriptionId, certificate);
        }
    }
}
