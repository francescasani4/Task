using System;
using System.Diagnostics;

namespace TaskExemple.Parte1
{
	public static class AsyncClass
	{
        public static Task IstruzioneA()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Istruzione A start");

                Thread.Sleep(4000);

                Console.WriteLine("Istruzione A end");
            });
        }

        public static Task IstruzioneB()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Istruzione B start");

                Thread.Sleep(3000);

                Console.WriteLine("Istruzione B end");
            });
        }

        public static Task IstruzioneC()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Istruzione C start");

                Thread.Sleep(2000);

                Console.WriteLine("Istruzione C end");
            });
        }

        public static async Task Execute()
        {
            Console.WriteLine("-------------- Esecuzione Asincrona --------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // si prederebbe il vantaggio della programmazione asincrona
            await IstruzioneA();
            await IstruzioneB();
            await IstruzioneC();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione: " + elapsedTime);
            Console.WriteLine("--------------------------------------------------------");

            //return Task.CompletedTask;
        }

        public static async Task Execute1()
        {
            Console.WriteLine("-------------- Esecuzione Asincrona --------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var task1 = IstruzioneA();
            var task2 = IstruzioneB();
            var task3 = IstruzioneC();

            await task1;
            await task2;
            await task3;

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione: " + elapsedTime);
            Console.WriteLine("--------------------------------------------------------");
        }

        public static async Task Execute2()
        {
            Console.WriteLine("-------------- Esecuzione Asincrona --------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var task1 = IstruzioneA();
            var task2 = IstruzioneB();
            var task3 = IstruzioneC();

            // attende il completamente di tutti e tre i task
            await Task.WhenAll(task1, task2, task3);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione: " + elapsedTime);
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}

