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
            MotherboardTemperatureChangesHighTextBox.Text = motherboardTemperatureChanges.Item1.ToString();
            MotherboardTemperatureChangesLowTextBox.Text = motherboardTemperatureChanges.Item2.ToString();
            MemoryCacheUsedPercentageChangesHighTextBox.Text = memoryCacheUsedPercentageChanges.Item1.ToString();
            MemoryCacheUsedPercentageChangesLowTextBox.Text = memoryCacheUsedPercentageChanges.Item2.ToString();
            DiskFragmentationChangeHighTextBox.Text = diskFragmentationPercentageChanges.Item1.ToString();
            DiskFragmentationChangeLowTextBox.Text = diskFragmentationPercentageChanges.Item2.ToString();

        }
    }
}
