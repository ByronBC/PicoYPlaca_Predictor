using System;
using PP_Predictor;
using System.Windows.Forms;

namespace PredictorTester
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void cmdEvaluate_Click(object sender, EventArgs e)
        {
            Predictor predictor = new Predictor(txtPlate.Text, txtDate.Text, txtTime.Text);
            lblResult.Text = "Predictor Status: " + predictor.PredictorStatus.ToString();
            lblResult.Text += System.Environment.NewLine;
            lblResult.Text += "Can Be On The Road: " + predictor.CanBeOnTheRoad().ToString();
        }
    }
}
