using System;
using System.Collections.Generic;
using System.Text;

/// AUTHOR: DAN FERGUSON

namespace COM310JobScheduler
{
    /// <summary>
    /// ROUND-ROBIN CLASS
    /// </summary>
    public class RR : Scheduler, Algorithm
    {
        private int quantum = 0;
        /// <summary>
        /// CONSTRUCTOR FOR ROUND-ROBIN CLASS
        /// </summary>
        /// <param name="quantum">QUANTUM DURATION</param>
        public RR(int quantum)
        {
            this.quantum = quantum;
        }

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
            //sortArrival(ref activeProcesses);
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
                            p.BurstTime = p.BurstTime - quantum;
                            p.WaitTime = totalWaitTime;
                            if (p.BurstTime < 0)
                            {
                                totalWaitTime = totalWaitTime + quantum - (Math.Abs(p.BurstTime));
                            }
                            else totalWaitTime = totalWaitTime + quantum;
                            buildGantt(p, ref gantt);
                            if (p.BurstTime <= 0)
                            {
                                p.BurstTime = 999;
                                p.TurnAroundTime = p.WaitTime + p.InitialBurst;
                            }
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
