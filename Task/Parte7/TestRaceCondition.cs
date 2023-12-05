using System;
namespace TaskExemple.Parte7
{
	public static class TestRaceCondition
	{
		private static int RaceConditionVariable = 0;

		private static void RaceConditionAction1()
		{
			Thread.Sleep(50);
			RaceConditionVariable = 3;
		}

        private static void RaceConditionAction2()
        {
            Thread.Sleep(50);
            RaceConditionVariable = 5;
        }

        private static void RaceConditionAction3()
        {
            Thread.Sleep(50);
            RaceConditionVariable = 7;
        }

        private static void RaceConditionAction4()
        {
            Thread.Sleep(50);
            RaceConditionVariable = 11;
        }

        public static void RaceCondition()
        {
            Thread thread1 = new Thread(RaceConditionAction1);
            Thread thread2 = new Thread(RaceConditionAction2);
            Thread thread3 = new Thread(RaceConditionAction3);
            Thread thread4 = new Thread(RaceConditionAction4);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

            Console.WriteLine($"Il valore di RaceConditionVariable è: {RaceConditionVariable}");
        }
    }
}

