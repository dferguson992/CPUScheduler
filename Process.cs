/// AUTHOR: DAN FERGUSON

namespace COM310JobScheduler
{
    /// <summary>
    /// DEFINES THE PROCESS OBJECT
    /// </summary>
    public class Process
    {
        private int pid;
        private int burstTime;
        private int initBurstTime;
        private int arriveTime;
        private int waitTime = 0;
        private int turnAroundTime = 0;
        private int priority = 0;
        private bool prioritized = false;

        /// <summary>
        /// CONSTRUCTOR FOR EVERY PROCESS OBJECT.  CONTAINS ALL NECESSARY INFORMATION FOR EACH PROCESS
        /// </summary>
        /// <param name="PID">PROCESS ID</param>
        /// <param name="burstTime">PROCESS BURST TIME</param>
        /// <param name="priority">PROCESS PRIORITY IF APPLICABLE.  IF NOT, PRIORITY IS -1</param>
        /// <param name="arriveTime">PROCESS ARRIVAL TIME</param>
        public Process(int pid, int burstTime, int priority, int arriveTime)
        {
            if (priority == -1)
                prioritized = false;
            else prioritized = true;

            this.pid = pid;
            this.burstTime = burstTime;
            this.initBurstTime = burstTime;
            this.priority = priority;
            this.arriveTime = arriveTime;
        }

        #region GETTERS AND SETTERS FOR PROCESS DATA MEMBERS
        public int PID { get { return pid; } }
        public int InitialBurst { get { return initBurstTime; } }
        public int BurstTime { get { return burstTime; } set { burstTime = value; } }
        public int ArriveTime { get { return arriveTime; } }
        public int WaitTime { get { return waitTime; } set { waitTime = value; } }
        public int TurnAroundTime { get { return turnAroundTime; } set { turnAroundTime = value; } }
        public int Priority { get { return priority; } set { priority = value; } }
        public bool Prioritized { get { return prioritized; } }
        #endregion
    }
}
