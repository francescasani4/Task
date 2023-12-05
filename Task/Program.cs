using TaskExemple.Parte1;
using TaskExemple.Parte2;
using TaskExemple.Parte3;
using TaskExemple.Parte4;
using TaskExemple.Parte5;
using TaskExemple.Parte6;
using TaskExemple.Parte7;


// Parte 1

//SyncClass.Execute();

//SyncClass2.Execute();

//await AsyncClass.Execute();

//await AsyncClass.Execute1();

//await AsyncClass.Execute2();

// Parte 2

//GiornataSync.Execute();

//await GiornataAsync.Execute();

//await GiornataAsyncV1.Execute();


// Parte 3

//await TaskEx.Execute();

// await TaskEx1.Execute();

// Parte 4

// await TaskWhenAll.Execute();

// await TaskWhenAny.Execute();

// await TaskWhenAllAndWhenAny.Execute();

// Parte 5

// await TaskCancellationToken.Execute();

// Parte 6

// await TestCancellationToken.Execute();

// Parte 7

/*for(int i = 0; i < 10; i++)
{
    TestRaceCondition.RaceCondition();
}

Console.Read();*/

CheckSynchronizationContext();

SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

CheckSynchronizationContext();

static void CheckSynchronizationContext()
{
    var currentSynchronizationContext = SynchronizationContext.Current;

    if(currentSynchronizationContext == null)
    {
        Console.WriteLine("No SynchronizationContext");
    }
    else
    {
        Console.WriteLine($"SynchronizationContext Preset = {currentSynchronizationContext}");
    }
}