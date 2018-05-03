using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachine.Core.Model
{
    public enum JobType
    {
        UpdateMachineProductTable = 1
    }

    public class Job
    {
        [Key]
        public int Id { get; set; }

        public bool Done { get; set; }

        public string Data { get; set; }

        public JobType JobType { get; set; }

        public DateTime ScheduledDateTime { get; set; }

        public DateTime? ExecutionDateTime { get; set; }
    }
}
