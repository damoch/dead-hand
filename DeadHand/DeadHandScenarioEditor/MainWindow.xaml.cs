﻿using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Implementations;
using DeadHandScenarioEditor.View;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DeadHandScenarioEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScenarioBase _scenario;
        private WeatherDataEditor _weatherDataEditor;
        private DeviceSettingsData _deviceSettingsData;
        private EditEmailsWindow _editEmailsWindow;


        public MainWindow()
        {
            _scenario = new ScenarioBase();
            InitializeComponent();
        }

        private void WeatherDataEditor_WeatherServiceDataChanged(object? sender, Tuple<string, string> e)
        {
            _scenario.WeatherServiceData = e;
            _weatherDataEditor.Close();
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialoge = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            openFileDialoge.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialoge.FileName))
            {
                _scenario = ScenarioBase.FromJson(openFileDialoge.FileName);
                FilePath.Text = openFileDialoge.FileName;
                ScenarioNameTextBox.Text = _scenario.ScenarioName;
                ScenarioEndingLaunchText.Text = _scenario.EndingLaunchText;
                ScenarioEndingShutdownTextBox.Text = _scenario.EndingShutdownText;
                ActivationCodeTextBox.Text = _scenario.ActivationCode;
                ShutdownCodeTextBox.Text = _scenario.ShutdownCode;
                DelayCodeTextBox.Text = _scenario.DelayCode;
                RadioIDTextBox.Text = _scenario.RadioStationID;
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

        private void EditWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            _weatherDataEditor = new WeatherDataEditor();
            _weatherDataEditor.WeatherServiceDataChanged += WeatherDataEditor_WeatherServiceDataChanged;
            _weatherDataEditor.SetWeatherServiceData(_scenario.WeatherServiceData);
            _weatherDataEditor.ShowDialog();
        }

        private void EditDifficultyButton_Click(object sender, RoutedEventArgs e)
        {
            _deviceSettingsData = new DeviceSettingsData();
            _deviceSettingsData.SetupData(_scenario.MotherboardTemperature, _scenario.MemoryCacheUsedPercentage, _scenario.DiskFragmentationPercentage, _scenario.MotherboardTemperatureChanges, _scenario.MemoryCacheUsedPercentageChanges, _scenario.DiskFragmentationPercentageChanges);
            _deviceSettingsData.OnSave += DeviceSettingsData_OnSave;
            _deviceSettingsData.ShowDialog();
        }

        private void DeviceSettingsData_OnSave(object? sender, Tuple<int, int, int, Tuple<int, int>, Tuple<int, int>, Tuple<int, int>> e)
        {
            _scenario.MotherboardTemperature = e.Item1;
            _scenario.MemoryCacheUsedPercentage = e.Item2;
            _scenario.DiskFragmentationPercentage = e.Item3;
            _scenario.MotherboardTemperatureChanges = e.Item4;
            _scenario.MemoryCacheUsedPercentageChanges = e.Item5;
            _scenario.DiskFragmentationPercentageChanges = e.Item6;
            _deviceSettingsData.Close();
        }

        private void EditEmailsButton_Click(object sender, RoutedEventArgs e)
        {
            _editEmailsWindow = new EditEmailsWindow();
            _editEmailsWindow.SetEmails(_scenario.Emails);
            _editEmailsWindow.OnSave += EditEmailsWindow_OnSave;
            _editEmailsWindow.ShowDialog();
        }

        private void EditEmailsWindow_OnSave(object? sender, Dictionary<float, Email> e)
        {
            _scenario.Emails = e;
            _editEmailsWindow.Close();
        }

        private void CreateScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            //Create new scenario and clear editor fields
            _scenario = new ScenarioBase();
            _scenario.Emails = new Dictionary<float, Email>();
            _scenario.WeatherServiceData = new Tuple<string, string>("", "");
            _scenario.MotherboardTemperatureChanges = new Tuple<int, int>(0, 0);
            _scenario.MemoryCacheUsedPercentageChanges = new Tuple<int, int>(0, 0);
            _scenario.DiskFragmentationPercentageChanges = new Tuple<int, int>(0, 0);
            
            FilePath.Text = "";
            ScenarioNameTextBox.Text = "";
            ScenarioEndingLaunchText.Text = "";
            ScenarioEndingShutdownTextBox.Text = "";
            ActivationCodeTextBox.Text = "";
            ShutdownCodeTextBox.Text = "";
            DelayCodeTextBox.Text = "";
            RadioIDTextBox.Text = "";
        }

        private void SaveToJsonButton_Click(object sender, RoutedEventArgs e)
        {
            //open save dialoge
            var saveFileDialoge = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            saveFileDialoge.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialoge.FileName))
            {
                //save scenario to file
                _scenario.ScenarioName = ScenarioNameTextBox.Text;
                _scenario.EndingLaunchText = ScenarioEndingLaunchText.Text;
                _scenario.EndingShutdownText = ScenarioEndingShutdownTextBox.Text;
                _scenario.ActivationCode = ActivationCodeTextBox.Text;
                _scenario.ShutdownCode = ShutdownCodeTextBox.Text;
                _scenario.DelayCode = DelayCodeTextBox.Text;
                _scenario.RadioStationID = RadioIDTextBox.Text;
                _scenario.ToJson(saveFileDialoge.FileName);
                FilePath.Text = saveFileDialoge.FileName;
            }
        }

        private void ExportScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            //open save dialoge
            var saveFileDialoge = new Microsoft.Win32.SaveFileDialog
            {
                Filter = $"Scenario Files (*.{ScenarioBase.FileExtension})|*.{ScenarioBase.FileExtension}"
            };
            saveFileDialoge.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialoge.FileName))
            {
                //save scenario to file
                _scenario.ScenarioName = ScenarioNameTextBox.Text;
                _scenario.EndingLaunchText = ScenarioEndingLaunchText.Text;
                _scenario.EndingShutdownText = ScenarioEndingShutdownTextBox.Text;
                _scenario.ActivationCode = ActivationCodeTextBox.Text;
                _scenario.ShutdownCode = ShutdownCodeTextBox.Text;
                _scenario.DelayCode = DelayCodeTextBox.Text;
                _scenario.RadioStationID = RadioIDTextBox.Text;
                _scenario.ExportScenario(saveFileDialoge.FileName);
            }
        }
    }
}
