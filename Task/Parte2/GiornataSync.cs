using System;
using System.Diagnostics;
using TaskExemple.Parte2.Models;

namespace TaskExemple.Parte2
{
	public static class GiornataSync
	{
        private static PanniLavatrice FareLavatrice()
		{
			Console.WriteLine("Inizio lavaggio lavatrice");

			Thread.Sleep(TempiOperazioni.TempoLavatrice);

			Console.WriteLine($"Fine lavatrice in {TempiOperazioni.TempoLavatrice}");

			return new PanniLavatrice();
		}

        private static void StendiPanni(PanniLavatrice panni)
        {
            Console.WriteLine("Inizio a stendere i panni");

            Thread.Sleep(TempiOperazioni.TempoStenderePanni);

            Console.WriteLine($"Fine stendere i panni in {TempiOperazioni.TempoStenderePanni}");
        }

        private static RicettaMamma ChiamareMamma()
        {
            Console.WriteLine("Chiamo mamma");

            Thread.Sleep(TempiOperazioni.TempoChiamataMamma);

            Console.WriteLine($"Fine chiamata in {TempiOperazioni.TempoChiamataMamma}");

            return new RicettaMamma();
        }

        private static Spesa FareSpesa()
        {
            Console.WriteLine("Vado a fare spesa");

            Thread.Sleep(TempiOperazioni.TempoFareSpesa);

            Console.WriteLine($"Fine spesa in {TempiOperazioni.TempoFareSpesa}");

            return new Spesa();
        }

        private static void PreparareCena(Spesa spesa, RicettaMamma ricetta)
        {
            Console.WriteLine("Inizio a preparare cena");

            Thread.Sleep(TempiOperazioni.TempoPreparareCena);

            Console.WriteLine($"Cena pronta in {TempiOperazioni.TempoPreparareCena}");
        }

        private static void VedereFilm()
        {
            Console.WriteLine("Inizio a vederee il film");
        }

        public static void Execute()
        {
            Console.WriteLine("-------------- Esecuzione Giornata Sincrona --------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var panni = FareLavatrice();

            StendiPanni(panni);

            var ricetta = ChiamareMamma();
            var spesa = FareSpesa();

            PreparareCena(spesa, ricetta);
            VedereFilm();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione: " + elapsedTime);
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}

