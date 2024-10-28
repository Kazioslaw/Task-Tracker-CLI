namespace TaskTracker
{
	internal class Program
	{
		static void Main(string[] args)
		{

			Console.Write("Wybierz co chcesz zrobić: ");
			int userInput = 0;
			if (!Int32.TryParse(Console.ReadLine(), out userInput))
			{
				Console.Write("Błędna komenda wpisz ponownie: ");
			};


		}
	}
}
