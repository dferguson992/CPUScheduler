using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM310JobScheduler
{
    /// <summary>
    /// MASTER SCHEDULE ORGANIZER.  USED TO DETERMINE WHICH ALGORITHM IS USED.
    /// </summary>
    public class ScheduleController
    {
        private List<Process> activeProcesses;
        private string scheduleType;
        private Algorithm selectedAlgorithm;
        private string gantt = "";
  
        /// <summary>
        /// THIS CONSTRUCTOR DETERMINES WHICH SCHEDULING ALGORITHM IS IN USE AND CALCULATES THE APPROPRIATE GANTT CHART
        /// </summary>
        /// <param name="activeProcesses">LIST OF PROCESSES FROM GUI</param>
        /// <param name="algorithm">TYPE OF ALGORITHM USED</param>
        public ScheduleController(List<Process> activeProcesses, string algorithm, int quantum)
        {
            this.activeProcesses = activeProcesses;
            this.scheduleType = algorithm;

            switch (scheduleType)
            {
                case "First Come First Served":
                {
                    selectedAlgorithm = new FCFS();
                    this.gantt = selectedAlgorithm.calculateGantt(activeProcesses);
                    break;
                }
                case "Shortest Job First":
                {
                    selectedAlgorithm = new SJF();
                    this.gantt = selectedAlgorithm.calculateGantt(activeProcesses);
                    break;
                }
                case "Priority":
                {
                    selectedAlgorithm = new Priority();
                    this.gantt = selectedAlgorithm.calculateGantt(activeProcesses);
                    break;
                }
                case "Round-Robin":
                {
                    selectedAlgorithm = new RR(quantum);
                    this.gantt = selectedAlgorithm.calculateGantt(activeProcesses);
                    break;
                }
                default:
                {
                    StringBuilder defaultGantt = new StringBuilder();
                    defaultGantt.AppendLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");   //50 tildas
                    defaultGantt.AppendLine("UNRECOGNIZED SCHEDULING ALGORITHM");
                    defaultGantt.AppendLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");   //50 tildas
                    this.gantt = defaultGantt.ToString();
                    break;
                }
            }
        }

        /// <summary>
        /// RETURNS THE NAME OF THE SCHEDULING ALGORITHM
        /// </summary>
        public string ScheduleType { get { return scheduleType; } }
        /// <summary>
        /// RETURNS THE LIST OF PROCESSES
        /// </summary>
        public List<Process> ActiveProcesses { get { return activeProcesses; } }
        /// <summary>
        /// RETURNS THE GANTT CHART AS A STRING[] TO BE ADDED TO THE LIST BOX
        /// </summary>
        public string[] Gantt 
        { 
            get 
            {
                char[] seperator = new char[] { '\n' };
                string[] itemsToAdd = gantt.Split(seperator);
                return itemsToAdd; 
            } 
        }


    }
}
