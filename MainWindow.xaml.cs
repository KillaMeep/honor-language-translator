using System;
using System.Linq;
using System.Windows;

namespace WpfTranslator
{
    public partial class MainWindow : Window
    {
        private readonly string hon = " !;\"#÷$%&>*()?,[]+=@/:x'-_";
        private readonly string eng = " abcdefghijklmnopqrstuvwxyz";

        private readonly HashSet<char> engChars;
        private readonly HashSet<char> honChars;

        public MainWindow()
        {
            InitializeComponent();

            engChars = new HashSet<char>(eng);
            honChars = new HashSet<char>(hon);
        }

        private string TranslateTo(string translate)
        {
            return new string(translate
                .Where(chars => engChars.Contains(chars))
                .Select(chars => hon[eng.IndexOf(chars)])
                .ToArray());
        }

        private string TranslateFrom(string translate)
        {
            return new string(translate
                .Where(chars => honChars.Contains(chars))
                .Select(chars => eng[hon.IndexOf(chars)])
                .ToArray());
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text?.ToLower();

            if (!string.IsNullOrEmpty(input))
            {
                string result = AutoTranslate(input);
                ResultTextBox.Text = result;
            }
            else
            {
                ResultTextBox.Text = "Invalid input.";
            }
        }

        private string AutoTranslate(string inputString)
        {
            foreach (char c in inputString.Distinct())
            {
                if (engChars.Contains(c))
                {
                    TranslationLabel.Content = "Translated to Honor Language:";
                    return TranslateTo(inputString);
                }
                else if (honChars.Contains(c))
                {
                    TranslationLabel.Content = "Translated from Honor Language:";
                    return TranslateFrom(inputString);
                }
            }
            return string.Empty;
        }
    }
}
