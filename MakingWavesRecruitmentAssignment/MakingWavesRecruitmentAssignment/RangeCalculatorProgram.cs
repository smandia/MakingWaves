using System;
using System.Globalization;

namespace MakingWavesRecruitmentAssignment
{
   public class RangeCalculatorProgram
    {        
        static void Main(string[] args)
        {
            if (!HasValidNumberOfArguments(args))
            {
                Console.WriteLine("Invalid number of input arguments");
                return;
            }

            try
            {   
                Console.WriteLine(CalculateOutputFormat(ParseInput(args[0]), ParseInput(args[1])));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Incorrect input format - {0}",ex);
            }            
        }

        public static bool HasValidNumberOfArguments(string[] args)
        {
            return args != null && args.Length == 2;
        }

        public static DateTime ParseInput(string dateArgument)
        {
            return DateTime.ParseExact(dateArgument, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        public static string CalculateOutputFormat(DateTime startDate, DateTime endDate)
        {
            var differentYearMsg = string.Format("{0}-{1}", startDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture), endDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            var differentMonthSameYearMsg = string.Format("{0}.{1}-{2}", startDate.Day.ToString("00"), startDate.Month.ToString("00"), endDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            var differentDaySameMonthYearMsg = string.Format("{0}-{1}", startDate.Day.ToString("00"), endDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            var sameDateMsg = string.Format(endDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            var contactSupportMsg = string.Format("Please contact support for such input {0}, {1}", startDate, endDate);

            if(startDate > endDate)
            {
                return contactSupportMsg;
            }

            return startDate.Year != endDate.Year ? differentYearMsg
                    : startDate.Month != endDate.Month ? differentMonthSameYearMsg
                    : startDate.Day != endDate.Day ? differentDaySameMonthYearMsg
                    : DateTime.Compare(startDate, endDate) == 0 ? sameDateMsg
                    : contactSupportMsg;
        }        
    }
}
