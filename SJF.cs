using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM310JobScheduler
{
    /// <summary>
    /// SHORTEST JOB FIRST CLASS
    /// </summary>
    public class SJF : Scheduler, Algorithm
    {
        public override string calculateGantt(List<Process> activeProcesses)
        {
            return "WORKS!";
        }
    }
}
