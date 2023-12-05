using System;
using System.Diagnostics;
using TaskExemple.Parte2.Models;

namespace TaskExemple.Parte2
{
	public static class GiornataAsyncV1
	{
        private static async Task<PanniLavatrice> FareLavatrice()
        {
            Console.WriteLine("Inizio lavaggio lavatrice");

            await Task.Delay(TempiOperazioni.TempoLavatrice);

            Console.WriteLine($"Fine lavatrice in {TempiOperazioni.TempoLavatrice}");

            return new PanniLavatrice();
        }

        private static async Task StendiPanni(PanniLavatrice panni)
        {
            Console.WriteLine("Inizio a stendere i panni");

            await Task.Delay(TempiOperazioni.TempoStenderePanni);

            Console.WriteLine($"Fine stendere i panni in {TempiOperazioni.TempoStenderePanni}");
        }

        private static async Task<RicettaMamma> ChiamareMamma()
        {
            Console.WriteLine("Chiamo mamma");

            await Task.Delay(TempiOperazioni.TempoChiamataMamma);

            Console.WriteLine($"Fine chiamata in {TempiOperazioni.TempoChiamataMamma}");

            return new RicettaMamma();
        }

        private static async Task<Spesa> FareSpesa()
        {
            Console.WriteLine("Vado a fare spesa");

            await Task.Delay(TempiOperazioni.TempoFareSpesa);

            Console.WriteLine($"Fine spesa in {TempiOperazioni.TempoFareSpesa}");

            return new Spesa();
        }

        private static async Task PreparareCena(Spesa spesa, RicettaMamma ricetta)
        {
            Console.WriteLine("Inizio a preparare cena");

            await Task.Delay(TempiOperazioni.TempoPreparareCena);

            Console.WriteLine($"Cena pronta in {TempiOperazioni.TempoPreparareCena}");
        }

        private static Task VedereFilm()
        {
            Console.WriteLine("Inizio a vederee il film");

            return Task.CompletedTask;
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

