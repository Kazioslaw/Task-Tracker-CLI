using System.Text;
using System.Text.Json;

namespace TaskTracker
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			//args = new string[] { "update", "0", "Error" };
			string fileName = "TaskTracker.json";
			int taskID = 0;
			string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
			List<Task> taskList;
			if (File.Exists(filePath))
			{
				using (StreamReader sr = new StreamReader(filePath))
				{
					string fileContent = sr.ReadToEnd();
					taskList = JsonSerializer.Deserialize<List<Task>>(fileContent) ?? new List<Task>();
				}
			}
			else
			{
				taskList = new List<Task>();
			}
			var header = $"{"ID",-5}{"Description",-30}{"Status",-10}{"Created",-22}{"Updated",-22}";
			if (args.Length > 0)
			{
				if (args[0].ToLower() == "add")
				{
					Task task = new Task()
					{
						ID = taskList.Count > 0 ? taskList.Max(t => t.ID) + 1 : 1,
						Description = args[1] ?? "Nie podano opisu zadania"
					};

					taskList.Add(task);
					Console.WriteLine($"Task added successfully (ID: {task.ID})");
				}

				else if (args[0].ToLower() == "update")
				{
					if (Int32.TryParse(args[1], out taskID))
					{
						var taskIndex = GetTaskIndex(taskList, taskID);
						if (taskIndex > -1)
						{
							taskList[taskIndex].Description = args[2];
							taskList[taskIndex].UpdatedAt = DateTime.Now;
						}

						Console.WriteLine($"Task updated successfully");
					}
				}

				else if (args[0].ToLower() == "mark-in-progress")
				{
					if (Int32.TryParse(args[1], out taskID))
					{
						var taskIndex = GetTaskIndex(taskList, taskID);
						if (taskIndex > -1)
						{
							taskList[taskIndex].Status = Status.InProgress;
							taskList[taskIndex].UpdatedAt = DateTime.Now;
						}
					}
				}

				else if (args[0].ToLower() == "mark-done")
				{
					if (Int32.TryParse(args[1], out taskID))
					{
						var taskIndex = GetTaskIndex(taskList, taskID);
						if (taskIndex > -1)
						{
							taskList[taskIndex].Status = Status.Complete;
							taskList[taskIndex].UpdatedAt = DateTime.Now;
						}
					}
				}

				else if (args[0].ToLower() == "delete")
				{
					if (Int32.TryParse(args[1], out taskID))
					{
						var taskIndex = GetTaskIndex(taskList, taskID);
						if (taskIndex > -1)
						{
							taskList.RemoveAt(taskIndex);
							Console.WriteLine("Task successfully removed");
						}
					}
				}

				else if (args[0].ToLower() == "list")
				{
					if (args.Length < 2)
					{
						Console.WriteLine(header);
						Console.WriteLine(new string('-', header.Length));
						foreach (var task in taskList)
						{
							string status = task.Status switch
							{
								Status.ToDo => "\U0001F4DD",
								Status.InProgress => "\U0001F504",
								Status.Complete => "\U00002705",
								_ => "\U00002753"
							};
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{status,-10}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-22}{task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}");
						}
					}
					else if (args[1].ToLower() == "done")
					{
						var doneTaskList = (from task in taskList
											where task.Status == Status.Complete
											select task).ToList();
						Console.WriteLine(header);
						Console.WriteLine(new string('-', header.Length));
						foreach (var task in doneTaskList)
						{
							string status = task.Status switch
							{
								Status.ToDo => "\U0001F4DD",
								Status.InProgress => "\U0001F504",
								Status.Complete => "\U00002705",
								_ => "\U00002753"
							};
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{status,-10}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-22}{task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}");
						}
					}
					else if (args[1].ToLower() == "todo")
					{
						var todoTaskList = (from task in taskList
											where task.Status == Status.ToDo
											select task).ToList();
						Console.WriteLine(header);
						Console.WriteLine(new string('-', header.Length));
						foreach (var task in todoTaskList)
						{
							string status = task.Status switch
							{
								Status.ToDo => "\U0001F4DD",
								Status.InProgress => "\U0001F504",
								Status.Complete => "\U00002705",
								_ => "\U00002753"
							};
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{status,-10}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-22}{task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}");
						}
					}
					else if (args[1].ToLower() == "in-progress" || args[1].ToLower() == "inprogress")
					{
						var inProgressTaskList = (from task in taskList
												  where task.Status == Status.InProgress
												  select task).ToList();
						Console.WriteLine(header);
						Console.WriteLine(new string('-', header.Length));
						foreach (var task in inProgressTaskList)
						{
							string status = task.Status switch
							{
								Status.ToDo => "\U0001F4DD",
								Status.InProgress => "\U0001F504",
								Status.Complete => "\U00002705",
								_ => "\U00002753"
							};
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{status,-10}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-22}{task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}");
						}
					}
				}
			}
			var taskListSerialized = JsonSerializer.Serialize(taskList);
			using (StreamWriter sw = new StreamWriter(filePath, false))
			{
				sw.WriteLine(taskListSerialized);
			}
		}

		public static int GetTaskIndex(List<Task> tasks, int taskId)
		{
			return tasks.FindIndex(t => t.ID == taskId);
		}
	}
}
