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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private List<TaskItem> tasks = new List<TaskItem>(); // List to hold tasks
        private MainWindow mainWindow; // Reference to the main window

        public TaskWindow(MainWindow caller)
        {
            InitializeComponent();
            RefreshTaskList();
            mainWindow = caller;
        }
        private void AddTask_Click(object sender, RoutedEventArgs e) // Event handler for Add Task button
        {
            string title = TaskTitle.Text.Trim();
            string description = TaskDescription.Text.Trim();
            string reminder = TaskReminder.Text.Trim();

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Task title is required.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsCyberSecurityTask(title))
            {
                MessageBox.Show("Only cybersecurity-related tasks are allowed.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TaskItem newTask = new TaskItem // Create a new task item
            {
                Title = title,
                Description = description,
                Reminder = reminder,
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };
            tasks.Add(newTask);
            RefreshTaskList();

            MessageBox.Show($"Task '{title}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information); // Notify user of success
            mainWindow.TaskAddedFromAssistant(title); // Notify MainWindow

            TaskTitle.Clear();
            TaskDescription.Clear();
            TaskReminder.Clear();
        }

        private void RefreshTaskList() // Method to refresh the task list display
        {
            TaskList.Items.Clear();
            foreach (var task in tasks)
            {
                string display = $"[{(task.IsCompleted ? "✔" : "Pending")}] {task.Title} - {task.Description}";
                if (!string.IsNullOrEmpty(task.Reminder))
                    display += $" (Reminder: {task.Reminder})";

                TaskList.Items.Add(display);
            }
        }
        private void MarkComplete_Click(object sender, RoutedEventArgs e) // Event handler for Mark Complete button
        {
            if (TaskList.SelectedIndex >= 0)
            {
                tasks[TaskList.SelectedIndex].IsCompleted = true;
                RefreshTaskList();
            }
        }
        private void DeleteTask_Click(object sender, RoutedEventArgs e) // Event handler for Delete Task button
        {
            if (TaskList.SelectedIndex >= 0)
            {
                tasks.RemoveAt(TaskList.SelectedIndex);
                RefreshTaskList();
            }
        }
        private bool IsCyberSecurityTask(string title) // Method to check if the task title is related to cybersecurity
        {
            string[] keywords = { "password", "update", "phishing", "privacy", "security", "reminder", "cyber", "authentication" };
            return keywords.Any(k => title.ToLower().Contains(k));
        }

        private void Close_Click(object sender, RoutedEventArgs e) // Event handler for Close button
        {
            this.Close();
        }

        public void TaskAddedFromAssistant(string taskName) // Method to notify the main window when a new task is added
        {
            MessageBox.Show($"New cybersecurity task added: {taskName}", "Task Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public class TaskItem // Represents a task item
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Reminder { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}

    

    

        

   
