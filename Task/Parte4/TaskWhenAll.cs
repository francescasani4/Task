using System;
using System.Diagnostics;

namespace TaskExemple.Parte4
{
	public static class TaskWhenAll
	{
		public static async Task TestAsyncAction(string action, int time)
		{
			Console.WriteLine($"{action} Start");

			await Task.Delay(time);

			Console.WriteLine($"{action} End");
		}

		public static async Task TestAll()
		{
			Console.WriteLine("-------------------- Esecuzione TestAll --------------------");

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			var task1 = TestAsyncAction("Metodo async1", 1000);
            var task2 = TestAsyncAction("Metodo async2", 2000);
            var task3 = TestAsyncAction("Metodo async3", 4000);

			await Task.WhenAll(task1, task2, task3);

			stopWatch.Stop();

			TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione di TestAll:" + elapsedTime);
			Console.WriteLine("---------------------------------------------------------------");
        }

		public static async Task<string> FakeAPICall(string codiceProdotto)
		{
			string returnValue = string.Empty;

            Console.WriteLine($"FakeAPICall per il prodotto {codiceProdotto} START");

            switch (codiceProdotto)
			{
				case "codice1":
					{
						await Task.Delay(1000);
						returnValue = "Prodotto 1";
						break;
					}
				case "codice2":
					{
                        await Task.Delay(5000);
                        returnValue = "Prodotto 2";
                        break;
                    }
				case "codice3":
					{
                        await Task.Delay(3000);
                        returnValue = "Prodotto 3";
                        break;
                    }
			}

			Console.WriteLine($"FakeAPICall per il prodotto {codiceProdotto} END");

			return returnValue;
		}

		public static async Task TestWhenAllWithReturn_BadWay()
		{
			string[] plpCodes = { "codice1", "codice2", "codice3" };

			Console.WriteLine("-------------------- Esecuzione TestWhenAllWithReturn_BadWay --------------------");

			Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

			foreach(var code in plpCodes)
			{
				var productDetail = await FakeAPICall(code);

				Console.WriteLine($"Codice prodott = {code} - Nome Prodotto = {productDetail}");
			}

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione di TestWhenAllWithReturn_BadWay:" + elapsedTime);
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static async Task TestWhenAllWithReturn()
        {
            string[] plpCodes = { "codice1", "codice2", "codice3" };

            Console.WriteLine("-------------------- Esecuzione TestWhenAllWithReturn --------------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

			Task<string>[] tasks = plpCodes.Select(x => FakeAPICall(x)).ToArray();

			var result = await Task.WhenAll(tasks); 

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione di TestWhenAllWithReturn:" + elapsedTime);
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static async Task TestWhenAllWithReturn_Order()
        {
            string[] plpCodes = { "codice1", "codice2", "codice3" };

            Console.WriteLine("-------------------- Esecuzione TestWhenAllWithReturn_Order --------------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Task<string>[] tasks = plpCodes.Select(x => FakeAPICall(x)).ToArray();

            var result = await Task.WhenAll(tasks);

            stopWatch.Stop();

			foreach(var singleResult in result)
			{
				Console.WriteLine($"Result = {singleResult}");
			}

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione di TestWhenAllWithReturn:" + elapsedTime);
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static async Task Execute()
		{
			// await TestAll();
			// await TestWhenAllWithReturn_BadWay();
			// await TestWhenAllWithReturn();
			await TestWhenAllWithReturn_Order();
		}
    }
}

