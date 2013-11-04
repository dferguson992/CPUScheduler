﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// AUTHOR: COLEEN FITZSIMONS

namespace COM310JobScheduler
{
    /// <summary>
    /// FIRST COME FIRST SERVED CLASS
    /// </summary>
    public class FCFS : Scheduler, Algorithm
    {
        /// <summary>
        /// CALCULATES GANTT CHART FOR THIS ALGORITHM
        /// INHERITED FROM BASE CLASS AND INTERFACE
        /// </summary>
        /// <param name="activeProcesses">LIST OF ALL PROCESSES THAT ARE ACTIVE</param>
        /// <returns>RETURNS THE STRING REPRESENTATION OF A GANTT CHART BUILT BY A STRINGBUILDER</returns>
        
            StringBuilder gantt = new StringBuilder();
            int totalWaitTime = 0;
            sortArrival(ref activeProcesses);
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
                            /// In normal circumstances, ArriveTime will vary, but due to the nature
                            ///of this prgram, we are having all of the processes arrive at the same time.
                            ///The 'first come first serve' order is being determined by the user's input,
                            ///not the actual arrival of the processes.
                            ///This allows us to use the same algorithm to determine the total wait time and 
                            ///turn around times of each process without having to alter the code.  
                            ///If we were to ask for the arrival times, we would first sort the arrival
                            ///then we would find the difference between the arrival and the wait time to 
                            ///determine the actual wait time for each process.  We would then use the actual 
                            ///wait time to help us determine the turn around time for the process as well.
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
