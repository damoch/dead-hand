using DeadHand.Scenarios.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeadHandScenarioEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScenarioBase _scenario;
        
        public MainWindow()
        {
            _scenario = new ScenarioBase();
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialoge = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            openFileDialoge.ShowDialog();
            if (openFileDialoge.FileName != null)
            {
                _scenario = ScenarioBase.FromJson(openFileDialoge.FileName);
                FilePath.Text = openFileDialoge.FileName;
                ScenarioNameTextBox.Text = _scenario.ScenarioName;
                ScenarioEndingLaunchText.Text = _scenario.EndingLaunchText;
                ScenarioEndingShutdownTextBox.Text = _scenario.EndingShutdownText;
                ActivationCodeTextBox.Text = _scenario.ActivationCode;
                ShutdownCodeTextBox.Text = _scenario.ShutdownCode;
                //delayCodeTextBox.Text = _scenario.DelayCode;
                //radioStationIDTextBox.Text = _scenario.RadioStationID;
                //weatherServiceDataTextBox.Text = _scenario.WeatherServiceData.Item1;
                //weatherServiceDataTextBox.Text = _scenario.WeatherServiceData.Item2;
                //motherboardTemperatureTextBox.Text = _scenario.MotherboardTemperature.ToString();
                //memoryCacheUsedPercentageTextBox.Text = _scenario.MemoryCacheUsedPercentage.ToString();
                //diskFragmentationPercentageTextBox.Text = _scenario.DiskFragmentationPercentage.ToString();
                //motherboardTemperatureChangesTextBox.Text = _scenario.MotherboardTemperatureChanges.Item1.ToString();
                //motherboardTemperatureChangesTextBox.Text = _scenario.MotherboardTemperatureChanges.Item2.ToString();
                //memoryCacheUsedPercentageChangesTextBox.Text = _scenario.MemoryCacheUsedPercentageChanges.Item1.ToString();
                //memoryCacheUsedPercentageChangesTextBox.Text = _scenario.MemoryCacheUsedPercentageChanges.Item2.ToString();
                //diskFragmentationPercentageChangesTextBox.Text = _scenario.DiskFragmentationPercentageChanges.Item1.ToString();
                //diskFragmentationPercentageChangesTextBox.Text = _scenario.DiskFragmentationPercentageChanges.Item2.ToString();
            }
        }
    }
}
