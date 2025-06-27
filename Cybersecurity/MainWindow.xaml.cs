using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Cybersecurity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Variables for user input and responses
        private List<string> activityLog = new List<string>();

        // Dictionary to hold topic responses
        string userName = "";
        string keywords = "";
        Random random = new Random();

        Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>
        {
            {"phishing tips", new List<string>
                {
                    "Be cautious of emails asking for urgent action.",
                    "Never click on suspicious links.",
                    "Verify email senders before responding."
                }
            }
        };
        private bool awaitingName;
        public MainWindow()
        {
            InitializeComponent();
            PlayVoiceGreeting();
            ShowAsciiArt();

            awaitingName = true;
            BotReply("Hello! I'm your Cybersecurity Awareness Chatbot. What's your name?"); // Prompt for user's name
        }
        private void PlayVoiceGreeting() // This method plays a voice greeting when the application starts
        {
            string audioFilePath = "C:\\Users\\27817\\source\\repos\\ProgrammingAssignmentOne\\ProgrammingAssignmentOne\\bin\\Debug\\Project name (en) v2.wav";
            using (SoundPlayer player = new SoundPlayer(audioFilePath))
            {
                try
                {
                    player.Load();
                    player.Play();
                }
                catch (Exception ex)
                {
                    AppendChat($"[Audio] Error playing voice greeting: {ex.Message}");
                }
            }
        }

        private void ShowAsciiArt() // This method displays ASCII art in the chat history
        {
            AppendChat(@"   ____      _                                        _ _                  
 / ___|   _| |__   ___ _ __ ___  ___  ___ _   _ _ __(_) |_ _   _          
| |  | | | | '_ \ / _ \ '__/ __|/ _ \/ __| | | | '__| | __| | | |         
| |__| |_| | |_) |  __/ |  \__ \  __/ (__| |_| | |  | | |_| |_| |         
 \____\__, |_.__/ \___|_|  |___/\___|\___|\__,_|_|  |_|\__|\__, |     _ _ 
   / \|___/   ____ _ _ __ ___ _ __   ___  ___ ___  | __ )  |___/ |_  | | |
  / _ \ \ /\ / / _ | '__/ _ \ '_ \ / _ \/ __/ __| |  _ \ / _ \| __| | | |
 / ___ \ V  V / (_| | | |  __/ | | |  __/\__ \__ \ | |_) | (_) | |_  |_|_|
/_/   \_\_/\_/ \__,_|_|  \___|_| |_|\___||___/___/ |____/ \___/ \__| (_|_)");
        }

        private void ShowMainMenu() // This method displays the main menu options in the chat history
        {
            AppendChat("You can choose from the following options:\n- Friendly chat\n- Password safety\n- Phishing\n- Safe browsing\n- Recommend books\nType 'exit' to leave the chatbot.");
        }

        private void AppendChat(string message) // This method appends a message to the chat history
        {
            ChatHistory.Items.Add(message);
        }





        private void Send_Click(object sender, RoutedEventArgs e) // This method is triggered when the user clicks the send button
        {
            string userMessage = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userMessage)) return;

            if (awaitingName)
            {
                userName = userMessage;
                AppendChat($"Welcome, {userName}! I'm your Cybersecurity Awareness Bot.");
                awaitingName = false;
                ShowMainMenu();
                UserInput.Clear();
                return;
            }

            ChatHistory.Items.Add("You: " + userMessage);
            ProcessInput(userMessage.ToLower());
            UserInput.Clear();
        }

        private void HandleGeneralCommands(string input) // This method processes general commands and provides appropriate responses
        {
            switch (input)
            {
                case "friendly chat":
                    AppendChat("I'm here for a friendly chat! Ask me things like 'how are you', 'what's your purpose', 'what can I ask you about', or 'recommend books'.");
                    break;
                case "how are you":
                    AppendChat("I'm just a bot, but I'm running smoothly! 😄");
                    break;
                case "what's your purpose":
                case "what is your purpose":
                    AppendChat("I'm here to help you stay safe online by spreading cybersecurity awareness.");
                    break;
                case "what can i ask you about":
                    AppendChat("You can ask about password safety, phishing, or safe browsing. Just type the topic.");
                    break;
                case "recommend books":
                    AppendChat("Here are a few great books for learning more about cybersecurity:\n- 'Cybersecurity for Beginners' by Raef Meeuwisse\n- 'The Art of Invisibility' by Kevin Mitnick\n- 'Cybersecurity and Cyberwar' by P.W. Singer and Allan Friedman");
                    break;
                case "password safety":
                    AppendChat("Use strong, unique passwords for each account and consider using a password manager. Never use personal information like your name, birthday, or email. Include numbers, symbols, uppercase and lowercase letters.");
                    break;
                case "phishing":
                    AppendChat("Phishing is when cybercriminals trick you into revealing sensitive data via emails, messages, or fake websites. Never click suspicious links.");
                    break;
                case "safe browsing":
                    AppendChat("Always use HTTPS websites, avoid unknown downloads, and keep your browser updated.");
                    break;
                case "exit":
                    Application.Current.Shutdown();
                    break;
                default:
                    AppendChat("I didn't quite understand that. Could you please rephrase?");
                    break;
            }
        }

        private void ProcessInput(string input) //This method processes the user's input and provides appropriate responses
        {
            if (input.Contains("worried") || input.Contains("frustrated") || input.Contains("angry")) //Sentiment Dectection
            {
                BotReply("I understand your concern. It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe.");
            }
            else if (input.Contains("curious"))
            {
                BotReply("Curiosity is a great start! Ask me anything about cybersecurity.");
            }
            else if (input.Contains("quiz"))
            {
                BotReply("Starting the cybersecurity quiz!");
                new QuizWindow().ShowDialog();
                activityLog.Add($"[{DateTime.Now}] Quiz started");
            }
            else if (Regex.IsMatch(input, @"(add task|set reminder|remind me|task)", RegexOptions.IgnoreCase))
            {
                BotReply("Opening task assistant for cybersecurity tasks...");
                new TaskWindow(this).ShowDialog();
            }
            else if (input.Contains("activity log") || input.Contains("what have you done"))
            {
                ShowActivityLog();
            }
            else if (input.Contains("password") || input.Contains("phishing") || input.Contains("privacy")) //NLP Dectection
            {
                ProvideCyberTip(input);
            }
            else if (input.Contains("friendly chat"))
            {
                BotReply("I'm here for a friendly chat! Ask me things like 'how are you', 'what's your purpose', 'what can I ask you about', or 'recommend books'.");
            }
            else if (input.Contains("how are you"))
            {
                BotReply("I'm just a bot, but I'm running smoothly! 😄");
            }
            else if (input.Contains("what's your purpose") || input.Contains("what is your purpose"))
            {
                BotReply("I'm here to help you stay safe online by spreading cybersecurity awareness.");
            }
            else if (input.Contains("what can i ask you about"))
            {
                BotReply("You can ask about password safety, phishing, or safe browsing. Just type the topic.");
            }
            else if (input.Contains("recommend books"))
            {
                BotReply("Here are a few great books for learning more about cybersecurity:\n- 'Cybersecurity for Beginners' by Raef Meeuwisse\n- 'The Art of Invisibility' by Kevin Mitnick\n- 'Cybersecurity and Cyberwar' by P.W. Singer and Allan Friedman");
            }
            else if (input.Contains("exit"))
            {
                Application.Current.Shutdown();
            }
            else
            {
                BotReply("I'm not sure I understand. Can you try rephrasing?");
            }
        }


        public void TaskAddedFromAssistant(string taskTitle) // This method is called when a task is added from the task assistant
        {
            BotReply($"Task '{taskTitle}' successfully added to your cybersecurity task list.");
            activityLog.Add($"[{DateTime.Now}] Task added: {taskTitle}");
        }

        private void ProvideCyberTip(string input) // This method provides cybersecurity tips based on the user's input
        {
            if (input.Contains("password"))
                BotReply("Always use strong, unique passwords and enable two-factor authentication.");
            else if (input.Contains("phishing"))
                BotReply("Beware of suspicious emails and never click unknown links.");
            else if (input.Contains("privacy"))
                BotReply("Regularly review your privacy settings on all platforms.");

            activityLog.Add($"[{DateTime.Now}] Cyber tip provided for: {input}");
        }

        private void ShowActivityLog() // This method displays the last 10 actions taken by the bot
        {
            BotReply("Here's a summary of recent actions:");
            int start = activityLog.Count > 10 ? activityLog.Count - 10 : 0;
            for (int i = start; i < activityLog.Count; i++)
            {
                BotReply(activityLog[i]);
            }
        }

        private void BotReply(string message) // This method adds a bot reply to the chat history
        {
            ChatHistory.Items.Add("Bot: " + message);
        }
    }
}


