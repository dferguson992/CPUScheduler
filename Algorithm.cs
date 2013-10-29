using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM310JobScheduler
{
    /// <summary>
    /// THIS INTERFACE IS DESIGNED TO OUTLINE THE STRUCTURE OF ANY ALGORITHM CLASS
    /// </summary>
    public interface Algorithm
    {
        /// <summary>
        /// SEE IMPLEMENTATION
        /// </summary>
        /// <param name="activeProcesses">LIST OF PROCESSES FROM GUI</param>
        /// <returns>STRING REPRESENATION OF GANTT</returns>
        string calculateGantt(List<Process> activeProcesses);
    }
}
