namespace TaskTracker
{
	class Task
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public Status Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Task()
		{
			Status = Status.ToDo;
			CreatedAt = DateTime.Now;
			UpdatedAt = null;
		}
	}
}
