using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCCApp.Helpers;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace FCCApp
{
    public partial class Form1 : Form
    {
        static string ModelFilePath = Dataset.GetAbsolutePath("../../../MLNetSampleFCCML.Model/MLModel.zip");
        public Form1()
        {
            InitializeComponent();
            LoadData();
            PaintChart();
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
        }

        int _predictionIndex = 0;

        static List<ScorePrediction> predictions;
        int _resultCount = 0;

        void LoadData()
        {
            Predictor predictor = new Predictor(ModelFilePath);
            predictions = predictor.RunMultiplePredictions(10);
            _resultCount = predictions.Count() - 1;
        }

        void PaintChart()
        {
            PlotModel chart = Chart.GetPlotModel(predictions[_predictionIndex]);
            lblTripID.Text = (_predictionIndex + 1).ToString();
            string predictedAmount = String.Format("{0:n2}", Convert.ToDecimal(predictions[_predictionIndex].Score));
            lblFare.Text = predictedAmount;
            this.plot1.Model = chart;
        }

        internal static class Chart
        {
            public static PlotModel GetPlotModel(ScorePrediction prediction)
            {
                var model = new PlotModel { Title = "Score Prediction Impact per Feature" };

                var listBarItem = from x in prediction.Features
                                  where x.Value > 0
                                  orderby x.Value 
                                  select new BarItem { Value = (x.Value) };
                 var barSeries = new BarSeries
                {
                    ItemsSource = listBarItem,
                    LabelPlacement = LabelPlacement.Inside,
                    LabelFormatString = "{0:.00}"
                };

                model.Series.Add(barSeries);

                var listName = from x in prediction.Features
                               where x.Value > 0
                               orderby x.Value 
                               select x.Name;
               
                model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Left,
                    Key = "FeatureAxis",
                    ItemsSource = listName
                }
                );

                return model;
            }


        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (_predictionIndex < _resultCount)
            {
                _predictionIndex++;
                PaintChart();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (_predictionIndex > 0)
            {
                _predictionIndex--;
                PaintChart();
            }
        }
    }
}
