using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM310JobScheduler
{
    /// <summary>
    /// ROUND-ROBIN CLASS
    /// </summary>
    public class RR : Scheduler, Algorithm
    {
        private int quantum = 0;
        public RR(int quantum)
        {
            this.quantum = quantum;
        }

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

        private void buildGantt(Process p, ref StringBuilder g)
        {
            g.Append("--------------------\n");
            g.Append("|                  |\n");
            g.Append("|       P" + p.PID + "        |\n");
            g.Append("|  wait: " + p.WaitTime + "       |\n");
            g.Append("|                  |\n");

        }
    }
}
