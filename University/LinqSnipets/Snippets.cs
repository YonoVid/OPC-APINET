using LinqSnippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnipets
{
    public class Snippets
    {
        public static void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Feat Ibiza",
                "Feat León"
            };

            //Select * of cars
            var carList = from car in cars select car;

            foreach (var car in carList) Console.WriteLine(car);

            //Select where car is "Audi"
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach(var audi in audiList) Console.WriteLine(audi);

        }

        public static void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Multiply by 3 => Exclude 9 => Sort in ascending order
            var processedNumberList = 
                numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);

        }

        public static void SearchExamples()
        {
            List<String> textList = new List<String>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c",
            };

            //1. First of all elements
            var first = textList.First();

            //2. First element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            //3. First element that contains "j"
            var jText = textList.First(text => text.Contains("j"));

            //4. First element that contains "z" or default
            var zFirst= textList.FirstOrDefault(text => text.Contains("z"));

            //5. Last element that contains "z" or default
            var zLast = textList.LastOrDefault(text => text.Contains("z"));

            //6. Single values
            var uniqueText = textList.Single();
            var uniqueDefaultText = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8};
            int[] otherEvenNumbers = { 0, 2, 6 };

            //Obtain {4, 8} 
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
        }

        public static void MulipleSelects()
        {
            //Select many
            string[] myOpinions =
            {
                "Opinion 1, Text 1",
                "Opinion 2, Text 2",
                "Opinion 3, Text 3"
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new []
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Martín",
                            Email = "martin@imaginationgroup.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "pepe@imaginationgroup.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Juanjo",
                            Email = "juanjo@imaginationgroup.com",
                            Salary = 2000
                        },
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new []
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "ana@imaginationgroup.com",
                            Salary = 3500
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Mario",
                            Email = "mario@imaginationgroup.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Poco",
                            Email = "poco@imaginationgroup.com",
                            Salary = 2500
                        },
                    }
                }
            };

            //Obtain all employees
            var allEmployees = enterprises.SelectMany(enterprise => enterprise.Employees);

            //Know if a list is empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //Enterprises has employees with at least 1000 salary
            bool hasEmployeesWithSalaryAtLeast1000 =
                enterprises
                .Any(enterprise => enterprise.Employees
                .Any(employee => employee.Salary >= 1000));
        }

        static public void LinqCollections()
        {
            var firstList = new List<String>() { "a", "b", "c" };
            var secondList = new List<String>() { "a", "c", "d" };

            //Inner join
            var innerJoin = from element in firstList 
                            join secondElement in secondList
                            on element equals secondElement
                            select new { element, secondElement };
            var functionInnerJoin = firstList
                .Join(secondList, 
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement });

            //Outter join
            var leftOutterJoin = from element in firstList
                                 join secondElement in secondList
                                 on element equals secondElement
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where element != temporalElement
                                 select new { Element = element };

            /*
            var leftOutterJoin2 = from element in firstList
                                  from secondElement in secondList.Where(s => s == element).DefaultIfEmpty
                                  select new { Element = element , SecondElement = secondElement};
            */

            var rightOutterJoin = from secondElement in secondList
                                  join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            //Union
            var unionList = leftOutterJoin.Union(rightOutterJoin); 
        }

        public static void SkipTakeLinq()
        {
            var myList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            //Skip
            var skipFirstTwoValues = myList.Skip(2); // {3, 4, 5, 6, 7, 8, 9, 10}

            var skipLastTwoValues = myList.SkipLast(2);// {1, 2, 3, 4, 5, 6, 7, 8}

            var skipWhileSmalletThan4 = myList.SkipWhile(num => num < 4);// {5, 6, 7, 8, 9, 10}

            //Take
            var takeFirstTwoValues = myList.Take(2); //{1, 2}

            var takeLastTwoValues = myList.TakeLast(2); //{9, 10}

            var takeWhileSmalletThan4 = myList.TakeWhile(num => num < 4); //{9, 10}
        }
    }
}