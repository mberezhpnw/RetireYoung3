using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RetireYoung3
{
    class ViewModel : INotifyPropertyChanged
    {
        Calculate calc = new Calculate();

        public ViewModel()
        {
          
            SimpleBarModel = CreateSimpleBarChart();
            ColumnModel = CreateColumnModel();
            RecalculateCommand = new Command(RecalcChart);
            
        }
        

        double annualIncome = 50000;
        double currentPort = 1000;
        double annualSavings;
        double annualExpense;
        double annualSavingsPct = .6;
        double annualROI = .07;
        double annualWithdrawRate = .04;


        double lblYrsRetire, lblMonthlyExp, lblMonthlySavings, lblNetworth;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand RecalculateCommand { get; private set; }

        public double AnnualIncome
        {
            set
            {
                if (value >= 500 && value < 1000000000000)
                {

                    if (annualIncome != value)
                    {

                        annualIncome = value;
                        OnPropertyChanged("AnnualIncome");
                        Recalculate();

                    }
                }
            }
            get
            { return annualIncome; }
        }

        public double CurrentPort
        {
            set
            {
                if (value <= AnnualIncome * 2)
                {
                    if (currentPort != value)
                    {
                        currentPort = value;
                        OnPropertyChanged("CurrentPort");
                        Recalculate();

                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Invalid Numbers", "Your portfolio dollars seem high.", "OK");

                }
            }
            get
            { return currentPort; }
        }

        public double AnnualSavings
        {
            set
            {
                if (annualSavings != value)
                {
                    annualSavings = value;
                    OnPropertyChanged("AnnualSavings");
                    Recalculate();
                   
                }
            }
            get
            { return annualSavings; }
        }

        public double AnnualExpense
        {
            set
            {
                if (annualExpense != value)
                {
                    annualExpense = value;
                    OnPropertyChanged("AnnualExpense");
                    Recalculate();
                   
                }
            }
            get
            { return annualExpense; }
        }

        public double AnnualSavingsPct
        {
            set
            {
                if (annualSavingsPct != value)
                {
                    annualSavingsPct = value;
                    OnPropertyChanged("AnnualSavingsPct");
                    Recalculate();
                    
                }
            }
            get
            { return annualSavingsPct; }
        }

        public double AnnualROI
        {
            set
            {
                if (annualROI != value)
                {
                    annualROI = value;
                    OnPropertyChanged("AnnualROI");
                    Recalculate();
                    
                }

            }
            get
            { return annualROI; }
        }

        public double AnnualWithdrawRate
        {
            set
            {
                if (annualWithdrawRate != value)
                {
                    annualWithdrawRate = value;
                    OnPropertyChanged("AnnualWithdrawRate");
                    Recalculate();
                    calc.WithDrawRate = value;

                }
            }
            get
            { return annualWithdrawRate; }
        }

        public double YearsToRetire
        {
            set
            {
                if (lblYrsRetire != value)
                {
                    lblYrsRetire = value;
                    OnPropertyChanged("YearsToRetire");
                  
                }
            }
            get
            { return lblYrsRetire; }
        }

        public double MonthlyExpense
        {
            set
            {
                if (lblMonthlyExp != value)
                {
                    lblMonthlyExp = value;
                    OnPropertyChanged("MonthlyExpense");
                  
                    //Recalculate();
                }
            }
            get
            { return lblMonthlyExp; }
        }

        public double MonthlySavings
        {
            set
            {
                if (lblMonthlySavings != value)
                {
                    lblMonthlySavings = value;
                    OnPropertyChanged("MonthlySavings");
                   
                    //Recalculate();
                }
            }
            get
            { return lblMonthlySavings; }
        }

        public double Networth
        {
            set
            {
                if (lblNetworth != value)
                {
                    lblNetworth = value;
                    OnPropertyChanged("Networth");
                    
                }
            }
            get
            { return lblNetworth; }
        }


        void Recalculate()
        {
            if (AnnualIncome > 1000 || AnnualIncome != double.NaN)
            {



                this.AnnualSavings = (this.AnnualSavingsPct * this.AnnualIncome);
                this.AnnualExpense = this.AnnualIncome - this.AnnualSavings;
                this.AnnualSavingsPct = (this.AnnualIncome - this.AnnualExpense) / this.AnnualIncome;

                var numerator = Math.Log((((this.AnnualROI * (1 - this.AnnualSavingsPct) * this.AnnualIncome) / this.AnnualWithdrawRate) + (this.AnnualSavingsPct * this.AnnualIncome)) /
                    ((this.AnnualROI * this.CurrentPort) + (this.AnnualSavingsPct * this.AnnualIncome)));
                var denominator = Math.Log(1 + this.annualROI);

                this.YearsToRetire = numerator / denominator;
                this.MonthlyExpense = this.AnnualExpense / 12;
                this.MonthlySavings = this.AnnualSavings / 12;

                calc.Income = this.AnnualIncome;
                calc.Expense = this.AnnualExpense;
                calc.CurrentPortfolio = this.CurrentPort;
                calc.SavingsPct = this.AnnualSavingsPct;
                calc.ROI = this.AnnualROI;
                calc.WithDrawRate = this.AnnualWithdrawRate;
                calc.YearsToRetire = this.YearsToRetire;

                List<Stats> data = calc.GenerateData();

                this.Networth = data[(int)this.YearsToRetire].NetWorth;
            }
           
        }

        void RecalcChart()
        {
            if (this.YearsToRetire <= 105)
            {
                SimpleBarModel.PlotView.InvalidatePlot();
                SimpleBarModel = CreateSimpleBarChart();
                ColumnModel.PlotView.InvalidatePlot();
                ColumnModel = CreateColumnModel();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Invalid Numbers", "You won't be alive to enjoy your retirement!", "OK");
                return;
            }
        }

        
        //private PlotModel lineModel;
        //public PlotModel LineModel
        //{
        //    get { return lineModel; }
        //    set
        //    {
        //        if (lineModel != value)
        //        {
        //            lineModel = value;
        //            OnPropertyChanged("LineModel");

        //            Recalculate();
        //        }
        //    }
        //}
        


        //public PlotModel CreateLineChart()
        //{
           
        //    LineModel = new PlotModel { Title = string.Format("{0:P0} Return on Investment", this.AnnualROI) };

           
        //    LineModel.Series.Clear();
        //    LineModel.PlotType = PlotType.XY;

        //    calc.Income = this.AnnualIncome;
        //    calc.Expense = this.AnnualExpense;
        //    calc.CurrentPortfolio = this.CurrentPort;
        //    calc.SavingsPct = this.AnnualSavingsPct;
        //    calc.ROI = this.AnnualROI;
        //    calc.WithDrawRate = this.AnnualWithdrawRate;
        //    calc.YearsToRetire = this.YearsToRetire;

        //    List<Stats> data = calc.GenerateData();
            
        //    List<DataPoint> listPointAray = new List<DataPoint>();
        //    listPointAray.Clear();

        //    var ls = new LineSeries();

        //    for (int i = 0; i < this.YearsToRetire + 2; i++)
        //    {
        //        double years = i;
        //        double netWorth = data[i].NetWorth;
        //        double income = data[i].AnnualIncome;
        //        listPointAray.Add(new DataPoint(netWorth, years));
        //    }

        //    ls.ItemsSource = listPointAray;

        //    LineModel.Series.Add(ls);
           
        //    return LineModel;

        //}

        private PlotModel simpleBarModel;
        public PlotModel SimpleBarModel
        {
            get { return simpleBarModel; }
            set
            {
                if (simpleBarModel != value)
                {
                    simpleBarModel = value;
                    OnPropertyChanged("SimpleBarModel");
                    Recalculate();
                }
            }
        }

        private PlotModel CreateSimpleBarChart()
        {
            SimpleBarModel = new PlotModel { Title = string.Format("Yearly Expenses Covered by {0:P0} ROI", this.AnnualROI), TextColor=OxyColors.DarkSlateGray };
            SimpleBarModel.Series.Clear();
            SimpleBarModel.PlotType = PlotType.XY;
            SimpleBarModel.PlotAreaBorderColor = OxyColors.Transparent;
            

            calc.Income = this.AnnualIncome;
            calc.Expense = this.AnnualExpense;
            calc.CurrentPortfolio = this.CurrentPort;
            calc.SavingsPct = this.AnnualSavingsPct;
            calc.ROI = this.AnnualROI;
            calc.WithDrawRate = this.AnnualWithdrawRate;
            calc.YearsToRetire = this.YearsToRetire;

            List<Stats> data = calc.GenerateData();


            CategoryAxis yaxis = new CategoryAxis();
            yaxis.Position = AxisPosition.Left;
            yaxis.IsPanEnabled = false;
            //yaxis.Minimum = 0;
            yaxis.MajorGridlineStyle = LineStyle.None;
            yaxis.MinorGridlineStyle = LineStyle.None;
            yaxis.MaximumPadding = .05;
            yaxis.MinimumPadding = 0.01;
            yaxis.TextColor = OxyColors.DarkSlateGray;
            yaxis.TicklineColor = OxyColors.Transparent;
            for (int i = (int)YearsToRetire + 1; i > 0; i--)
            {
                yaxis.Labels.Add(string.Format("{0:F0}", i));
            }

            LinearAxis xaxis = new LinearAxis();
            xaxis.Position = AxisPosition.Bottom;
            xaxis.IsPanEnabled = false;
            xaxis.Minimum = 0;
            xaxis.LabelFormatter = (x) => { return string.Format("{0:P0}", x); };
            xaxis.MaximumPadding = 0.1;
            xaxis.MinimumPadding = 0.05;
            xaxis.IsAxisVisible = false;

            xaxis.MajorGridlineStyle = LineStyle.None;
            xaxis.MinorGridlineStyle = LineStyle.None;

            List<BarItem> barItems = new List<BarItem>();

            BarSeries b1 = new BarSeries();
            for (int i = (int)YearsToRetire + 1; i > 0; i--)
            {
                barItems.Add(new BarItem(data[i].ExpenseCoveredByROI));
            }

            b1.LabelPlacement = LabelPlacement.Outside;
            b1.LabelFormatString = " {0:P0}";
            b1.TextColor = OxyColors.DarkSlateGray;
            b1.FillColor = OxyColors.Fuchsia;
            b1.ItemsSource = barItems;

            SimpleBarModel.Axes.Add(xaxis);
            SimpleBarModel.Axes.Add(yaxis);
            SimpleBarModel.Series.Add(b1);

            return SimpleBarModel;

        }

        class CustomColumnItem : ColumnItem
        {
            public CustomColumnItem(double value, int categoryIndex = -1) : base(value, categoryIndex)
            {
            }

            public string CurrencyFormat
            {
                get
                {
                    if (this.Value > 1000000)
                        return string.Concat(string.Format("{0:C1}", this.Value / 1000000), "m");
                    else if (this.Value > 9999)
                        return string.Concat(string.Format("{0:C0}", this.Value / 1000), "k");
                    else if (this.Value > 999)
                        return string.Concat(string.Format("{0:C1}", this.Value / 1000), "k");
                    else
                        return string.Format("{0:C0}", this.Value);
                }
            }
        }

        private PlotModel columnModel;
        public PlotModel ColumnModel
        {
            get { return columnModel; }
            set
            {
                if (columnModel != value)
                {
                    columnModel = value;
                    OnPropertyChanged("ColumnModel");
                    Recalculate();
                }
            }
        }

        private PlotModel CreateColumnModel()
        {
            ColumnModel = new PlotModel { Title = string.Format("Networth in {0:F1} years", this.YearsToRetire), TextColor = OxyColors.DarkSlateGray };
            ColumnModel.Series.Clear();
            ColumnModel.PlotType = PlotType.XY;
            ColumnModel.PlotAreaBorderColor = OxyColors.Transparent;


            calc.Income = this.AnnualIncome;
            calc.Expense = this.AnnualExpense;
            calc.CurrentPortfolio = this.CurrentPort;
            calc.SavingsPct = this.AnnualSavingsPct;
            calc.ROI = this.AnnualROI;
            calc.WithDrawRate = this.AnnualWithdrawRate;
            calc.YearsToRetire = this.YearsToRetire;

            List<Stats> data = calc.GenerateData();

            CategoryAxis xaxis = new CategoryAxis();
            xaxis.Position = AxisPosition.Bottom;
            xaxis.IsPanEnabled = false;
            xaxis.Minimum = 0;
            xaxis.MajorGridlineStyle = LineStyle.None;
            xaxis.MinorGridlineStyle = LineStyle.None;
            xaxis.MaximumPadding = 0.05;
            xaxis.TextColor = OxyColors.DarkSlateGray;
            xaxis.TicklineColor = OxyColors.Transparent;
            for (int i = 0; i < YearsToRetire + 1; i++)
            {
                xaxis.Labels.Add(string.Format("{0:F0}", i));
            }

            LinearAxis yaxis = new LinearAxis();
            yaxis.Position = AxisPosition.Left;
            yaxis.IsPanEnabled = false;
            yaxis.Minimum = 0;
            yaxis.IsAxisVisible = false;
            //yaxis.LabelFormatter = (x) => { return string.Format("{0:C0}", x); };
            yaxis.MaximumPadding = .1;


            yaxis.MajorGridlineStyle = LineStyle.None;
            yaxis.MinorGridlineStyle = LineStyle.None;

            List<CustomColumnItem> columnItems = new List<CustomColumnItem>();

            ColumnSeries s1 = new ColumnSeries();
            for (int i = 0; i < YearsToRetire + 1; i++)
            {
                columnItems.Add(new CustomColumnItem(data[i].NetWorth));
            }

            s1.LabelPlacement = LabelPlacement.Outside;
            s1.LabelFormatString = "{CurrencyFormat}";
            s1.FillColor = OxyColors.Fuchsia;
            s1.ItemsSource = columnItems;

            ColumnModel.Axes.Add(xaxis);
            ColumnModel.Axes.Add(yaxis);
            ColumnModel.Series.Add(s1);

            return ColumnModel;

        }


    }

    


}



