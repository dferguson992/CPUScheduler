using System.Collections.Generic;
using System.Linq;
using System.Text;

/// AUTHOR: DAN FERGUSON

namespace COM310JobScheduler
{
    /// <summary>
    /// BASE CLASS FOR ALL CPU SCHEDULE ALGORITHMS
    /// </summary>
    public class Scheduler : Algorithm
    {
        /// <summary>
        /// WHERE THE ACTUAL ALGORITHM GOES. MUST BE OVERWRITTEN
        /// </summary>
        /// <param name="activeProcesses">LIST OF ALL PROCESSES THAT ARE ACTIVE</param>
        /// <returns>RETURNS THE STRING REPRESENTATION OF A GANTT CHART BUILT BY A STRINGBUILDER</returns>
        public virtual string calculateGantt(List<Process> activeProcesses)
        {
            return "";
        }

        /// <summary>
        /// CALLS BASE METHOD TO ADD GANTT CHART INFO TO A STRINGBUILDER
        /// </summary>
        /// <param name="p">THE REFERENCED PROCESS</param>
        /// <param name="g">THE STRING BUILDER OBJECT BEING ADDED TO</param>
        protected void buildGantt(Process p, ref StringBuilder g)
        {
            g.Append("--------------------\n");
            g.Append("|                  |\n");
            g.Append("|       P" + p.PID + "        |\n");
            g.Append("|  wait: " + p.WaitTime + "    |\n");
            g.Append("|                  |\n");

        }

        /// <summary>
        /// SORTS PROCESSES BASED ON ARRIVAL
        /// </summary>
        /// <param name="activeProcesses">LIST OF ACTIVE PROCESSES</param>
        protected void sortArrival(ref List<Process> activeProcesses)
        {
            List<Process> temp = new List<Process>();
            Process[] processArr = activeProcesses.ToArray();
            Process dummy = new Process(999, 999, 999, 999);
            int ptr = 0;
            for (int i = 0; i < processArr.Length; i++)
            {
                Process earliest = processArr[i];
                ptr = i;
                for (int j = 0; j < processArr.Length; j++)
                {
                    if ((processArr[j].ArriveTime < earliest.ArriveTime) && (processArr[j].ArriveTime != 999))
                    {
                        earliest = processArr[j];
                        ptr = j;
                    }
                    else
                    {
                        ptr = i;
                        continue;
                    }
                }
                processArr[ptr] = dummy;
                temp.Add(earliest);
            }
            activeProcesses = temp;
        }

        /// <summary>
        /// SORTS PROCESSES BASED ON BURST TIME
        /// </summary>
        /// <param name="activeProcesses">LIST OF ACTIVE PROCESSES</param>
        protected void sortSize(ref List<Process> activeProcesses)
        {
            List<Process> temp = new List<Process>();
            Process[] processArr = activeProcesses.ToArray();
            Process dummy = new Process(999, 999, 999, 999);
            int ptr = 0;
            for (int i = 0; i < processArr.Length; i++)
            {
                Process shortest = processArr[i];
                ptr = i;
                for (int j = 0; j < processArr.Length; j++)
                {
                    if ((processArr[j].BurstTime < shortest.BurstTime) && (processArr[j].BurstTime != 999))
                    {
                        shortest = processArr[j];
                        ptr = j;
                    }
                    else continue;
                }
                processArr[ptr] = dummy;
                temp.Add(shortest);
            }
            activeProcesses = temp;
        }

        /// <summary>
        /// SORTS PROCESSES BASED ON PRIORITY
        /// LOW NUMBER -> HIGH PRIORITY
        /// </summary>
        /// <param name="activeProcesses">LIST OF ACTIVE PROCESSES</param>
        protected void sortPriority(ref List<Process> activeProcesses)
        {
            List<Process> temp = new List<Process>();
            Process[] processArr = activeProcesses.ToArray();
            Process dummy = new Process(999, 999, 999, 999);
            int ptr = 0;
            for (int i = 0; i < processArr.Length; i++)
            {
                Process mostImportant = processArr[i];
                ptr = i;
                for (int j = 0; j < processArr.Length; j++)
                {
                    if ((processArr[j].Priority < mostImportant.Priority) && (processArr[j].Priority != 999))
                    {
                        mostImportant = processArr[j];
                        ptr = j;
                    }
                    else continue;
                }
                processArr[ptr] = dummy;
                temp.Add(mostImportant);
            }
            
            processArr = temp.ToArray();
            for (ptr = 0; ptr < processArr.Length; ptr++)
            {
                processArr[ptr].Priority = ptr + 1;
            }
            activeProcesses = processArr.ToList<Process>();
        }
    }
}
