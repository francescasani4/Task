using System;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace TaskExemple.Parte6
{
	public static class TestCancellationToken
	{
        // primitiva di sincronizzazione
        private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

		public static async Task OperazionePesante(CancellationToken cancellationToken)
		{
			int timeOperation = 500;

			Console.WriteLine($"OperazionePesante Start - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

			for(int i = 0; i < 10; i++)
			{
				Console.WriteLine($"OperazionePesante Esecuzione - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

				await Task.Delay(timeOperation); // serve solo per simulare che l'operazione è pesante

				if(cancellationToken.IsCancellationRequested) // uso del polling
				{
                    Console.WriteLine($"OperazionePesante Esecuzione - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

					cancellationToken.ThrowIfCancellationRequested();
                }
			}

            Console.WriteLine($"OperazionePesante End - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");
        }

        public static async Task OperazionePesante1(CancellationToken cancellationToken)
        {
            Console.WriteLine($"OperazionePesante Start - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket server = null;

            cancellationToken.Register(() =>
            {
                // call back che chiude il socket
                server?.Close();
            });

            try
            {
                await Task.Delay(100);

                server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                server.Bind(localEndPoint);
                server.Listen(1);

                Console.WriteLine("Waiting for a connection...");

                // il flusso resta bloccato qua,
                // per aggirare questo esiste un costrutto particolare: REGISTER su cancellation token
                var client = server.Accept(); 
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine($"OperazionePesante End - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");
            }
        }

        public static async Task OperazionePesante2(CancellationToken cancellationToken)
        {
            Console.WriteLine($"OperazionePesante Start - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

            await Task.Delay(100);

            WaitHandle.WaitAny(new[] { manualResetEvent, cancellationToken.WaitHandle });

            cancellationToken.ThrowIfCancellationRequested();

            //manualResetEvent.WaitOne();

            Console.WriteLine("OperazionePesante in Esecuzione...");

            Console.WriteLine($"OperazionePesante End - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

            cancellationToken.ThrowIfCancellationRequested();
        }


        public static async Task TestAction(CancellationToken cancellationToken)
		{
			// int timeOperation = 5000;

			try
			{
				Console.WriteLine($"TestAction Start - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");

                // await Task.Delay(timeOperation, cancellationToken);
                // await OperazionePesante(cancellationToken);
                // await OperazionePesante1(cancellationToken);
                await OperazionePesante2(cancellationToken);


                Console.WriteLine($"TestAction End - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");
			}
			catch (OperationCanceledException) // catch (TaskCanceledException)
            {
				Console.WriteLine($"TestAction Cancelled! - Richiesta di cancellazione = {cancellationToken.IsCancellationRequested}");
			}
		}

		public static async Task Example()
		{
			CancellationTokenSource cancellationToken = new CancellationTokenSource();

			string exampleName = "Example 1";

            Console.WriteLine($"-----------------------Esecuzione {exampleName}-----------------------");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var task = TestAction(cancellationToken.Token);

            await Task.Delay(2000);

            // cancellationToken.Cancel();
            manualResetEvent.Set();

            await task;

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

