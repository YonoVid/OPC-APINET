using System.Diagnostics;
using ExampleAsyncFlow;

//Start watch for Async function
Stopwatch sw = Stopwatch.StartNew();

Console.WriteLine("Welcome to the \'SYNC Mortage Calculator\'");

var laboralYears = MortageCalculatorSync.GetLaboralLifeYears();
Console.WriteLine($"Laboral years {laboralYears}");

var isIndefiniteContract = MortageCalculatorSync.IsIndefiniteContract();
Console.WriteLine($"Is indefinite contract {(isIndefiniteContract? "Yes":"NO")}");

var netIncome = MortageCalculatorSync.GetNetIncome();
Console.WriteLine($"Net income ${netIncome}");

var monthlyExpenses = MortageCalculatorSync.GetMontlyExpenses();
Console.WriteLine($"Monthly expenses ${monthlyExpenses}");

var isMortageApproved = MortageCalculatorSync
    .AnalizeDataToGetAMorge(laboralYears, 
    isIndefiniteContract, netIncome, monthlyExpenses,
    morgeMoneyAsked: 50000, yearsToPay: 30);

var result = isMortageApproved ? "APPROVED": "REJECTED";

Console.WriteLine($"RESULT CALCULATED, MORTAGE: " + isMortageApproved);

sw.Stop();
Console.WriteLine($"Time of execution SYNC FUNCTION:: {sw.Elapsed}");

//Start watch for Sync function
sw.Restart();

Console.WriteLine("Welcome to the \'ASYNC Mortage Calculator\'");

Task<int> asyncLaboralYears = MortageCalculatorAsync.GetLaboralLifeYears();

Task <bool> asyncIsIndefiniteContract = MortageCalculatorAsync.IsIndefiniteContract();

Task <int> asyncNetIncome = MortageCalculatorAsync.GetNetIncome();

Task <int> asyncMonthlyExpenses = MortageCalculatorAsync.GetMontlyExpenses();

var asyncTaskList = new List<Task>
{
    asyncLaboralYears,
    asyncIsIndefiniteContract,
    asyncNetIncome,
    asyncMonthlyExpenses
};

while (asyncTaskList.Any())
{
    Task completeTask = await Task.WhenAny(asyncTaskList);

    if(completeTask == asyncLaboralYears)
    {
        Console.WriteLine($"Laboral years {asyncLaboralYears.Result}");
    }
    else if(completeTask == asyncIsIndefiniteContract)
    {
        Console.WriteLine($"Is indefinite contract {(asyncIsIndefiniteContract.Result? "Yes" : "NO")}");
    }
    else if(completeTask == asyncNetIncome)
    {
        Console.WriteLine($"Net income ${asyncNetIncome.Result}");
    }
    else if(completeTask == asyncMonthlyExpenses)
    {
        Console.WriteLine($"Monthly expenses ${asyncMonthlyExpenses.Result}");
    }

    asyncTaskList.Remove(completeTask);

}


isMortageApproved = MortageCalculatorSync
    .AnalizeDataToGetAMorge(asyncLaboralYears.Result,
    asyncIsIndefiniteContract.Result, asyncNetIncome.Result, asyncMonthlyExpenses.Result,
    morgeMoneyAsked: 50000, yearsToPay: 30);


result = isMortageApproved ? "APPROVED" : "REJECTED";

Console.WriteLine($"RESULT CALCULATED, MORTAGE: " + isMortageApproved);

sw.Stop();
Console.WriteLine($"Time of execution ASYNC FUNCTION:: {sw.Elapsed}");
