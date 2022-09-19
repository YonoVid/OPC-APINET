using LinqSnippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityApiBackend.Model.DataModels;

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
        public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            //Paging with skip and take
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage); 
        }

        public static void LinqVariables()
        {
            //Variables
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Average = {0}", numbers.Average());

            foreach (var number in aboveAverage)
                Console.WriteLine("Query: Number = {0} Square {1}", number, Math.Pow(number, 2));
        }

        public static void LinqZip()
        {
            //Zip
            int[] numbers = { 1, 2, 3, 4, 5 };
            String[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<String> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);
                //{ "1=one", ... , "5=five"}
        }

        public static void LinqRepeatRange()
        {
            // Range
            var first1000 = Enumerable.Range(0, 10000); // Generate collection 1 - 1000

            //Repeat
            var fiveXs = Enumerable.Repeat("X", 5); // Generate {"X","X","X","X","X"}
        }

        public static void LinqStudents()
        {
            var classRoom = new[]
            {
                new Student
                {
                    FirstName = "Martín",
                    LastName = "Martín",
                    Id = 1,
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    FirstName = "Juan",
                    LastName = "Juan",
                    Id = 2,
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    FirstName = "Álvaro",
                    LastName = "Álvaro",
                    Id = 3,
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    FirstName = "Ana",
                    LastName = "Ana",
                    Id = 4,
                    Grade = 40,
                    Certified = true
                },
                new Student
                {
                    FirstName = "Pedro",
                    LastName = "Pedro",
                    Id = 5,
                    Grade = 50,
                    Certified = true
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var aprovedStudentsName = from student in classRoom
                                  where student.Grade >= 50 && student.Certified 
                                  select student.FirstName;
        }

        public static void LinqAll()
        {
            //All
            int[] numbers = { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreGreaterOrEqualThan2 = numbers.All(x => x >= 2); // false

            bool allAreGreaterThan0 = numbers.All(x => x > 0); // true
        }

        public static void LinqAggregateQueries()
        {
            //Aggregate
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current); // 55
            // (0 + 1 => 1 + 2 => ... 45 + 10)
            
            //Strings
            string[] words = { "hello,", "my", "name", "is", "Martín" };
            string greeting = words.Aggregate((greeting, word) => greeting + " " + word);
        }

        public static void LinqDistinctValues()
        {
            //Distinct
            int[] numbers = { 1, 2, 3, 4, 5, 4, 3, 2, 1};

            //Sum all numbers
            IEnumerable<int> sum = numbers.Distinct();
        }

        public static void LinqGroupBy()
        {
            //GroupBy
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Obtain even numbers and generate two groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);
            //Generate two groups => Group of even anumbers and group of odd numbers

            foreach(var group in grouped)
            {
                foreach(var value in group)
                {
                    Console.WriteLine(value);
                }
            }

            var classRoom = new[]
            {
                new Student
                {
                    FirstName = "Martín",
                    LastName = "Martín",
                    Id = 1,
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    FirstName = "Juan",
                    LastName = "Juan",
                    Id = 2,
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    FirstName = "Álvaro",
                    LastName = "Álvaro",
                    Id = 3,
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    FirstName = "Ana",
                    LastName = "Ana",
                    Id = 4,
                    Grade = 40,
                    Certified = true
                },
                new Student
                {
                    FirstName = "Pedro",
                    LastName = "Pedro",
                    Id = 5,
                    Grade = 50,
                    Certified = true
                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);
            //Group by certification => Two groups: Certified and Uncertified

            foreach(var group in certifiedQuery)
            {
                Console.WriteLine("{0}", group.Key);
                foreach(var student in group)
                {
                    Console.WriteLine(student.FirstName);
                }
            }

        }

        public static void LinqRelations()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Title = "My first comment",
                            Content = "First comment"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Title = "My second comment",
                            Content = "Second comment"
                        },
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Title = "My third comment",
                            Content = "Third comment"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Title = "My fourth comment",
                            Content = "Fourthcomment"
                        },
                    }
                }
            };

            var commentsContent = 
                posts
                .SelectMany(post => post.Comments, (post, comment) => new { PostId = post.Id , CommentContent = comment.Content});
        }
    }
}