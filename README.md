# Programming-Cybersecurity
--MyWpfApp

A WPF desktop application demonstrating how to apply a background image to a `Grid` using embedded resources. Designed for educational and UI design practice.
-- Purpose
The purpose of this project is to provide a practical demonstration of how to apply visual styling in a WPF application—specifically by setting an image background for a Grid using XAML and embedded resources.The purpose of this cybersecurity chatbot is to provide an engaging and educational platform that raises awareness about digital security practices in a rapidly evolving digital landscape. It is designed to serve as a friendly and accessible companion that empowers users to better understand the threats they face online from phishing scams and password vulnerabilities to malware and social engineering tactics.Through features like interactive quiz games, personalized task reminders, and conversational simulations, the chatbot transforms cybersecurity from a complex, intimidating topic into an intuitive, gamified learning journey. Users not only test their knowledge but also receive real-time feedback, making the learning process both informative and enjoyable. Behind the scenes, it also supports core natural language processing techniques to detect key terms, respond to varied user inputs, and simulate helpful digital assistant behavior.This chatbot aims to be more than just a teaching tool—it aspires to cultivate everyday cybersecurity habits by nudging users toward safer choices and helping them track their progress through activity logs and reminders. It is especially valuable in contexts like schools, digital literacy initiatives, or community outreach programs—particularly in regions like South Africa, where increasing online access must go hand in hand with increased cyber-awareness. By combining education, accessibility, and interaction, the chatbot becomes a proactive guardian against cyber risks and a catalyst for a culture of digital resilience.

-- Technologies Used
- **WPF (Windows Presentation Foundation)** – For building the desktop UI.
- **XAML** – To define UI structure and style declaratively.
- **.NET Framework / .NET Core** – The runtime environment for executing the application.
- **Visual Studio** – IDE used for development and UI design.
- **Resource Management** – Embedding and referencing images as resources.
- **C#** – For handling logic and dynamic behavior (if needed).

--Features
- Background image on `Grid` using `ImageBrush`
- Clean layout with flexible image scaling using `Stretch`
- Lightweight and responsive design
- Easy to extend with additional UI components and animations


--Prerequisites
- Visual Studio with WPF support installed
- .NET Framework or .NET Core SDK (depending on project setup)

--Setup Instructions
1. Clone repository.
2. Open the solution in Visual Studio.
3. Add an image to the `Images/` folder in your project.
4. Right-click the image in Solution Explorer → **Properties** → set **Build Action** to `Resource`.
5. In your `XAML` file, add the following to your Grid:

   --xml
   <Grid.Background>
       <ImageBrush ImageSource="Images/background.jpg" Stretch="UniformToFill"/>
   </Grid.Background>
