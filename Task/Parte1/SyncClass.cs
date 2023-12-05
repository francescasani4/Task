using System.Diagnostics;

namespace TaskExemple.Parte1
{
	public static class SyncClass
	{
		public static void IstruzioneA()
		{
			Console.WriteLine("Istruzione A start");

			Thread.Sleep(4000);

			Console.WriteLine("Istruzione A end");
		}

        public static void IstruzioneB()
        {
            Console.WriteLine("Istruzione B start");

            Thread.Sleep(3000);

            Console.WriteLine("Istruzione B end");
        }

        public static void IstruzioneC()
        {
            Console.WriteLine("Istruzione C start");

            Thread.Sleep(2000);

            Console.WriteLine("Istruzione C end");
        }

        public static void Execute()
        {
            Console.WriteLine("-------------- Esecuzione Sincrona --------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            IstruzioneA();
            IstruzioneB();
            IstruzioneC();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione: " + elapsedTime);
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}

