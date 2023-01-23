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
    /// Interaction logic for WeatherDataEditor.xaml
    /// </summary>
    public partial class WeatherDataEditor : Window
    {
        //public event handler taking 2 strings parameters
        public event EventHandler<Tuple<string, string>> WeatherServiceDataChanged;
        public WeatherDataEditor()
        {
            InitializeComponent();
        }

        //a method to set values to textboxes
        public void SetWeatherServiceData(Tuple<string, string> serviceValues)
        {
            SeviceNameTextBox.Text = serviceValues.Item1;
            WeatherServiceText.Text = serviceValues.Item2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WeatherServiceDataChanged.Invoke(this, new Tuple<string, string>(SeviceNameTextBox.Text, WeatherServiceText.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
