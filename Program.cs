using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CrimeAnalyzer

{

    public class CrimeStatistics

    {

        public int robbery;
        public int assault;
        public int property;
        public int burglary;
        public int theft;
        public int vehicle;
        public int year;
        public int murder;
        public int rape;
        public int population;
        public int violentcrime;

        public CrimeStatistics(int population, int year, int rape, int murder, int violentcrime, int robbery, int assault, int theft, int burglary, int property, int vehicle)

        {
            this.robbery = robbery;
            this.assault = assault;
            this.property = property;
            this.burglary = burglary;
            this.theft = theft;
            this.vehicle = vehicle;
            this.year = year;
            this.population = population;
            this.violentcrime = violentcrime;
            this.murder = murder;
            this.rape = rape;
        }

    }


    class Program

    {

        static void Main(string[] args)

        {
            String input;
            String newFile;
            List<CrimeStatistics> list = new List<CrimeStatistics>();
            int count = 0;

            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect arguments. Format Should Be \n dotnet CrimeAnalyzer.dll <csv_file_path> <report_file_path>  \n");
                Environment.Exit(-1);
            }

            input = args[0];

            if (File.Exists(input) == false)
            {
                Console.WriteLine("File Does Not Exist. ");
                Environment.Exit(-1);
            }

            using (var reader = new StreamReader(input))
            {


                string header = reader.ReadLine();
                var hValues = header.Split(',');

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    int year = Convert.ToInt32(values[0]);
                    int population = Convert.ToInt32(values[1]);
                    int violentcrime = Convert.ToInt32(values[2]);
                    int murder = Convert.ToInt32(values[3]);
                    int rape = Convert.ToInt32(values[4]);
                    int robbery = Convert.ToInt32(values[5]);
                    int assault = Convert.ToInt32(values[6]);
                    int property = Convert.ToInt32(values[7]);
                    int burglary = Convert.ToInt32(values[8]);
                    int theft = Convert.ToInt32(values[9]);
                    int vehicle = Convert.ToInt32(values[10]);


                    list.Add(new crimeStatistics(year, population, violentcrime, rape, murder, robbery, assault, property, burglary, theft, vehicle));
                }

            }

            string results = "";

            var years = from CrimeStatistics in list select CrimeStatistics.year;
            foreach (var x in years)
            {
                count++;
            }

            var question3Murders = from CrimeStatistics in list where CrimeStatistics.murder < 15000 select CrimeStatistics.year;

            var question4Robberies = from CrimeStatistics in list where CrimeStatistics.robbery > 500000 select new { CrimeStatistics.year, CrimeStatistics.robbery };

            var question5Violence = from CrimeStatistics in list where CrimeStatistics.year == 2010 select CrimeStatistics.violentcrime;

            var question5Capita = from CrimeStatistics in list where CrimeStatistics.year == 2010 select CrimeStatistics.population;


            double v = 0;

            double c = 0;

            foreach (var x in question5Violence)
            
            {
            
                    v = (double)x;
            }


            foreach (var x in question5Capita)
            
            {
            
                    c = (double)x;
            }



            double question5Answer = v / c;


            var question6 = from CrimeStatistics in list select CrimeStatistics.murder;

            double question6Murders = 0;

            foreach (var i in question6)
            {
                question6Murders += i;
            }

            double question6Answer = question6Murders / count;

            var question7 = from CrimeStatistics in list where CrimeStatistics.year >= 1994 && CrimeStatistics.year <= 1997 select CrimeStatistics.murder;

            double question7Murder = 0;

            int question7Count = 0;

            foreach (var x in question7)
            {
                question7Murder += x;

                question7Count++;
            }

            double question7Answer = question7Murder / question7Count;

            var question8 = from CrimeStatistics in list where CrimeStatistics.year >= 2010 && CrimeStatistics.year <= 2013 select CrimeStatistics.murder;

            double question8Murder = 0;

            int question8Count = 0;

            foreach (var x in question8)
            {
                question8Murder += x;
                question8Count++;
            }

            double question8Answer = question8Murder / question8Count;


            var question9 = from CrimeStatistics in list where CrimeStatistics.year >= 1999 && CrimeStatistics.year <= 2004 select CrimeStatistics.theft;

            int question9Answer = question9.Min();

            var question10 = from CrimeStatistics in list where CrimeStatistics.year >= 1999 && CrimeStatistics.year <= 2004 select CrimeStatistics.theft;

            int question10Answer = question10.Max();

            var question11 = from CrimeStatistics in list select new { CrimeStatistics.year, CrimeStatistics.vehicle };

            int question11Answer = 0;

            int new = 0;

            foreach (var x in question11)
            {

                if (x.vehicle > new)
                {
                    question11Answer = x.year;
                    new = x.vehicle;
                }
            }


            results += "The Range Of Years Include " + years.Min() + " - " + years.Max() + " (" + count + " years) \n";


            results += "Years Murders Per Year < 15000: ";

            foreach (var x in question3Murders)
            {
                results += x + " ";
            }
            results += "\n";


            results += "Robberies Per Year > 500000: ";

            foreach (var x in question4Robberies)
           
             {
                results += string.Format("{0} = {1}, ", x.year, x.robbery);
           
             }

            results += "\n";

            results += "Violent Crime Per Capita Rate (2010): " + question5Answer + "\n";

            results += "Average Murder Per Year (Across All Years): " + question6Answer + "\n";

            results += "Average Murder Per Year (1994 To 1997): " + question7Answer + "\n";

            results += "Average Murder Per Year (2010 To 2013): " + question8Answer + "\n";

            results += "Minimum Thefts Per Year (1999 To 2004): " + question9Answer + "\n";

            results += "Maximum Thefts Per Year (1999 To 2004): " + question10Answer + "\n";

            results += "Year Of Highest Number Of Motor Vehicle Thefts: " + question11Answer + "\n";



            newFile = "Output.txt";

            StreamWriter sw = new StreamWriter(newFile);

            try
            {

                sw.WriteLine(results);

            }
            catch (Exception x)
            {
                Console.WriteLine("Exception: " + x.Message);
            }
            finally
            {
                Console.WriteLine("Executing.");

                sw.Close();

            }
        }