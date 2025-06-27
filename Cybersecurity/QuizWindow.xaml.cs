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

namespace Cybersecurity
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
     /// <summary>
        /// Interaction logic for QuizWindow.xaml
        /// </summary>
        public partial class QuizWindow : Window
        {
            private List<QuizQuestion> questions = new List<QuizQuestion>(); // List to hold quiz questions
            private int currentIndex = 0; // Current question index
            private int score = 0; // Score counter
            public QuizWindow()
            {
                InitializeComponent();
                LoadQuestions();
            }
            private void StartQuiz_Click(object sender, RoutedEventArgs e) // Event handler for Start button
            {
                MessageBox.Show("Start button clicked"); // Confirm event triggers
                StartScreen.Visibility = Visibility.Collapsed;
                QuizPanel.Visibility = Visibility.Visible;

                DisplayQuestion();
            }

            private void LoadQuestions() // Method to load quiz questions
            {
                questions.Add(new QuizQuestion("What is phishing?", new[] { "A type of malware", "Tricking users to reveal sensitive info", "Password cracking method", "Safe browsing technique" }, 1));
                questions.Add(new QuizQuestion("True or False: You should reuse the same password everywhere.", new[] { "True", "False" }, 1));
                questions.Add(new QuizQuestion("Which is a good password practice?", new[] { "Use personal info", "Keep short passwords", "Use complex unique passwords", "Share passwords with friends" }, 2));
                questions.Add(new QuizQuestion("What should you do if you receive a suspicious email?", new[] { "Ignore it", "Click on links", "Report it", "Reply with your info" }, 2));
                questions.Add(new QuizQuestion("What is two-factor authentication?", new[] { "A method to bypass security", "Using two passwords", "An extra layer of security", "A type of phishing" }, 2));
                questions.Add(new QuizQuestion("True or False: Public Wi-Fi is always secure.", new[] { "True", "False" }, 1));
                questions.Add(new QuizQuestion("What should you do with software updates?", new[] { "Ignore them", "Install them promptly", "Delay them indefinitely", "Only update when convenient" }, 1));
                questions.Add(new QuizQuestion("What is a strong password?", new[] { "123456", "password", "A mix of letters, numbers, and symbols", "Your name" }, 2));
                questions.Add(new QuizQuestion("What is the purpose of a firewall?", new[] { "To speed up your computer", "To block unauthorized access", "To clean viruses", "To manage passwords" }, 1));
                questions.Add(new QuizQuestion("True or False: You should always log out of accounts on shared devices.", new[] { "True", "False" }, 0));
                questions.Add(new QuizQuestion("What is social engineering?", new[] { "A type of software", "Manipulating people to gain information", "A security protocol", "A programming language" }, 1));
                questions.Add(new QuizQuestion("What should you do if you suspect your account has been compromised?", new[] { "Change your password immediately", "Ignore it", "Wait for a few days", "Share your password with friends" }, 0));
                questions.Add(new QuizQuestion("What is the best way to protect your personal information online?", new[] { "Share it freely", "Use strong privacy settings", "Ignore privacy settings", "Only use public Wi-Fi" }, 1));
                questions.Add(new QuizQuestion("What is a VPN?", new[] { "Virtual Private Network", "Virus Protection Network", "Very Private Network", "Virtual Public Network" }, 0));
                questions.Add(new QuizQuestion("True or False: You should click on links in emails from unknown senders.", new[] { "True", "False" }, 1));
            }

            private void DisplayQuestion() // Method to display the current question
            {
                if (currentIndex >= questions.Count)
                {
                    ShowFinalScore();
                    return;
                }

                var q = questions[currentIndex];
                QuestionText.Text = $"Question {currentIndex + 1}: {q.Question}";

                // Set options text and visibility
                Option1.Content = q.Options.Length > 0 ? q.Options[0] : "";
                Option1.Visibility = q.Options.Length > 0 ? Visibility.Visible : Visibility.Collapsed;

                Option2.Content = q.Options.Length > 1 ? q.Options[1] : "";
                Option2.Visibility = q.Options.Length > 1 ? Visibility.Visible : Visibility.Collapsed;

                Option3.Content = q.Options.Length > 2 ? q.Options[2] : "";
                Option3.Visibility = q.Options.Length > 2 ? Visibility.Visible : Visibility.Collapsed;

                Option4.Content = q.Options.Length > 3 ? q.Options[3] : "";
                Option4.Visibility = q.Options.Length > 3 ? Visibility.Visible : Visibility.Collapsed;

                Option1.IsChecked = false;
                Option2.IsChecked = false;
                Option3.IsChecked = false;
                Option4.IsChecked = false;

                FeedbackText.Text = "";
            }



            private void SubmitAnswer_Click(object sender, RoutedEventArgs e) // Event handler for Submit button
            {
                int selectedIndex = -1;
                if (Option1.IsChecked == true) selectedIndex = 0;
                if (Option2.IsChecked == true) selectedIndex = 1;
                if (Option3.IsChecked == true) selectedIndex = 2;
                if (Option4.IsChecked == true) selectedIndex = 3;

                if (selectedIndex == -1)
                {
                    MessageBox.Show("Please select an answer.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var correctAnswer = questions[currentIndex].Options[questions[currentIndex].CorrectOption];

                if (selectedIndex == questions[currentIndex].CorrectOption)
                {
                    score++;
                    FeedbackText.Text = "Correct! Great job, you're a cybersecurity pro!";
                }
                else
                {
                    FeedbackText.Text = $"Sorry, incorrect. Keep trying! The correct answer was: {correctAnswer}\n{ProvideExplanation(currentIndex)}";
                }

                currentIndex++;
                Dispatcher.InvokeAsync(async () =>
                {
                    await System.Threading.Tasks.Task.Delay(3000);  // Longer delay for feedback
                    DisplayQuestion();
                });
            }


            private string ProvideExplanation(int index) // Method to provide explanations for answers
            {
                switch (index)
                {
                    case 0: return "Phishing is when attackers trick users into revealing sensitive information.";
                    case 1: return "False. You should never reuse passwords.";
                    case 2: return "Using complex and unique passwords is essential for security.";
                    case 3: return "Suspicious emails should be reported, not clicked or replied to.";
                    case 4: return "Two-factor authentication adds an extra layer of security.";
                    case 5: return "False. Public Wi-Fi is often insecure.";
                    case 6: return "Always install software updates promptly to stay protected.";
                    case 7: return "Strong passwords mix letters, numbers, and symbols.";
                    case 8: return "Firewalls block unauthorized access to your device or network.";
                    case 9: return "Always log out of accounts on shared devices to protect your information.";
                    case 10: return "Social engineering involves manipulating people to gain information.";
                    case 11: return "If your account is compromised, change your password immediately.";
                    case 12: return "Strong privacy settings help protect your personal information online.";
                    case 13: return "A VPN (Virtual Private Network) secures your internet connection.";
                    case 14: return "Never click on links from unknown senders—they could be malicious.";
                    default: return "Stay vigilant online!";
                }
            }


            private void ShowFinalScore() // Method to show the final score after all questions are answered
            {
                MessageBox.Show($"Quiz completed! Your score: {score}/{questions.Count}", "Results", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }

            private void Close_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }

        public class QuizQuestion // Represents a quiz question
        {
            public string Question { get; set; }
            public string[] Options { get; set; }
            public int CorrectOption { get; set; }

            public QuizQuestion(string question, string[] options, int correctOption)
            {
                Question = question;
                Options = options;
                CorrectOption = correctOption;
            }
        }
    }


