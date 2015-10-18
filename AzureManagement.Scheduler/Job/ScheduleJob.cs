using AzureManagement.Scheduler.Common;
using AzureManagement.Scheduler.Credentials;
using Microsoft.WindowsAzure.Management.Scheduler;
using Microsoft.WindowsAzure.Management.Scheduler.Models;
using Microsoft.WindowsAzure.Scheduler;
using Microsoft.WindowsAzure.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler
{
    public class ScheduleJob : IScheduleJob
    {
        // private readonly ILog _logger;
        private readonly ICredentialManager _credentialManager;
        private readonly ISchedulerManagement _scheduleManagement;

        public String cloudSerivceName { get; set; }
        public ScheduleJob()
        {
            _credentialManager = new CredentialManager();
            _scheduleManagement = new SchedulerManagement();
        }

        public JobCreateOrUpdateResponse CreateOrUpdate(string jobCollectionName, string jobID, DateTime startTime, string URL)
        {
            _credentialManager.Initialise();
           var schedulerServiceClient= _scheduleManagement.CreateClient();
            CreateJobCollection(schedulerServiceClient, jobCollectionName);
           return CreateOrUpdateJOB(jobCollectionName, jobID, startTime, URL);
        }
        public SchedulerOperationStatusResponse CreateJobCollection(SchedulerManagementClient schedulerServiceClient, string jobCollectionName)
        {
            var result = schedulerServiceClient.JobCollections.Create(cloudSerivceName, jobCollectionName, new JobCollectionCreateParameters()
            {
                Label = jobCollectionName,
                IntrinsicSettings = new JobCollectionIntrinsicSettings()
                {
                    Plan = JobCollectionPlan.Standard,
                    Quota = new JobCollectionQuota()
                    {
                        MaxJobCount = 100,
                        MaxJobOccurrence = 100,
                        MaxRecurrence = new JobCollectionMaxRecurrence()
                        {
                            Frequency = JobCollectionRecurrenceFrequency.Minute,
                            Interval = 1
                        }
                    }
                }
            });
            return result;
        }

        public JobCreateOrUpdateResponse CreateOrUpdateJOB(string jobCollectionName, string jobID,DateTime startTime, string URL, JobActionType jobActionType = JobActionType.Http, String method = HttpActionMethod.Get, JobRecurrenceFrequency jobRecurrenceFrequency = JobRecurrenceFrequency.Hour, int interval = 1, int executionCount = 1)
        {
            var credentials = _credentialManager.GetManagementCredentials();
            var schedulerClient = new SchedulerClient(cloudSerivceName, jobCollectionName, credentials);
            
            var result = schedulerClient.Jobs.CreateOrUpdate(jobID,new JobCreateOrUpdateParameters()
            {
                Action = new JobAction()
                {
                    Type = jobActionType,
                    Request = new JobHttpRequest()
                    {
                        Method = HttpActionMethod.Post,
                        Uri = new Uri(URL)
                    }
                },
                StartTime = startTime,
                Recurrence = new JobRecurrence()
                {
                    Frequency = jobRecurrenceFrequency,
                    Interval = interval,
                    Count = executionCount
                }
            });
            return result;
        }
    }
}
