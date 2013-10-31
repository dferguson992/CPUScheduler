using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/// AUTHOR: DAN FERGUSON

namespace COM310JobScheduler
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //  ASSUMPTIONS MADE:
    //  1) DEFAULT SORTING ALGORITHM IS FCFS
    //  2) ALL BURST TIMES MUST BE BETWEEN 1 - 10 TO EXECUTE
    //  3) A PROCESS THAT IS NOT BEING USED WILL NOT BE CHECKED
    //  4) ALL PRIORITIES ARE NON-UNIQUE AND BETWEEN 1 - 10
    //  5) ALL PROCESSES ARRIVE AT THE SAME TIME
    //  6) IN CASE OF TIE IN PRIORITY, PREFERENCE GOES TO PROCESS ID CLOSEST TO 1

    public partial class Form1 : Form
    {

        #region Maintenance Code 1

        Random newBurst = new Random();
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Before asking for the Gantt Chart, please click \n\"Refresh\"\n to ensure all data is correct.\n" +
                            "Written by Colleen Fitzsimons and Dan Ferguson.\n" +
                            "COM310 CPU Scheduling Simulator, S. Jane Fritz");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  DEFAULT MESSAGE...
            cmbAlgorithm.SelectedItem = "First Come First Served";
            defaults();
        }
        public void defaults()
        {
            TextBox[] PrioTexts = new TextBox[] { txtPriority1, txtPriority2, txtPriority3, txtPriority4,
                txtPriority5, txtPriority6, txtPriority7, txtPriority8, txtPriority9, txtPriority10 };
            TextBox[] ArriveTexts = new TextBox[] { txtArrive1, txtArrive2, txtArrive3, txtArrive4,
                txtArrive5, txtArrive6, txtArrive7, txtArrive8, txtArrive9, txtArrive10 };

            //  MAKE "ARRIVAL" FORM ELEMENTS INVISIBLE
            ////    DID NOT IMPLEMENT ARRIVAL DUE TO TIME CONSTRAINTS
            ////    ELEMENTS REMAIN IN GUI FOR REV.2
            foreach (TextBox t in ArriveTexts)
            {
                t.Visible = false;
            }
            label18.Visible = false;

            //  SET DEFAULT QUANTUM
            txtQuantum.Text = "10";

            //  DISENABLE PRIORITY TEXT BOXES UPON STARTUP
            if (!cmbAlgorithm.SelectedItem.Equals("Priority"))
            {
                for (int i = 0; i < PrioTexts.Length; i++)
                {
                    PrioTexts[i].Enabled = false;
                }
            }

            //  DISENABLE QUANTUM TEXT BOX UPON STARTUP
            if (!cmbAlgorithm.SelectedItem.Equals("Round-Robin"))
            {
                txtQuantum.Enabled = false;
            }

            //  GET RANDOM NUMBERS ON STARTUP
            if (ckbRandomBurst.CheckState == CheckState.Checked)
            {
                fillWithRandom();
            }
        }

        private void ckbRandomBurst_CheckedChanged(object sender, EventArgs e)
        {
            TextBox[] BurstTexts = new TextBox[] { txtBurst1, txtBurst2, txtBurst3, txtBurst4, txtBurst5,
                txtBurst6, txtBurst7, txtBurst8, txtBurst9, txtBurst10 };
            TextBox[] PrioTexts = new TextBox[] { txtPriority1, txtPriority2, txtPriority3, txtPriority4,
                txtPriority5, txtPriority6, txtPriority7, txtPriority8, txtPriority9, txtPriority10 };
            TextBox[] ArriveTexts = new TextBox[] { txtArrive1, txtArrive2, txtArrive3, txtArrive4,
                txtArrive5, txtArrive6, txtArrive7, txtArrive8, txtArrive9, txtArrive10 };

            //  IF THE RANDOM BURST CHECK BOX IS CHECKED...
            if (ckbRandomBurst.CheckState == CheckState.Checked)
            {
                //  FILL ALL BURST BOXES AND PRIORITIES WITH RANDOM NUMBERS FROM 1 - 10;
                fillWithRandom();
            }
            else if (ckbRandomBurst.CheckState == CheckState.Unchecked)
            {
                //  OTHERWISE, CLEAR ALL THE TEXT BOXES FOR USER ENTRY
                for (int i = 0; i < BurstTexts.Length; i++)
                {
                    BurstTexts[i].Text = "";
                    PrioTexts[i].Text = "";
                    ArriveTexts[i].Text = "";
                }
            }
        }

        //  FILL ALL BURST ARRIVAL AND PRIORITY TEXT BOXES WITH RANDOM NUMBERS FROM 1 - 10
        public void fillWithRandom()
        {
            TextBox[] BurstTexts = new TextBox[] { txtBurst1, txtBurst2, txtBurst3, txtBurst4, txtBurst5,
                txtBurst6, txtBurst7, txtBurst8, txtBurst9, txtBurst10 };
            TextBox[] PrioTexts = new TextBox[] { txtPriority1, txtPriority2, txtPriority3, txtPriority4,
                txtPriority5, txtPriority6, txtPriority7, txtPriority8, txtPriority9, txtPriority10 };
            TextBox[] ArriveTexts = new TextBox[] { txtArrive1, txtArrive2, txtArrive3, txtArrive4,
                txtArrive5, txtArrive6, txtArrive7, txtArrive8, txtArrive9, txtArrive10 };
            for (int i = 0; i < BurstTexts.Length; i++)
            {
                BurstTexts[i].Text = genRandom(1, 10).ToString();
                PrioTexts[i].Text = genRandom(1, 10).ToString();
                ArriveTexts[i].Text = genRandom(1, 10).ToString();
            }
        }

        //  GENERATE RANDOM INTS FROM BOT TO TOP RANGE
        public int genRandom(int bot, int top)
        {
            return newBurst.Next(bot, top);
        }

        private void cmbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox[] PrioTexts = new TextBox[] { txtPriority1, txtPriority2, txtPriority3, txtPriority4,
                txtPriority5, txtPriority6, txtPriority7, txtPriority8, txtPriority9, txtPriority10 };

            //  ENABLE THE PRIORITY TEXT BOXES IF AND ONLY IF THE PRIORITY ALGORITHM IS SELECTED
            if (cmbAlgorithm.SelectedItem.Equals("Priority"))
            {
                for (int i = 0; i < PrioTexts.Length; i++)
                {
                    PrioTexts[i].Enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < PrioTexts.Length; i++)
                {
                    PrioTexts[i].Enabled = false;
                }
            }

            //  ENABLE/DISABLE THE QUANTUM TEXT BOX IF THE ALGORITHM IS/IS NOT ROUND-ROBIN
            if (cmbAlgorithm.SelectedItem.Equals("Round-Robin"))
            {
                txtQuantum.Enabled = true;
            }
            else
            {
                txtQuantum.Enabled = false;
            }
        }

        //  DETERMINE WHICH PROCESSES ARE ACTIVELY BEING USED BY THE USER
        #region
        public void refreshProcesses()
        {
            if (chkP1.CheckState == CheckState.Unchecked)
            {
                txtArrive1.Enabled = false;
                txtBurst1.Enabled = false;
                txtPriority1.Enabled = false;
                lblTurn1.Text = "X";
                lblWait1.Text = "X";
                lblWait1.ForeColor = Color.Black;
                lblTurn1.ForeColor = Color.Black;
            }
            else
            {
                txtArrive1.Enabled = true;
                txtBurst1.Enabled = true;
                txtPriority1.Enabled = true;
                lblTurn1.Text = "";
                lblWait1.Text = "";
            }
            if (chkP2.CheckState == CheckState.Unchecked)
            {
                txtArrive2.Enabled = false;
                txtBurst2.Enabled = false;
                txtPriority2.Enabled = false;
                lblTurn2.Text = "X";
                lblWait2.Text = "X";
                lblWait2.ForeColor = Color.Black;
                lblTurn2.ForeColor = Color.Black;
            }
            else
            {
                txtArrive2.Enabled = true;
                txtBurst2.Enabled = true;
                txtPriority2.Enabled = true;
                lblTurn2.Text = "";
                lblWait2.Text = "";
            }
            if (chkP3.CheckState == CheckState.Unchecked)
            {
                txtArrive3.Enabled = false;
                txtBurst3.Enabled = false;
                txtPriority3.Enabled = false;
                lblTurn3.Text = "X";
                lblWait3.Text = "X";
                lblWait3.ForeColor = Color.Black;
                lblTurn3.ForeColor = Color.Black;
            }
            else
            {
                txtArrive3.Enabled = true;
                txtBurst3.Enabled = true;
                txtPriority3.Enabled = true;
                lblTurn3.Text = "";
                lblWait3.Text = "";
            }
            if (chkP4.CheckState == CheckState.Unchecked)
            {
                txtArrive4.Enabled = false;
                txtBurst4.Enabled = false;
                txtPriority4.Enabled = false;
                lblTurn4.Text = "X";
                lblWait4.Text = "X";
                lblWait4.ForeColor = Color.Black;  
                lblTurn4.ForeColor = Color.Black;
            }
            else
            {
                txtArrive4.Enabled = true;
                txtBurst4.Enabled = true;
                txtPriority4.Enabled = true;
                lblTurn4.Text = "";
                lblWait4.Text = "";
            }
            if (chkP5.CheckState == CheckState.Unchecked)
            {
                txtArrive5.Enabled = false;
                txtBurst5.Enabled = false;
                txtPriority5.Enabled = false;
                lblTurn5.Text = "X";
                lblWait5.Text = "X";
                lblWait5.ForeColor = Color.Black;
                lblTurn5.ForeColor = Color.Black;
            }
            else
            {
                txtArrive5.Enabled = true;
                txtBurst5.Enabled = true;
                txtPriority5.Enabled = true;
                lblTurn5.Text = "";
                lblWait5.Text = "";
            }
            if (chkP6.CheckState == CheckState.Unchecked)
            {
                txtArrive6.Enabled = false;
                txtBurst6.Enabled = false;
                txtPriority6.Enabled = false;
                lblTurn6.Text = "X";
                lblWait6.Text = "X";
                lblWait6.ForeColor = Color.Black;
                lblTurn6.ForeColor = Color.Black;
            }
            else
            {
                txtArrive6.Enabled = true;
                txtBurst6.Enabled = true;
                txtPriority6.Enabled = true;
                lblTurn6.Text = "";
                lblWait6.Text = "";
            }
            if (chkP7.CheckState == CheckState.Unchecked)
            {
                txtArrive7.Enabled = false;
                txtBurst7.Enabled = false;
                txtPriority7.Enabled = false;
                lblTurn7.Text = "X";
                lblWait7.Text = "X";
                lblWait7.ForeColor = Color.Black;
                lblTurn7.ForeColor = Color.Black;
            }
            else
            {
                txtArrive7.Enabled = true;
                txtBurst7.Enabled = true;
                txtPriority7.Enabled = true;
                lblTurn7.Text = "";
                lblWait7.Text = "";
            }
            if (chkP8.CheckState == CheckState.Unchecked)
            {
                txtArrive8.Enabled = false;
                txtBurst8.Enabled = false;
                txtPriority8.Enabled = false;
                lblTurn8.Text = "X";
                lblWait8.Text = "X";
                lblWait8.ForeColor = Color.Black;
                lblTurn8.ForeColor = Color.Black;
            }
            else
            {
                txtArrive8.Enabled = true;
                txtBurst8.Enabled = true;
                txtPriority8.Enabled = true;
                lblTurn8.Text = "";
                lblWait8.Text = "";
            }
            if (chkP9.CheckState == CheckState.Unchecked)
            {
                txtArrive9.Enabled = false;
                txtBurst9.Enabled = false;
                txtPriority9.Enabled = false;
                lblTurn9.Text = "X";
                lblWait9.Text = "X";
                lblWait9.ForeColor = Color.Black;
                lblTurn9.ForeColor = Color.Black;
            }
            else
            {
                txtArrive9.Enabled = true;
                txtBurst9.Enabled = true;
                txtPriority9.Enabled = true;
                lblTurn9.Text = "";
                lblWait9.Text = "";
            }
            if (chkP10.CheckState == CheckState.Unchecked)
            {
                txtArrive10.Enabled = false;
                txtBurst10.Enabled = false;
                txtPriority10.Enabled = false;
                lblTurn10.Text = "X";
                lblWait10.Text = "X";
                lblWait10.ForeColor = Color.Black;
                lblTurn10.ForeColor = Color.Black;
            }
            else
            {
                txtArrive10.Enabled = true;
                txtBurst10.Enabled = true;
                txtPriority10.Enabled = true;
                lblTurn10.Text = "";
                lblWait10.Text = "";
            }
        #endregion
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //  CHANGE GUI ELEMENTS BASED ON SELECTED PROCESSES
            refreshProcesses();
            defaults();
            lblAvgTurn.Text = "";
            lblAvgWait.Text = "";
        }
        
        #endregion

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            #region Maintenance Code 2
            int q;
            TextBox[] BurstTexts = new TextBox[] { txtBurst1, txtBurst2, txtBurst3, txtBurst4, txtBurst5,
                txtBurst6, txtBurst7, txtBurst8, txtBurst9, txtBurst10 };
            TextBox[] PrioTexts = new TextBox[] { txtPriority1, txtPriority2, txtPriority3, txtPriority4,
                txtPriority5, txtPriority6, txtPriority7, txtPriority8, txtPriority9, txtPriority10 };
            TextBox[] ArriveTexts = new TextBox[] { txtArrive1, txtArrive2, txtArrive3, txtArrive4,
                txtArrive5, txtArrive6, txtArrive7, txtArrive8, txtArrive9, txtArrive10 };
            CheckBox[] Processes = new CheckBox[] { chkP1, chkP2, chkP3, chkP4, chkP5,chkP6, chkP7,
                chkP8, chkP9, chkP10 };
            int[] BurstTimes = new int[BurstTexts.Length];
            int[] PrioTimes = new int[PrioTexts.Length];
            bool cleared = true;

            //  MAKE SURE A SORTING ALGORITHM IS SELECTED
            if (cmbAlgorithm.SelectedItem.Equals(null))
                MessageBox.Show("You must select a sorting algorithm!");

            //  MAKE SURE ALL TEXT BOXES ARE NOT NULL OR EMPTY
            for (int i = 0; i < BurstTexts.Length; i++)
            {
                if (string.IsNullOrEmpty(BurstTexts[i].Text))
                {
                    MessageBox.Show("All Burst Times must be filled out!");
                    cleared = false;
                    break;
                }
                if (string.IsNullOrEmpty(PrioTexts[i].Text))
                {
                    MessageBox.Show("All Priorities must be filled out!");
                    cleared = false;
                    break;
                }
                if (string.IsNullOrEmpty(ArriveTexts[i].Text))
                {
                    MessageBox.Show("All Arrival Times must be filled out!");
                    cleared = false;
                    break;
                }
                if (string.IsNullOrEmpty(txtQuantum.Text))
                {
                    MessageBox.Show("Please enter a quantum.");
                    cleared = false;
                    break;
                }

                //  MAKE SURE THE BURST TIMES AND PRIORITIES ARE BETWEEN 1 AND 10
                int tempBurst = Convert.ToInt16(BurstTexts[i].Text);
                int tempPrio = Convert.ToInt16(PrioTexts[i].Text);
                if ((tempBurst > 0) && (tempBurst < 11))
                {
                    continue;
                }
                if ((tempPrio > 0) && (tempPrio < 11))
                {
                    continue;
                }
                else
                {
                    MessageBox.Show("Make sure all burst times are between 1 and 10!");
                    MessageBox.Show("Make sure all priorities are between 1 and 10!");
                    cleared = false;
                    break;
                }
            }

            //  MAKE SURE THAT THE QUANTUM TEXT BOX, BURST TEXT BOXES, PRIORITY TEXT BOXES ARE ALL NUMBERS
            try
            {
                q = Convert.ToInt16(txtQuantum.Text);
                for (int i = 0; i < BurstTimes.Length; i++)
                {
                    BurstTimes[i] = Convert.ToInt16(BurstTexts[i].Text);
                    PrioTimes[i] = Convert.ToInt16(PrioTexts[i].Text);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Please enter a properly formatted quantum, burst fields, or priorities.");
                cleared = false;
                Console.WriteLine(err.Message);
            }

            #endregion

            //  GET LIST OF ALL IN USE PROCESSES AND ADD THEM TO THE DATA STRUCTURE
            if (cleared)
            {
                List<Process> activeProcesses = new List<Process>();
                for (int i = 0; i < Processes.Length; i++)
                {
                    Process temp;
                    if (Processes[i].CheckState == CheckState.Checked)
                    {
                        if (cmbAlgorithm.SelectedItem.Equals("Priority"))
                        {
                            temp = new Process(i + 1, Convert.ToInt16(BurstTexts[i].Text), Convert.ToInt16(PrioTexts[i].Text),
                                Convert.ToInt16(ArriveTexts[i].Text));
                        }
                        else
                        {
                            temp = new Process(i + 1, Convert.ToInt16(BurstTexts[i].Text), -1, Convert.ToInt16(ArriveTexts[i].Text));
                        }
                        activeProcesses.Add(temp);
                    }
                }
                //  CREATE THE SCHEDULER WHICH AUTOMATICALLY GENERATES THE GANTT CHART
                ScheduleController CPUScheduler = new ScheduleController(activeProcesses, cmbAlgorithm.SelectedItem.ToString(),
                    Convert.ToInt16(txtQuantum.Text));


                //  OUTPUT THE GANTT CHART
                lstLog.Items.AddRange(CPUScheduler.Gantt);

                //  CALCULATE AVERAGES
                double avgWait = 0.0;
                int sumWait = 0;
                double avgTurn = 0.0;
                int sumTurn = 0;
                foreach (Process p in activeProcesses)
                {
                    sumWait += p.WaitTime;
                    sumTurn += p.TurnAroundTime;
                }
                avgWait = sumWait / activeProcesses.Count;
                avgTurn = sumTurn / activeProcesses.Count;

                lblAvgWait.Text = avgWait.ToString();
                lblAvgTurn.Text = avgTurn.ToString();

                //  UPDATE FORM ELEMENTS
                #region
                foreach (Process p in activeProcesses)
                {
                    switch (p.PID)
                    {
                        case 1:
                            {
                                lblWait1.ForeColor = Color.Red;
                                lblTurn1.ForeColor = Color.Red;
                                lblWait1.Text = p.WaitTime.ToString();
                                lblTurn1.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 2:
                            {
                                lblWait2.ForeColor = Color.Red;
                                lblTurn2.ForeColor = Color.Red;
                                lblWait2.Text = p.WaitTime.ToString();
                                lblTurn2.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 3:
                            {
                                lblWait3.ForeColor = Color.Red;
                                lblTurn3.ForeColor = Color.Red;
                                lblWait3.Text = p.WaitTime.ToString();
                                lblTurn3.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 4:
                            {
                                lblWait4.ForeColor = Color.Red;
                                lblTurn4.ForeColor = Color.Red;
                                lblWait4.Text = p.WaitTime.ToString();
                                lblTurn4.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 5:
                            {
                                lblWait5.ForeColor = Color.Red;
                                lblTurn5.ForeColor = Color.Red;
                                lblWait5.Text = p.WaitTime.ToString();
                                lblTurn5.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 6:
                            {
                                lblWait6.ForeColor = Color.Red;
                                lblTurn6.ForeColor = Color.Red;
                                lblWait6.Text = p.WaitTime.ToString();
                                lblTurn6.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 7:
                            {
                                lblWait7.ForeColor = Color.Red;
                                lblTurn7.ForeColor = Color.Red;
                                lblWait7.Text = p.WaitTime.ToString();
                                lblTurn7.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 8:
                            {
                                lblWait8.ForeColor = Color.Red;
                                lblTurn8.ForeColor = Color.Red;
                                lblWait8.Text = p.WaitTime.ToString();
                                lblTurn8.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 9:
                            {
                                lblWait9.ForeColor = Color.Red;
                                lblTurn9.ForeColor = Color.Red;
                                lblWait9.Text = p.WaitTime.ToString();
                                lblTurn9.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        case 10:
                            {
                                lblWait10.ForeColor = Color.Red;
                                lblTurn10.ForeColor = Color.Red;
                                lblWait10.Text = p.WaitTime.ToString();
                                lblTurn10.Text = p.TurnAroundTime.ToString();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                #endregion
            }   
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        
    }
}
