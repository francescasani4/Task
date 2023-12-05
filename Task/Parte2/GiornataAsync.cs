using System;
using System.Diagnostics;
using TaskExemple.Parte2.Models;

namespace TaskExemple.Parte2
{
	public static class GiornataAsync
	{
        private static Task<PanniLavatrice> FareLavatrice()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Inizio lavaggio lavatrice");

                Thread.Sleep(TempiOperazioni.TempoLavatrice);

                Console.WriteLine($"Fine lavatrice in {TempiOperazioni.TempoLavatrice}");

                return new PanniLavatrice();
            });
            
        }

        private static Task StendiPanni(PanniLavatrice panni)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Inizio a stendere i panni");

                Thread.Sleep(TempiOperazioni.TempoStenderePanni);

                Console.WriteLine($"Fine stendere i panni in {TempiOperazioni.TempoStenderePanni}");
            });
            
        }

        private static Task<RicettaMamma> ChiamareMamma()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Chiamo mamma");

                Thread.Sleep(TempiOperazioni.TempoChiamataMamma);

                Console.WriteLine($"Fine chiamata in {TempiOperazioni.TempoChiamataMamma}");

                return new RicettaMamma();
            });
        }

        private static Task<Spesa> FareSpesa()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Vado a fare spesa");

                Thread.Sleep(TempiOperazioni.TempoFareSpesa);

                Console.WriteLine($"Fine spesa in {TempiOperazioni.TempoFareSpesa}");

                return new Spesa();
            });
        }

        private static Task PreparareCena(Spesa spesa, RicettaMamma ricetta)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Inizio a preparare cena");

                Thread.Sleep(TempiOperazioni.TempoPreparareCena);

                Console.WriteLine($"Cena pronta in {TempiOperazioni.TempoPreparareCena}");
            });
        }

        private static Task VedereFilm()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Inizio a vederee il film");
            });
        }

        public static async Task Execute()
        {
            Console.WriteLine("-------------- Esecuzione Giornata Asincrona --------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var taskLavatrice = FareLavatrice();
            var taskRicettaMamma = ChiamareMamma();
            var taskSpesa = FareSpesa();

            var panni = await taskLavatrice;

            await StendiPanni(panni);

            var ricetta = await taskRicettaMamma;
            var spesa = await taskSpesa;
            
            await PreparareCena(spesa, ricetta);

            await VedereFilm();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = string.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds);

            Console.WriteLine("Tempo di esecuzione: " + elapsedTime);
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}

