using System;
namespace TaskExemple.Parte3
{
	public class TaskEx
	{
		public static void TestAction(string action, int time)
		{
			Console.WriteLine($"{action} Example");

			Thread.Sleep(time);

			Console.WriteLine($"{action} Example End!");
		}

        public static async Task TaskRunExample()
        {
			var task1 = Task.Run(() => TestAction("Task.Run", 1000));
            var task2 = Task.Run(() => TestAction("Task.Run", 2000));
            var task3 = Task.Run(() => TestAction("Task.Run", 3000));

			await task1;
			await task2;
			await task2;
        }

        public static async Task TaskSratNewExample()
        {
            var task1 = Task.Factory.StartNew(
                () => TestAction("Task.Factory.StartNew", 1000));
            var task2 = Task.Factory.StartNew(
                () => TestAction("Task.Factory.StartNew", 2000));
            var task3 = Task.Factory.StartNew(
                () => TestAction("Task.Factory.StartNew", 3000));

            await task1;
            await task2;
            await task2;
        }

        public static async Task Execute()
		{
            Console.WriteLine("-------------- Task.Run START --------------");
            await TaskRunExample();
            Console.WriteLine("-------------- Task.Run END --------------");

            Console.WriteLine("-------------- Task.Factory.StartNew START --------------");
            await TaskSratNewExample();
            Console.WriteLine("-------------- Task.Run END --------------");
        }
    }
}

