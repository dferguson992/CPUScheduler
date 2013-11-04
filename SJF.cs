using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// AUTHOR: COLLEEN FITZSIMONS

namespace COM310JobScheduler
{
    /// <summary>
    /// SHORTEST JOB FIRST CLASS
    /// </summary>
    public class SJF : Scheduler, Algorithm
    {
        /// <summary>
        /// CALCULATES GANTT CHART FOR THIS ALGORITHM
        /// INHERITED FROM BASE CLASS AND INTERFACE
        /// </summary>
        /// <param name="activeProcesses">LIST OF ALL PROCESSES THAT ARE ACTIVE</param>
        /// <returns>RETURNS THE STRING REPRESENTATION OF A GANTT CHART BUILT BY A STRINGBUILDER</returns>
        public override string calculateGantt(List<Process> activeProcesses)
        {
            StringBuilder gantt = new StringBuilder();
            int totalWaitTime = 0;
            
            ///By using the sorting algorithm, we will be able to determine the order of processes
            ///based on the burst time of each of them.  Because the only way of doing this is by 
            ///either having future knowledge(which is impossible), or by having all of the processes
            ///arrive at the same time, we are using the latter method.  This will allow us to be able
            ///to determine the order of the processes to have the most efficient wait times and turnaround times.
            
            sortSize(ref activeProcesses);
            bool complete = false;
            int i = 0;
            while(!complete)
            {
                if (activeProcesses[i++].BurstTime == 999)
                {
                    if (i == activeProcesses.Count)
                    {
                        complete = true;
                    }
                    continue;
                }
                else
                {
                    i = 0;
                    foreach (Process p in activeProcesses)
                    {
                        if (p.BurstTime != 999)
                        {
                            p.WaitTime = totalWaitTime;
                            totalWaitTime += p.BurstTime;
                            p.TurnAroundTime = p.WaitTime + p.InitialBurst;
                            buildGantt(p, ref gantt);
                            p.BurstTime = 999;
                        }
                        else continue;
                    }
                }
            }
            gantt.Append("--------------------\n\n");
            return gantt.ToString();
        }
        public override string calculateGantt(List<Process> activeProcesses)
        {
            base.buildGantt(p, ref g);
        }
    }
}
