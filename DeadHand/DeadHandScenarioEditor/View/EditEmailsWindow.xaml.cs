﻿using DeadHand.Commands.Implementations;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace DeadHandScenarioEditor.View
{
    /// <summary>
    /// Interaction logic for EditEmailsWindow.xaml
    /// </summary>
    public partial class EditEmailsWindow : Window
    {
        private Dictionary<float, Email> _emails;
        private float _emailId;
        private Email _email;

        public EditEmailsWindow()
        {
            InitializeComponent();
            EmailsListBox.SelectionChanged += EmailsListBox_SelectionChanged;
            ProgrammingTypeSelect.ItemsSource = Enum.GetValues(typeof(ProgrammingType)).Cast<ProgrammingType>();
            TimeOfArrivalTextBox.LostFocus += TimeOfArrivalTextBox_LostFocus;
        }

        private void TimeOfArrivalTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            float newValue;
            if (float.TryParse(TimeOfArrivalTextBox.Text, out newValue))
            {
                if (newValue != _emailId)
                {
                    _emails.Remove(_emailId);
                    _emails.Add(newValue, _email);
                    _emailId = newValue;
                }
            }
            SetEmails(_emails);
        }

        private void EmailsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmailsListBox.SelectedItem != null)
            {
                _emailId = float.Parse(((TextBlock)EmailsListBox.SelectedItem).Text.Split(' ')[0]);
                _email = _emails[_emailId];
                SubjectTextField.Text = _email.Subject;
                ContentTextField.Text = _email.Content;
                SenderTextField.Text = _email.Sender;
                EmailDatePicker.Value = _email.ReceivedDate;
                TimeOfArrivalTextBox.Text = _emailId.ToString();
                ProgrammingTypeSelect.SelectedItem = _email.ProgrammingType;

            }
        }

        public Action<object?, Dictionary<float, Email>> OnSave { get; internal set; }

        internal void SetEmails(Dictionary<float, Email> emails)
        {
            _email = null;
            EmailsListBox.SelectedItem = null;
            EmailsListBox.Items.Clear();
            _emails = emails;
            foreach (var email in emails.OrderBy(x => x.Key))
            {
                var textBlock = new TextBlock();
                textBlock.Text = $"{email.Key} - {email.Value.Subject}";
                textBlock.Margin = new Thickness(5);
                EmailsListBox.Items.Add(textBlock);
                
            }

        }

        private void SaveEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (_emailId != float.Parse(TimeOfArrivalTextBox.Text))
            {
                _emails.Add(float.Parse(TimeOfArrivalTextBox.Text), new Email(SenderTextField.Text, SubjectTextField.Text, ContentTextField.Text));
            }
            else
            {
                _emails[_emailId].Subject = SubjectTextField.Text;
                _emails[_emailId].Content = ContentTextField.Text;
                _emails[_emailId].Sender = SenderTextField.Text;
                _emails[_emailId].ReceivedDate = (DateTime)EmailDatePicker.Value;
                _emails[_emailId].ProgrammingType = (ProgrammingType)ProgrammingTypeSelect.SelectedItem;
            }
            SetEmails(_emails);
        }

        private void DeleteEmailButton_Click(object sender, RoutedEventArgs e)
        {
            _emails.Remove(_emailId);
            SetEmails(_emails);
        }

        private void SaveEmailsButton_Click(object sender, RoutedEventArgs e)
        {
            OnSave?.Invoke(this, _emails);
            Close();
        }

        private void AddNewEmailButton_Click(object sender, RoutedEventArgs e)
        {
            var max = _emails.Any() ? _emails.Keys.Max() : 0;
            _emails.Add(max + 1, new Email());
            SetEmails(_emails);
        }

        private void DuplicateEmailButton_Click(object sender, RoutedEventArgs e)
        {
            var max = _emails.Any() ? _emails.Keys.Max() : 0;
            _emails.Add(max + 1, new Email(_email));
            SetEmails(_emails);
        }
    }
}
