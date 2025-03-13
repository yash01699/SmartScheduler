# SmartScheduler

Smart Scheduler – AI-Powered Task Planner

![image](https://github.com/user-attachments/assets/b4ccd0c5-5dea-430a-aba5-78ddc5937c67)


📌 Overview
Smart Scheduler is an AI-powered scheduling application that helps users efficiently plan their daily tasks. Users can add tasks, specify the duration and priority, and the system will automatically generate an optimized schedule using Mistral AI.

✅ Key Features:
✔️ Task Management: Add, edit, and delete tasks dynamically.
✔️ AI Scheduling: Uses Mistral API to optimize task scheduling.
✔️ User Preferences (Upcoming): Set start & end time for working hours.
✔️ Google Calendar Integration (Upcoming): Auto-sync schedules with Google Calendar.
✔️ Modern UI: Built with React.js + Tailwind CSS for a seamless experience.
✔️ Robust Backend: Uses .NET Core & C# with a MySQL database.
✔️ RESTful API: Backend exposes secure REST APIs for CRUD operations.

🛠 Tech Stack
Layer	Technology
Frontend	React.js, Tailwind CSS
Backend	.NET Core, C#
Database	MySQL
AI Integration	Mistral API
API Communication	REST API, Axios
State Management	React Hooks (useState, useEffect)
🚀 Installation & Setup
🔹 Prerequisites
Ensure you have the following installed:

Node.js & npm (for frontend)
.NET SDK (for backend)
MySQL (for database)


🔹 Backend Setup (.NET Core & C#)

1️⃣ Clone the Repository

git clone https://github.com/yash01699/SmartScheduler.git
cd SmartScheduler/SmartScheduler

2️⃣ Configure MySQL Database
Create a MySQL database named YOUR-SCHEMA-NAME and update appsettings.json:


"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YOUR-SCHEMA-NAME;User=root;Password=yourpassword;"
}

NOTE: This is JUST the schema/database, and NOT the table that holds the data


3️⃣ Run Migrations & Start Backend

dotnet ef database update
dotnet run

Backend should now be running at https://localhost:7194/api/todo.

🔹 Frontend Setup (React.js)

1️⃣ Navigate to the Frontend Folder

cd ../smart-scheduler-React-UI

2️⃣ Create a react-app
npx create-react-app app_name

INSTALL NPM 

npm install

3️⃣ Run the React App after updating App.js with the App.js in this repo

npm start
Frontend will be available at http://localhost:3000.

🛠 API Endpoints
📌 Task Management APIs

Method	Endpoint	Description
GET	/api/todo	Get all tasks
GET	/api/todo/getById?id={id}	Get a single task with id
POST	/api/todo	Add a new task
PUT	/api/todo	Update an existing task
DELETE	/api/todo/{id}	Delete a task

📌 AI Scheduling API (Mistral Integration)

GET	/api/todo/generateschedule	Generate AI-powered schedule
NOTE: For Mistral Integration, you will have to create a Mistral API key

✅ AI Scheduling Workflow:

1️⃣ User adds tasks with duration & priority
2️⃣ App calls Mistral API to optimize task order
3️⃣ AI generates schedule based on constraints
4️⃣ Schedule is displayed in a user-friendly format

📌 Key Functionalities

🔹 Task Management
Add tasks with name, duration (1-8 hours), and priority (1-5)
Edit tasks dynamically
Mark tasks as complete
Delete tasks

🔹 AI-Powered Scheduling
Calls Mistral API to intelligently reorder tasks
Ensures high-priority tasks are scheduled first
Respects user-defined working hours

🔹 Google Calendar Integration (Upcoming Feature)
Auto-sync schedules to Google Calendar using OAuth
Users receive reminders for upcoming tasks



🛠 Contribution Guidelines
Want to contribute? 🚀 Feel free to fork this repository, create a new branch, and submit a pull request!

📌 Future Enhancements
✅ Google Calendar Sync – Auto-add schedules to Google Calendar
✅ AI Task Suggestions – Mistral AI suggests optimized tasks
✅ Having a specific start time for a task (NOTE: No end time specification since duration is already taken into consideration)
✅ User Profiles – Save multiple schedules per user

📌 Contact & Support
For issues or feature requests, open an issue or contact me:
📧 Email: yash01699@gmail.com
📌 GitHub: yash01699

⭐ If you found this project useful, give it a star! ⭐
🔹 Made with ❤️ by Yashasvi Nancherla
