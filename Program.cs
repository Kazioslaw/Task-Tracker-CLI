using System.Text.Json;

namespace TaskTracker
{
	internal class Program
	{
		static void Main(string[] args)
		{
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
			var header = $"{"ID",-5} {"Description",-30} {"Status",-15} {"Created",-20} {"Updated",-20}";
			if (args.Length > 0)
			{
				if (args[0].ToLower() == "add")
				{

					taskList.Add(new Task
					{
						ID = taskList.Count > 0 ? taskList.Max(t => t.ID) + 1 : 1,
						Description = args[1] ?? "Nie podano opisu zadania"
					});
				}

				else if (args[0].ToLower() == "update")
				{
					if (Int32.TryParse(args[1], out taskID))
					{
						var taskIndex = GetTaskIndex(taskList, taskID);
						if (taskIndex > -1)
						{
							taskList[taskIndex].Description = args[2];
						}
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
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{task.Status.ToString(),-15}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-20}{(task.UpdatedAt != null ? $",Updated: {task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}" : string.Empty)}");
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
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{task.Status.ToString(),-15}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-20}{(task.UpdatedAt != null ? $",Updated: {task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}" : string.Empty)}");
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
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{task.Status.ToString(),-15}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-20}{(task.UpdatedAt != null ? $",Updated: {task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}" : string.Empty)}");
						}
					}
					else if (args[1].ToLower() == "in-progress")
					{
						var inProgressTaskList = (from task in taskList
												  where task.Status == Status.InProgress
												  select task).ToList();
						Console.WriteLine(header);
						Console.WriteLine(new string('-', header.Length));
						foreach (var task in inProgressTaskList)
						{
							Console.WriteLine($"{task.ID,-5}{task.Description,-30}{task.Status.ToString(),-15}{task.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),-20}{(task.UpdatedAt != null ? $",Updated: {task.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss"),-20}" : string.Empty)}");
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
