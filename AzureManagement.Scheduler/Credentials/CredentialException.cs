using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler.Credentials
{
    public class CredentialException : Exception
    {
        public CredentialException() { }
        public CredentialException(string message) : base(message) { }
        public CredentialException(string message, Exception innerException) : base(message, innerException) { }
    }
}
