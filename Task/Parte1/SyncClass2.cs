using System.Diagnostics;

namespace TaskExemple.Parte1
{
    public static class SyncClass2
    {
        public static int IstruzioneA()
        {
            Console.WriteLine("Istruzione A start");

            Thread.Sleep(4000);

            Console.WriteLine("Istruzione A end");

            return 5;
        }

        public static int IstruzioneB()
        {
            Console.WriteLine("Istruzione B start");

            Thread.Sleep(3000);

            Console.WriteLine("Istruzione B end");

            return 7;
        }

        public static void IstruzioneC(int inputIstruzioneA, int inputIstruzioneB)
        {
            Console.WriteLine("Istruzione C start");

            Thread.Sleep(2000);

            int somma = inputIstruzioneA + inputIstruzioneB;

            Console.WriteLine($"Istruzione C end - Risultato: {somma}");
        }

        public static void Execute()
        {
            Console.WriteLine("-------------- Esecuzione Sincrona --------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var resultA = IstruzioneA();
            var resultB = IstruzioneB();
            IstruzioneC(resultA, resultB);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione: " + elapsedTime);
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}



