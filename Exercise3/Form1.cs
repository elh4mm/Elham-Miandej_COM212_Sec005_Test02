
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
  

        private async void btnCalc_Click(object sender, EventArgs e)
            {
            outputTextBox.Text = "Starting Task to calculate Factorial(46)\r\n";


            Task<TimeData> task1 = Task.Run(() => StartFactorial(46));

            outputTextBox.AppendText(
               "Starting Task to calculate Factorial(45)\r\n");


            Task<TimeData> task2 = Task.Run(() => StartFactorial(45));

            outputTextBox.AppendText( "Starting Task to RollDie(60000000)\r\n");
            Task<TimeData> task3 = Task.Run(() => RollDie(60000000));

            await Task.WhenAll(task1, task2, task3);



            double totalMinutes = task3.Result.total + task2.Result.total + task1.Result.total;
            outputTextBox.AppendText(
               $"Total calculation time = {totalMinutes:F6} minutes\r\n");

         
        }

      
        TimeData StartFactorial(int n)
        {
        
            var result = new TimeData();
            AppendText($"Calculating Factorial({n})");
            result.StartTime = DateTime.Now;
            double value = GetFactorial(n);
            result.EndTime = DateTime.Now;

            AppendText($"Factorial({n}) = {value}");
            result.total =
               (result.EndTime - result.StartTime).TotalMinutes;
            AppendText($"Calculation time = {result.total:F6} minutes\r\n");
            return result;
        }

    
        private double GetFactorial(int number)

        {
            if (number == 1)
                return 1;
            else
                return number * GetFactorial(number - 1);

        }

      
        public void AppendText(String text)
        {
            if (InvokeRequired) 
            {
                Invoke(new MethodInvoker(() => AppendText(text)));
            }
            else 
            {
                outputTextBox.AppendText(text + "\r\n");
            }
        }

         TimeData RollDie(int number)
        {
            var result = new TimeData();
            result.StartTime = DateTime.Now;
          
            AppendText($"Calculating RollDie(60000000)");
         
            Random random = new Random();
            int c;
            int i = 0;
            int[] intArr = { 0, 0, 0, 0, 0, 0, 0 };
            int face = 0;
            while (i < number)
            {
                c = random.Next(1, 7);

                intArr[c] = ++intArr[c];

                i++;


            }

            int max = 1;

            for (int j = 0; j < intArr.Length;j++)
            {

                if (intArr[j] > max)
                {
                    max = intArr[j];
                    face = j;
                }
            }
            AppendText($"RollDie    The most fequent Face is \"{face}\" with number of {max} times");
            result.EndTime = DateTime.Now;
            result.total =
              (result.EndTime - result.StartTime).TotalMinutes;
            AppendText($"Calculation time = {result.total:F6} minutes\r\n");
            return result;

        }
    }
}

