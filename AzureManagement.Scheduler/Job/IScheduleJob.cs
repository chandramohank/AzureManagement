using AzureManagement.Scheduler.Common;
using Microsoft.WindowsAzure.Management.Scheduler;
using Microsoft.WindowsAzure.Management.Scheduler.Models;
using Microsoft.WindowsAzure.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureManagement.Scheduler
{
    public interface IScheduleJob
    {
        JobCreateOrUpdateResponse CreateOrUpdate(string jobCollectionName, string jobID, DateTime startTime, string URL);
        SchedulerOperationStatusResponse CreateJobCollection(SchedulerManagementClient schedulerServiceClient, string jobCollectionName);
        JobCreateOrUpdateResponse CreateOrUpdateJOB(string jobCollectionName, string jobID, DateTime startTime, string URL, JobActionType jobActionType = JobActionType.Http, String method = HttpActionMethod.Get, JobRecurrenceFrequency jobRecurrenceFrequency = JobRecurrenceFrequency.Hour, int interval = 1, int executionCount = 1);
    }
}
