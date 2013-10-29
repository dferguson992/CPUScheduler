using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM310JobScheduler
{
    /// <summary>
    /// FIRST COME FIRST SERVED CLASS
    /// </summary>
    public class FCFS : Scheduler, Algorithm
    {
        public override string calculateGantt(List<Process> activeProcesses)
        {
            return "WORKS!";
        }
    }
}