/*
CodeAcademy.2024. Code Foundations.
[Online]. Available at: https://www.bing.com/search?pglt=41&q=coding+websites&cvid=84612eb9ae0c47afb9c29a59ddbed599&gs_lcrp=EgZjaHJvbWUqBggAEAAYQDIGCAAQABhAMgYIARAAGEAyBggCEAAYQDIGCAMQABhAMgYIBBAAGEAyBggFEAAYQDIGCAYQABhAMgYIBxAAGEAyBggIEAAYQNIBCDUxNjFqMGoxqAIIsAIB&FORM=ANNTA1&PC=HCTS
[Accessed 3 September 2024]
*/

/*
Caspio.2024. Best No Problem 
[Online]. Available at: https://www.bing.com/search?pglt=41&q=coding+websites&cvid=84612eb9ae0c47afb9c29a59ddbed599&gs_lcrp=EgZjaHJvbWUqBggAEAAYQDIGCAAQABhAMgYIARAAGEAyBggCEAAYQDIGCAMQABhAMgYIBBAAGEAyBggFEAAYQDIGCAYQABhAMgYIBxAAGEAyBggIEAAYQNIBCDUxNjFqMGoxqAIIsAIB&FORM=ANNTA1&PC=HCTS
[Accessed 3 September 2024]
*/

/*
FreeCodeCamp.2024. Learn how to code
[Online]. Available at: https://www.freecodecamp.org/ 
[Accessed 3 September 2024]
*/

/*
Programiz.2024. Learn programming
[Online]. Available at: https://www.programiz.com/
[Accessed 3 September 2024]
*/

/*
Udemy.2024. Coding for beginners
[Online]. Available at: https://www.udemy.com/course/coding-for-beginners/?utm_source=bing&utm_medium=udemyads&utm_campaign=BG-Search_DSA_Beta_Prof_la.EN_cc.ROW-English&campaigntype=Search&portfolio=Bing&language=EN&product=Course&test=&audience=DSA&topic=&priority=Beta&utm_content=deal4584&utm_term=_._ag_1324913899918011_._ad__._kw_Dev%20en_._de_c_._dm__._pl__._ti_dat-2334606783827006:loc-168_._li_136827_._pd__._&matchtype=b&msclkid=fb3eb42fffef17c655a2d2f931d6aeeb
[Accessed 3 September 2024]
*/




    
        
   

