using System;
using System.Diagnostics;

namespace TaskExemple.Parte4
{
	public static class TaskWhenAny
	{
        public static async Task<string> FakeAPICall(string codiceProdotto)
        {
            string returnValue = string.Empty;

            Console.WriteLine($"FakeAPICall per il prodotto {codiceProdotto} START");

            switch (codiceProdotto)
            {
                case "codice1":
                    {
                        await Task.Delay(10000);
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

        public static async Task TestWhenAnyWithReturn()
        {
            string[] plpCodes = { "codice1", "codice2", "codice3" };

            Console.WriteLine("-------------------- Esecuzione TestWhenAnyWithReturn --------------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Task<string>[] tasks = plpCodes.Select(x => FakeAPICall(x)).ToArray();

            var result = await Task.WhenAny(tasks);

            stopWatch.Stop();

            // si deve mettere result.Result per ottenere il risultato che si vuole,
            // ma questa non è la soluzione più corretta
            // Console.WriteLine($"Result = {result.Result}");

            // modo corretto per ottenere il risultato desiderato
            var stringResult = await result;

            Console.WriteLine($"Result = {stringResult}");

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione di TestWhenAnyWithReturn: " + elapsedTime);
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static async Task Execute()
        {
            await TestWhenAnyWithReturn();
        }
    }
}

