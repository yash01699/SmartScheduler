import React, { useState, useEffect } from "react";
import axios from "axios"; 
import "./App.css";

function App() {
  const [data, setData] = useState([]);
  const [editedTasks, setEditedTasks] = useState({});
  const [response, setResponse] = useState(null);
  const [loading, setLoading] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");

  // Fetch tasks from backend
  useEffect(() => {
    axios.get("https://localhost:7194/api/todo") 
      .then((response) => setData(response.data))
      .catch((error) => console.error("Error fetching data:", error));
  }, []);

  // Add new task
  const addTask = () => {
    const newTask = {
      id: data.length + 1, // Temporary ID
      name: "New Task",
      isComplete: false,
      time: 1,
      priority: 1,
    };
    setData([...data, newTask]);

    axios.post("https://localhost:7194/api/todo", newTask)
      .then(() => console.log("Task added successfully"))
      .catch((error) => console.error("Error adding task:", error));
  };

  // Track input changes
  const handleEditChange = (id, field, value) => {
    setEditedTasks((prev) => ({
      ...prev,
      [id]: { ...prev[id], [field]: value },
    }));
  };

  // Check if the task has changed
  const hasChanges = (id) => editedTasks[id] && Object.keys(editedTasks[id]).length > 0;

  // Update Task
  const updateTask = (id) => {
    if (!hasChanges(id)) return;

    const updatedTask = { ...data.find((task) => task.id === id), ...editedTasks[id] };

    axios.put(`https://localhost:7194/api/todo`, updatedTask)
      .then(() => {
        setData((prevData) =>
          prevData.map((task) => (task.id === id ? updatedTask : task))
        );
        setEditedTasks((prev) => {
          const newEdits = { ...prev };
          delete newEdits[id];
          return newEdits;
        });
        setAlertMessage("✅ Task updated successfully!");
        setTimeout(() => setAlertMessage(""), 3000);
      })
      .catch((error) => console.error("Error updating task:", error));
  };

  // Delete Task
  const deleteTask = (id) => {
    axios.delete(`https://localhost:7194/api/todo/${id}`)
      .then(() => setData(prevData => prevData.filter(item => item.id !== id)))
      .catch(error => console.error("Error deleting task:", error));
  };

  // Generate Schedule
  const handleGenerateSchedule = async () => {
    try {
      setLoading(true);
      setResponse(null);
      const res = await axios.get("https://localhost:7194/api/todo/generateschedule");
      setResponse(res.data);
    } catch (error) {
      console.error("Error generating schedule:", error);
    } finally {
      setLoading(false);
    }
  };

  // Clear Schedule
  const handleClearSchedule = () => setResponse(null);

  return (
    <div className="App p-8 max-w-4xl mx-auto bg-white shadow-lg rounded-lg">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">📝 Task Scheduler</h1>

      {/* Show Alert Message */}
      {alertMessage && (
        <div className="mb-4 p-2 bg-green-500 text-white text-center rounded-lg">
          {alertMessage}
        </div>
      )}

      {/* Add Task Button */}
      <div className="flex justify-center mb-4">
        <button 
          onClick={addTask} 
          className="p-2 bg-blue-600 hover:bg-blue-700 text-white rounded-lg transition-all"
        >
          ➕ Add New Task
        </button>
      </div>
      <br/>
      {/* Task Table */}
      <div className="overflow-x-auto">
        <table className="w-full border-collapse rounded-lg shadow-md" align="center">
          <thead>
            <tr className="bg-blue-600 text-white">
              <th className="border p-3">Task</th>
              <th className="border p-3">Done?</th>
              <th className="border p-3">Hours</th>
              <th className="border p-3">Priority</th>
              <th className="border p-3">Actions</th>
            </tr>
          </thead>
          <tbody className="text-center">
            {data.map((item) => (
              <tr key={item.id} className="border-b hover:bg-gray-100 transition-all">

                {/* Editable Name */}
                <td className="border p-2">
                  <input
                    type="text"
                    value={editedTasks[item.id]?.name ?? item.name}
                    onChange={(e) => handleEditChange(item.id, "name", e.target.value)}
                    className="border p-1 w-full text-center rounded-md focus:ring-2 focus:ring-blue-500"
                  />
                </td>

                {/* Checkbox for Completion */}
                <td className="border p-2">
                  <input
                    type="checkbox"
                    checked={editedTasks[item.id]?.isComplete ?? item.isComplete}
                    onChange={(e) => handleEditChange(item.id, "isComplete", e.target.checked)}
                  />
                </td>

                {/* Dropdown for Time (1 to 8) */}
                <td className="border p-2">
                  <select
                    value={editedTasks[item.id]?.time ?? item.time}
                    onChange={(e) => handleEditChange(item.id, "time", parseInt(e.target.value))}
                    className="border p-1 text-center rounded-md"
                  >
                    {[...Array(8)].map((_, i) => (
                      <option key={i + 1} value={i + 1}>{i + 1}</option>
                    ))}
                  </select>
                </td>

                {/* Dropdown for Priority (1 to 5) */}
                <td className="border p-2">
                  <select
                    value={editedTasks[item.id]?.priority ?? item.priority}
                    onChange={(e) => handleEditChange(item.id, "priority", parseInt(e.target.value))}
                    className="border p-1 text-center rounded-md"
                  >
                    {[...Array(5)].map((_, i) => (
                      <option key={i + 1} value={i + 1}>{i + 1}</option>
                    ))}
                  </select>
                </td>

                {/* Buttons */}
                <td style={{ display: "flex", justifyContent: "space-between" }}>
                  <button
                    onClick={() => updateTask(item.id)}
                    disabled={!hasChanges(item.id)}
                    //className={`px-3 py-1 rounded-md ${hasChanges(item.id) ? "bg-green-500 hover:bg-green-600 text-white" : "bg-gray-300 text-gray-500 cursor-not-allowed"}`}
                  >
                    ✅ Update
                  </button>
                  
                  <button
                    onClick={() => deleteTask(item.id)}
                    //className="px-3 py-1 bg-red-500 hover:bg-red-600 text-white rounded-md"
                  >
                    ❌ Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <br/>
      {/* Generate & Clear Schedule Buttons */}
      <div className="flex justify-center mt-4 gap-3">
        <button onClick={handleGenerateSchedule} className="p-2 bg-purple-500 hover:bg-purple-600 text-white rounded-lg">
          ⚡ Generate Schedule
        </button>
        
        <button
          onClick={handleClearSchedule}
          disabled={!response}
          className="p-2 bg-red-500 hover:bg-red-600 text-white rounded-lg"
        >
          ❌ Clear
        </button>
      </div>

      {/* Show Loading Spinner */}
      {loading && (
        <div className="mt-4 text-center text-gray-600">
          <p>⏳ Generating schedule, please wait...</p>
        </div>
      )}

      {/* Show API Response */}
      {response && (
        <div className="mt-6 bg-gray-100 p-4 rounded-lg shadow-md">
          <h2 className="text-lg font-semibold">Generated Schedule:</h2>
          <pre className="text-sm">{response}</pre>
        </div>
      )}
    </div>
  );
}

export default App;
