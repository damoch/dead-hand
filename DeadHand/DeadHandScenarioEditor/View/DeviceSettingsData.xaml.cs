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
using System.Windows.Shapes;

namespace DeadHandScenarioEditor.View
{
    /// <summary>
    /// Interaction logic for DeviceSettingsData.xaml
    /// </summary>
    public partial class DeviceSettingsData : Window
    {
        public DeviceSettingsData()
        {
            InitializeComponent();
        }

        public Action<object?, Tuple<int, int, int, Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>> OnSave { get; internal set; }

        internal void SetupData(int motherboardTemperature, 
            int memoryCacheUsedPercentage, 
            int diskFragmentationPercentage, 
            Tuple<int, int> motherboardTemperatureChanges, 
            Tuple<int, int> memoryCacheUsedPercentageChanges, 
            Tuple<int, int> diskFragmentationPercentageChanges)
        {
            BaseMotherboardTemperatureTextBox.Text = motherboardTemperature.ToString();
            BaseMemoryCacheUsedPercentageTextBox.Text = memoryCacheUsedPercentage.ToString();
            BaseDiskFragmentationPercentageTextBox.Text = diskFragmentationPercentage.ToString();
            MotherboardTemperatureChangesHighTextBox.Text = motherboardTemperatureChanges.Item2.ToString();
            MotherboardTemperatureChangesLowTextBox.Text = motherboardTemperatureChanges.Item1.ToString();
            MemoryCacheUsedPercentageChangesHighTextBox.Text = memoryCacheUsedPercentageChanges.Item2.ToString();
            MemoryCacheUsedPercentageChangesLowTextBox.Text = memoryCacheUsedPercentageChanges.Item1.ToString();
            DiskFragmentationChangeHighTextBox.Text = diskFragmentationPercentageChanges.Item2.ToString();
            DiskFragmentationChangeLowTextBox.Text = diskFragmentationPercentageChanges.Item1.ToString();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            OnSave.Invoke(this, new Tuple<int, int, int, Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                int.Parse(BaseMotherboardTemperatureTextBox.Text),
                int.Parse(BaseMemoryCacheUsedPercentageTextBox.Text),
                int.Parse(BaseDiskFragmentationPercentageTextBox.Text),
                new Tuple<int, int>(int.Parse(MotherboardTemperatureChangesLowTextBox.Text), int.Parse(MotherboardTemperatureChangesHighTextBox.Text)),
                new Tuple<int, int>(int.Parse(MemoryCacheUsedPercentageChangesLowTextBox.Text), int.Parse(MemoryCacheUsedPercentageChangesHighTextBox.Text)),
                new Tuple<int, int>(int.Parse(DiskFragmentationChangeLowTextBox.Text), int.Parse(DiskFragmentationChangeHighTextBox.Text))
           ));
        }
    }
}
