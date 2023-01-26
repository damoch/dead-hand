using DeadHand.Commands.Implementations;
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
                TimeOfArrivalTextBox.Text = _emailId.ToString();

            }
        }

        public Action<object?, Dictionary<float, Email>> OnSave { get; internal set; }

        internal void SetEmails(Dictionary<float, Email> emails)
        {
            _email = null;
            EmailsListBox.SelectedItem = null;
            EmailsListBox.Items.Clear();
            _emails = emails;
            foreach (var email in emails)
            {
                var textBlock = new TextBlock();
                textBlock.Text = $"{email.Key} - {email.Value.Subject}";
                textBlock.Margin = new Thickness(5);
                EmailsListBox.Items.Add(textBlock);
                
            }

        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_email != null)
            {
                _emails[_emailId].Subject = SubjectTextField.Text;
                _emails[_emailId].Content = ContentTextField.Text;
            }
            var textBlock = (TextBlock)sender;
            _emailId = float.Parse(textBlock.Text.Split(' ')[0]);
            _email = _emails[_emailId];
            SubjectTextField.Text = _email.Subject;
            ContentTextField.Text = _email.Content;
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
            _emails.Add(_emails.Keys.Max()+1, new Email());
            SetEmails(_emails);
        }
    }
}
