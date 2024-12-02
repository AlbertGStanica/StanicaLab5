using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name: Albert Stanica
         * Date: November 2024
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: https://github.com/AlbertGStanica/StanicaLab5
         * */

        //class-level random object
        Random rand = new Random();
        private const String NAME = "Albert Stanica";

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            radOneRoll.Checked = true;
            //add your name to end of form title
            this.Text += NAME;
        } // end form load

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();
            
        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1, dice2;
            //call ftn RollDice, placing returned number into integers
            dice1 = RollDice();
            dice2 = RollDice();
            //place integers into labels
            lblDice1.Text = dice1 + "";
            lblDice2.Text = dice2 + string.Empty;
            // call ftn GetName sending total and returning name
            int total = dice1 + dice2;
            string rollName = GetName(total);
            //display name in label
            lblRollName.Text = rollName;
        }

        /* Name: ClearOneRoll
        *  Sent: nothing
        *  Return: nothing
        *  Clear the labels */
        private void ClearOneRoll()
        {
            lblDice1.Text = "";
            lblDice2.Text = "";
            lblRollName.Text = "";
        }

        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */
        private void ClearStats()
        {
            nudNumber.Value = nudNumber.Minimum;
            chkSeed.Checked = false;
            lblPass.Text = "";
            lblFail.Text = "";
            lblAverage.Text = "";
            lstMarks.Items.Clear();
        }

        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */
        private int RollDice()
        {
            return rand.Next(1 , 7);
        }

        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/
        private string GetName(int total)
        {
            string rollName = "";
            switch (total)
            {
                case 2:
                    rollName = "Snake Eyes";
                    break;
                case 3:
                    rollName = "Litle Joe";
                    break;
                case 5:
                    rollName = "Fever";
                    break;
                case 7:
                    rollName = "Most Common";
                    break;
                case 9:
                    rollName = "Center Field";
                    break;
                case 11:
                    rollName = "Yo-level";
                    break;
                case 12:
                    rollName = "Boxcars";
                    break;
                default:
                    rollName = "";
                    break;
            }
            return rollName;
        }
        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            //call ftn DataPresent twice sending string returning boolean
            bool data1Present, data2Present;
            string dataDice1, dataDice2;
            dataDice1 = lblDice1.Text;
            dataDice2 = lblDice2.Text;
            data1Present = DataPresent(dataDice1);
            data2Present = DataPresent(dataDice2);
            
            //if data present in both labels, call SwapData sending both strings
            if (data1Present && data2Present)
            {
                SwapData(ref dataDice1,ref dataDice2);
                // Swap Data
            }
            else//if data not present in either label display error msg
            {
                MessageBox.Show("Roll the dice","Data Missing");
            }
            //put data back into labels
            lblDice1.Text = dataDice1;
            lblDice2.Text = dataDice2;
            
        }

        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/
        private bool DataPresent(string input)
        {
            bool present;
            switch(input){
                case "":
                    present = false;
                    break;
                default:
                    present = true;
                    break;
            }
            return present;
        }

        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/
        private void SwapData(ref string input1, ref string input2)
        {
            string container = input1;
            input1 = input2;
            input2 = container;
        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            if (radOneRoll.Checked)
            {
                grpMarkStats.Hide();
                grpOneRoll.Show();
                ClearStats();
            }
            else
            {
                grpOneRoll.Hide();
                grpMarkStats.Show();
                ClearOneRoll();
            }
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeed.Checked)
            {
                DialogResult selection = MessageBox.Show("Are you sure you want to use a seed value?", "Confirm Seed Value", MessageBoxButtons.YesNo);
                if (selection == DialogResult.No)
                {
                    chkSeed.Checked = false;
                }
            }
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //declare variables and array
            int[] valueofnud = new int[(int)nudNumber.Value];
            //check if seed value
            if (chkSeed.Checked)
            {
                rand = new Random(1000);
            }
            //fill array using random number
            for(int i = 0; i < valueofnud.Length; i++)
            {
                valueofnud[i] = rand.Next(40,101);
            }
            //call CalcStats sending and returning data
            int pass = 0, fail = 0;
            double avg = CalcStats(valueofnud,ref pass, ref fail);
            //display data sent back in labels - average, pass and fail
            // Format average always showing 2 decimal places 
            lblPass.Text = pass + string.Empty;
            lblFail.Text = fail + string.Empty;
            lblAverage.Text = avg.ToString("n2");

        } // end Generate click

        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        private double CalcStats(int[] array, ref int pass, ref int fail)
        {
            double passMark = 60.0;
            int total = 0;
            foreach(int i in array)
            {
                if (i >= passMark)
                {
                    pass++;
                }
                else
                {
                    fail++;
                }
                total += i;
                lstMarks.Items.Add(i);
            }

            return total /(double)array.Length;
        }
    }
}
