using System;
using System.Diagnostics;

namespace TaskExemple.Parte4
{
	public static class TaskWhenAllAndWhenAny
	{
        internal record FakeParameterObject
        {
            public string? CodiceMagazzino { get; set; }

            public string? CodiceProdotto { get; set; }

            public DateTime OriginalDate { get; set; } // serve per vedere quanto
        }

        private static async Task<FakeParameterObject> FakeAPIMagazzino(string codiceProdotto, DateTime originalDate)
        {
            string codiceMagazzino = string.Empty;

            switch (codiceProdotto)
            {
                case "codice1":
                    {
                        await Task.Delay(4000);
                        codiceMagazzino = "Magazzino7";
                        break;
                    }
                case "codice2":
                    {
                        await Task.Delay(5000);
                        codiceMagazzino = "Magazzino3";
                        break;
                    }
                case "codice3":
                    {
                        await Task.Delay(3000);
                        codiceMagazzino = "Magazzino1";
                        break;
                    }
            }

            return new FakeParameterObject { CodiceMagazzino = codiceMagazzino, CodiceProdotto = codiceProdotto, OriginalDate = originalDate };
        }

        private static async Task FakeAPIPrenotaProdotto(FakeParameterObject orderLine)
        {
            bool Prenotazione = false;

            switch (orderLine.CodiceMagazzino)
            {
                case "magazzino7":
                    {
                        await Task.Delay(7000);

                        if (orderLine.CodiceProdotto.Equals("codice1", StringComparison.InvariantCultureIgnoreCase))
                            Prenotazione = true;

                        break;
                    }
                case "magazzino3":
                    {
                        await Task.Delay(4000);

                        if (orderLine.CodiceProdotto.Equals("codice2", StringComparison.InvariantCultureIgnoreCase))
                            Prenotazione = true;

                        break;
                    }
                case "magazzino1":
                    {
                        await Task.Delay(2000);

                        if (orderLine.CodiceProdotto.Equals("codice3", StringComparison.InvariantCultureIgnoreCase))
                            Prenotazione = true;

                        break;
                    }
            }

            TimeSpan elapsedTimeSpan = DateTime.Now - orderLine.OriginalDate;

            string elapsedTime = string.Format("{0:00}.{1:00}", elapsedTimeSpan.Seconds, elapsedTimeSpan.Milliseconds);

            Console.WriteLine($"Prenotazione prodotto {orderLine.CodiceProdotto} - magazzino {orderLine.CodiceMagazzino} - Stato {Prenotazione} END in {elapsedTime}");
        }

        public static async Task TestWhenAnyReturn()
        {
            string[] plpCodes = { "codice1", "codice2", "codice3" };

            List<Task> selectedTaskList = new List<Task>();

            Console.WriteLine("-------------------- Esecuzione TestWhenAnyReturn --------------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var currentDate = DateTime.Now;

            List<Task<FakeParameterObject>> tasks = plpCodes.Select(x => FakeAPIMagazzino(x, currentDate)).ToList();

            while (tasks.Any())
            {
                Task<FakeParameterObject> taskMagazzino = await Task.WhenAny(tasks);

                FakeParameterObject orderLine = await taskMagazzino;

                tasks.Remove(taskMagazzino);

                selectedTaskList.Add(FakeAPIPrenotaProdotto(orderLine));
            }

            await Task.WhenAll(selectedTaskList);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione di TestWhenAnyReturn: " + elapsedTime);
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static async Task TestWhenAllReturn()
        {
            string[] plpCodes = { "codice1", "codice2", "codice3" };

            Console.WriteLine("-------------------- Esecuzione TestWhenAllReturn --------------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var currentDate = DateTime.Now;

            List<Task<FakeParameterObject>> tasks = plpCodes.Select(x => FakeAPIMagazzino(x, currentDate)).ToList();

            var resultMagazzino = await Task.WhenAll<FakeParameterObject>(tasks);

            var selectedTaskList = resultMagazzino.Select(x => FakeAPIPrenotaProdotto(x)).ToList();

            await Task.WhenAll(selectedTaskList);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione di TestWhenAllReturn:" + elapsedTime);
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static async Task Execute()
        {
            await TestWhenAnyReturn();

            await TestWhenAllReturn();
        }
    }
}

