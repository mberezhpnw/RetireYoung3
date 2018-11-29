using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace RetireYoung3
{
    
    public class Calculate
    {
        private double income;
        private double expense;
        private double roi;
        private double withDrawRate;
        private double savingsPct;
        private double currentPortfolio;
        private double yearToRetire;
       
        public double Income { get { return income; } set { income = value; } }
        public double Expense { get { return expense; } set { expense = value; } }
        public double ROI { get { return roi; } set { roi = value; } }
        public double WithDrawRate { get { return withDrawRate; } set { withDrawRate = value; } }
        public double SavingsPct { get { return savingsPct; } set { savingsPct = value; } }
        public double CurrentPortfolio { get { return currentPortfolio; } set { currentPortfolio = value; } }
        public double YearsToRetire { get { return yearToRetire; } set { yearToRetire = value; } }


        //public Calculate(double income, double expense, double roi, double withDrawRate, double savingsPct, double currentPortf)
        //{
        //    income = Income;
        //    expense = Expense;
        //    roi = ROI;
        //    withDrawRate = WithDrawRate;
        //    savingsPct = SavingsPct;
        //    currentPortf = CurrentPortfolio;
        //}

        //public double YearsToRetire(double roi, double savingsPct, double income, double withDrawRate, double currentPortfolio )
        //{
        //    var numerator = Math.Log((((roi * (1 - savingsPct) * income) / withDrawRate) + (savingsPct * income)) /
        //        ((roi * currentPortfolio) + (savingsPct * income)));
        //    var denominator = Math.Log(1 + roi);
        //    return numerator / denominator;
        //}

        //public double YearsToRetire()
        //{
        //    var numerator = Math.Log((((roi * (1 - savingsPct) * income) / withDrawRate) + (savingsPct * income)) /
        //        ((roi * currentPortfolio) + (savingsPct * income)));
        //    var denominator = Math.Log(1 + roi);
        //    return numerator / denominator;
        //}


        //List<Stats> stats = new List<Stats>();

        public List<Stats> GenerateData()
        {

            List<Stats> stats = new List<Stats>();
            List<int> counter = new List<int>();

            for (int i = 0; i < YearsToRetire + 2; i++)
            {

                Stats s = new Stats();

                s.EOY = i;
                s.AnnualIncome = income;
                s.AnnualExpense = expense;
                s.AnnualSavings = (income - expense);
                s.ROI = roi;
                s.WithDrawRate = withDrawRate;
                s.ExpenseCoveredByROI = 0;
                s.NetWorth = 0;
                
                stats.Add(s);

                if (stats[0].EOY == 0)
                {
                    stats[0].AnnualIncome = 0;
                    stats[0].AnnualExpense = 0;
                    stats[0].AnnualSavings = 0;
                    stats[0].ROI = 0;
                    stats[0].ChangeInNetWorth = 0;
                    stats[0].NetWorth = currentPortfolio;

                }

                if (i > 0)
                {

                    s.ROI = (stats[i - 1].NetWorth + (stats[i].AnnualSavings / 2)) * roi;
                    s.ChangeInNetWorth = stats[i].AnnualSavings + stats[i].ROI;
                    s.NetWorth = stats[i].ChangeInNetWorth + stats[i - 1].NetWorth;

                }

                if (i > 0)
                {
                    stats[i].ExpenseCoveredByROI = stats[i].ROI / stats[i].AnnualExpense;
                }

            }
            return stats;

        }

    }


    public class Stats
    {
        public double EOY { get; set; }
        public double AnnualIncome { get; set; }
        public double AnnualExpense { get; set; }
        public double AnnualSavings { get; set; }
        public double ROI { get; set; }
        public double WithDrawRate { get; set; }
        public double ExpenseCoveredByROI { get; set; }
        public double ChangeInNetWorth { get; set; }
        public double NetWorth { get; set; }
        public double YearsToRetire { get; set; }

    }
}

