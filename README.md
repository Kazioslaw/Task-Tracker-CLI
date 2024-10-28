
# TaskTracker
Project created via idea from [Roadmap.sh](https://roadmap.sh/projects/task-tracker)

## About
Task Tracker CLI is a project used to track and manage your tasks.

## Features

- Adding new tasks
- Updating tasks
- Deleting tasks
- Marking tasks as in progress or done
- Viewing task list
- Viewing task list based on status (done, todo, in progress)

## Technology used

- C# 12
- .NET 8

## Instalation
1. Clone the repository
2. Open the project in prefered IDE
3. Build the project

## Usage
For use the app you need open the folder with `tasktracker.exe` file (i.e. `bin\Debug\net8.0` folder) via prefered terminnal (e.g Git Bash or Powershell)

```bash
Usage: task-cli [command]
The options below may be used to perform the desired operations:
    add <description>               - Add a new task
    update <taskId> <description>   - Update a task
    delete <taskId>                 - Delete a task
    mark-in-progress <taskId>       - Mark a task as in progress
    mark-done <taskId>              - Mark a task as done
    list                            - List all tasks
    list done                       - List all done tasks
    list todo                       - List all todo tasks
    list in-progress                - List all in progress tasks    
```

## Examples

```powershell
# Adding a new task
task-cli add "Buy groceries"
# Output: Task added successfully (ID: 1)

# Updating and deleting tasks
task-cli update 1 "Buy groceries and cook dinner"
task-cli delete 1

# Marking a task as in progress or done
task-cli mark-in-progress 1
task-cli mark-done 1

# Listing all tasks
task-cli list

# Listing tasks by status
task-cli list done
task-cli list todo
task-cli list in-progress 
task-cli list inprogress
```
