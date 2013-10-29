using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM310JobScheduler
{
    /// <summary>
    /// BASE CLASS FOR ALL CPU SCHEDULE ALGORITHMS
    /// </summary>
    public class Scheduler : Algorithm
    {
        public virtual string calculateGantt(List<Process> activeProcesses)
        {
            return "";
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
            for (int i = 0; i < processArr.Length; i++)
            {
                Process earliest = processArr[i];
                int ptr = 0;
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
            for (int i = 0; i < processArr.Length; i++)
            {
                Process shortest = processArr[i];
                int ptr = 0;
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
        /// </summary>
        /// <param name="activeProcesses">LIST OF ACTIVE PROCESSES</param>
        protected void sortPriority(ref List<Process> activeProcesses)
        {
            List<Process> temp = new List<Process>();
            Process[] processArr = activeProcesses.ToArray();
            Process dummy = new Process(999, 999, 999, 999);
            for (int i = 0; i < processArr.Length; i++)
            {
                Process mostImportant = processArr[i];
                int ptr = 0;
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
            activeProcesses = temp;
        }
    }
}
