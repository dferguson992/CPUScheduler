using System.Collections.Generic;
using System.Text;

/// AUTHOR: DAN FERGUSON

namespace COM310JobScheduler
{
    /// <summary>
    /// PRIORITY CLASS
    /// </summary>
    
    public class Priority : Scheduler, Algorithm
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
            sortPriority(ref activeProcesses);
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

        /// <summary>
        /// CALLS BASE METHOD TO ADD GANTT CHART INFO TO A STRINGBUILDER
        /// </summary>
        /// <param name="p">THE REFERENCED PROCESS</param>
        /// <param name="g">THE STRING BUILDER OBJECT BEING ADDED TO</param>
        private new void buildGantt(Process p, ref StringBuilder g)
        {
            base.buildGantt(p, ref g);
        }
    }
}
