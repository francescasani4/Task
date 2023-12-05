using System;
namespace TaskExemple.Parte3
{
	public class TaskEx1
	{
        public static void TestAction(string action, int time)
        {
            Console.WriteLine($"{action}");

            Thread.Sleep(time);

            Console.WriteLine($"{action} End!");
        }

        private static Task CreateTestTask(string action, int time)
		{
			return new Task(() =>
			{
				Console.WriteLine($"{action} Start");
				Thread.Sleep(time);
				Console.WriteLine($"{action} End");
			}, TaskCreationOptions.AttachedToParent);
		}

		public static async Task TaskRunExample_TaskCreationOptions()
		{
			Task? TaskInterno = null;

            Console.WriteLine("-------------- Esecuzione Task.Run --------------");

			var task1 = Task.Run(() =>
			{
				TaskInterno = CreateTestTask("===> Esecuzione Task.Run interno", 3000);
				TaskInterno.Start(TaskScheduler.Default);

				TestAction("=> Metodo Task.Run esterno", 1000);
			});

            await task1;

            Console.WriteLine("-------------- Fine esecuzione Task.Run --------------");
			Console.WriteLine($"Task completed => {task1.IsCompleted} Task1 interno completed {TaskInterno?.IsCompleted}");
        }

        public static async Task TaskFactoryExample_TaskCreationOptions()
        {
            Task? TaskInterno = null;

            Console.WriteLine("-------------- Esecuzione Task.Factory.StartNew --------------");

            var task1 = Task.Factory.StartNew(() =>
            {
                TaskInterno = CreateTestTask("===> Esecuzione Task.Factory.StartNew interno", 3000);
                TaskInterno.Start(TaskScheduler.Default);

                TestAction("=> Metodo Task.Factory.StartNew esterno", 1000);
            });

            await task1;

            Console.WriteLine("-------------- Fine esecuzione Task.Factory.StartNew --------------");
            Console.WriteLine($"Task completed => {task1.IsCompleted} Task1 interno completed {TaskInterno?.IsCompleted}");
        }

        private static async Task TestTaskRun_State()
        {
            var tasks = new List<Task>();

            for (var i = 0; i < 10; i++)
            {
                // await Task.Delay(40);

                var index = i;

                var task = Task.Run(async () =>
                {
                    await Task.Delay(200);
                    Console.WriteLine($"Indice {index}");
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks.ToArray());
        }

        private static async Task TestTaskFactory_State()
        {
            var tasks = new List<Task>();

            for (var i = 0; i < 10; i++)
            {
                var task = await Task.Factory.StartNew(async (index) =>
                {
                    await Task.Delay(100);
                    Console.WriteLine($"Indice {index}");
                }, i);

                tasks.Add(task);
            }

            await Task.WhenAll(tasks.ToArray());
        }

        public static async Task Execute()
		{
            // await TaskRunExample_TaskCreationOptions();

            // await TaskFactoryExample_TaskCreationOptions();

            // await TestTaskRun_State();

            await TestTaskFactory_State();
		}
	}
}

