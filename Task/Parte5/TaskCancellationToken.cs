using System;
using System.Diagnostics;

namespace TaskExemple.Parte5
{
	public static class TaskCancellationToken
	{
		public static async Task<string> TestAsyncAction(string action, int time, CancellationToken cancellationToken)
		{
			// blocco try{} catch() {} per gestire l'eccezione che viene generata
			try
			{
                Console.WriteLine($"{action} Start");

                await Task.Delay(time, cancellationToken);

                Console.WriteLine($"{action} End");

                return $"result: {action}";
            }
			catch (TaskCanceledException)
			{
				Console.WriteLine($"{action} Cancelled!");

                return $"result: {action}";
            }
        }

        public static async Task<string> TestAsync(string action, int time, CancellationToken cancellationToken)
        {
            // blocco try{} catch() {} per gestire l'eccezione che viene generata
            try
            {
                Console.WriteLine($"{action} Start - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

                await Task.Delay(time, cancellationToken);

                Console.WriteLine($"{action} End - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

                return $"result: {action}";
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine($"{action} Cancelled! - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

                return $"result: {string.Empty}";
            }
        }



        // metodo che simula un operazione pesante
        public static async Task Example()
		{
			int operationTime = 5000;
			string exampleName = "Example 1";

			// CancellationTokenSource cancellationToken = new CancellationTokenSource();
			// cancellationToken.CancelAfter(2000);

			Console.WriteLine($"-----------------------Esecuzione {exampleName}-----------------------");

			Stopwatch stopWatch = new Stopwatch();

			stopWatch.Start();

            CancellationTokenSource cancellationToken = new CancellationTokenSource(2000);
            // cancellationToken.CancelAfter(2000);

            // var task = TestAsyncAction("Example1", operationTime, cancellationToken.Token);
            var task = TestAsync("Example1", operationTime, cancellationToken.Token);

            // await Task.Delay(2000);

			// cancellationToken.Cancel();

			var result = await task;

			Console.WriteLine(result);

			stopWatch.Stop();

			TimeSpan ts = stopWatch.Elapsed;

			string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

			Console.WriteLine($"Tempo di esecuzione di {exampleName} : {elapsedTime}");
			Console.WriteLine("-----------------------------------------------------------------");
		}

		public static async Task Execute()
		{
			await Example();
		}
	}
}

