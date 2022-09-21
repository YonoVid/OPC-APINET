using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAsyncFlow
{
    public static class MortageCalculatorAsync
    {
        public static async Task<int> GetLaboralLifeYears()
        {
            Console.WriteLine("\nGetting laboral life years");
            await Task.Delay(5000);

            return new Random().Next(1, 35); //Get random value as example [1,35]
        }

        public static async Task<bool> IsIndefiniteContract()
        {
            Console.WriteLine("\nGetting if contract is indefinite");

            await Task.Delay(5000);
            return (new Random().Next(1, 10) % 2 == 0);
        }

        public static async Task<int> GetNetIncome()
        {
            Console.WriteLine("\nGetting net pay");
            await Task.Delay(5000);

            return new Random().Next(800, 6000); //Get random value as example [800,6000]
        }

        public static async Task<int> GetMontlyExpenses()
        {
            Console.WriteLine("\nGetting monthly expenses");
            await Task.Delay(5000);

            return new Random().Next(200, 1000); //Get random value as example [200,1000]
        }

        public static bool AnalizeDataToGetAMorge(
            int laboralYears,
            bool isContractIndefinite,
            int netIncome,
            int monthlyExpenses,
            int morgeMoneyAsked,
            int yearsToPay)
        {
            Console.WriteLine("\n Analizing data to get morge");
            if (laboralYears < 2) return false;

            //Monthly amount to pay
            var fee = (morgeMoneyAsked / yearsToPay) / 12;
            if (fee >= netIncome || fee > (netIncome / 2)) return false;

            var porcentageOfExpensesToIncome = (monthlyExpenses * 100) / netIncome;
            if (porcentageOfExpensesToIncome > 30) return false;

            if (fee + monthlyExpenses >= netIncome) return false;

            if(!isContractIndefinite)
            {
                if ((fee + monthlyExpenses) > netIncome / 3) return false;
            }

            return true;
        }
    }
}
