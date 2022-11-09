using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace linq_practice
{

    // init Northwind db
    [Table(Name = "Customers")]
    public class Customer
    {
        private string _CustomerID;
        [Column(IsPrimaryKey = true, Storage = "_CustomerID")]
        public string CustomerID
        {
            get { return this._CustomerID; }
            set { this._CustomerID = value; }
        }

        private string _City;
        [Column(Storage = "_City")]
        public string City
        {
            get { return this._City; }
            set { this._City = value; }
        }
    }

    [Table(Name = "Orders")]
    public class Order
    {
        private int _OrderID = 0;
        private string _CustomerID;
        private EntityRef<Customer> _Customer;
        public Order() { this._Customer = new EntityRef<Customer>(); }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Use a connection string.
            DataContext db = new DataContext
                ("data source=MSI;initial catalog=Northwind;trusted_connection=true");


           
            // Get a typed table to run queries.
            Table<Customer> Customers = db.GetTable<Customer>();



            // Attach the log to show generated SQL.
            db.Log = Console.Out;

            // Query for customers in London.
            var custQuery =
                from cust in Customers
                where cust.City == "London"
                select cust;

            foreach (Customer cust in custQuery)
            {
                Console.WriteLine("ID={0}, City={1}", cust.CustomerID,
                    cust.City);
            }

            //// Prevent console window from closing.
            //Console.ReadLine();



            //code from previous linq demos
            //creating different linq queries and executing them (collapase highlight block using ctrl+m, ctrl+h)
            //create the linq query
            //var studentQuery =         // var is implicit typing for IEnumerable<Student>
            //    from student in students
            //    where student.Scores[0] > 90 
            //    orderby student.Scores[0] descending
            //    select student;


            //// create grouping linq query (The following new query groups the students by using the first letter of their last name as the key.)
            //var studentQueryTwo =
            //    from student in students
            //    group student by student.Last[0];

            //// create grouping linq query (The following new query groups the students by using the first letter of their last name as the key alphabetically.)
            //var studentQueryThree =
            //    from student in students
            //    group student by student.Last[0] into studentGroup
            //    orderby studentGroup.Key
            //    select studentGroup;

            //// create linq query that uses a let command (lets us stroe the result of sub-expression)
            //var studentQuery4 =
            //    from student in students
            //    let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
            //    let avg = totalScore / 4
            //    where avg < student.Scores[0]
            //    select student.Last + " " + student.First;

            //var studentQuery5 =
            //    from student in students
            //    let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
            //    select totalScore;

            //var avgScore = studentQuery5.Average();
            //Console.WriteLine("the avg is {0} for the class", avgScore);

            // execution of linq query

            //foreach (var studentGroup in studentQueryThree)
            //{
            //    Console.WriteLine(studentGroup.Key);
            //    foreach (var student in studentGroup)
            //    {
            //        Console.WriteLine("      {0}, {1}", student.Last, student.First);
            //    }
            //}

            //foreach (var s in studentQuery4)
            //{
            //    Console.WriteLine(s);
            //}


        }

                // Create a data source by using a collection initializer.
                static List<Student> students = new List<Student>
                {
                    new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
                    new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
                    new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
                    new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
                    new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
                    new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
                    new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
                    new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
                    new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
                    new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
                    new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
                    new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91}}
                };

            //NOTE!! to get connection string, execute code below in ssms db
        //            select
        //    'data source=' + @@servername +
        //    ';initial catalog=' + db_name() +
        //    case type_desc
        //        when 'WINDOWS_LOGIN'
        //            then ';trusted_connection=true'
        //        else
        //                ';user id=' + suser_name() + ';password=<<YourPassword>>'
        //    end
        //    as ConnectionString
        //from sys.server_principals
        //where name = suser_name()
    }
}

public class Student
{
    public String First { get; set; }
    public String Last { get; set; }    
    public int ID { get; set; }

    public List<int> Scores;
}





// continue on https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/walkthrough-querying-across-relationships-csharp 