using System;
using System.Globalization;
using NUnit.Framework;
using MakingWavesRecruitmentAssignment;

namespace MakingWavesRecruitmentAssignmentTests
{
    [TestFixture]
    public class RangeCalculatorProgramTests
    {
        [Test]
        public void HasValidNumberOfArguments_CorrectNumberOfArgs_ReturnsTrue()
        {
            var inputArguments = new string[] {"a", "b" };
            var result = RangeCalculatorProgram.HasValidNumberOfArguments(inputArguments);
            Assert.IsTrue(result);
        }

        [Test]
        public void HasValidNumberOfArguments_CorrectNumberOfEmptyArgs_ReturnsTrue()
        {
            var inputArguments = new string[] { "", "" };
            var result = RangeCalculatorProgram.HasValidNumberOfArguments(inputArguments);
            Assert.IsTrue(result);
        }

        [Test]
        public void HasValidNumberOfArguments_InCorrectNumberOfArgs_ReturnsFalse()
        {
            var inputArguments = new string[] { "a" };
            var result = RangeCalculatorProgram.HasValidNumberOfArguments(inputArguments);
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValidNumberOfArguments_NullArgs_ReturnsFalse()
        {
            string[] inputArguments = null;
            var result = RangeCalculatorProgram.HasValidNumberOfArguments(inputArguments);
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValidNumberOfArguments_CorrectNumberOfDateArgs_ReturnsTrue()
        {
            var inputArguments = new string[] { "01.01.2016", "05.02.2016" };
            var result = RangeCalculatorProgram.HasValidNumberOfArguments(inputArguments);
            Assert.IsTrue(result);
        }

        [TestCase("01.01.2016")]
        [TestCase("25.01.2016")]        
        public void ParseInput_CorrectDateArgs_ReturnsParsedDate(string inputArgument)
        {
            var result = RangeCalculatorProgram.ParseInput(inputArgument);
            Assert.IsInstanceOf<DateTime>(result);
        }

        [TestCase("30.02.2018")]
        [TestCase("01.01.20168")]
        [TestCase("abc")]
        [TestCase("-10.02.2018")]
        [TestCase("10.13.2018")]
        [TestCase("10/11/2018")]
        public void ParseInput_InCorrectDateArgs_ReturnsException(string inputArgument)
        {
             Assert.Throws<FormatException>(() => RangeCalculatorProgram.ParseInput(inputArgument));
        }

        [Test]
        public void CalculateOutputFormat_StartDateGreaterThenEndDate_ErrorMessage()
        {
            var startDate = DateTime.ParseExact("01.02.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact("15.01.2016", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var result = RangeCalculatorProgram.CalculateOutputFormat(startDate, endDate);
            StringAssert.Contains("Please contact support", result);
        }

        [Test]
        public void CalculateOutputFormat_StartDateYearDifferentEndDateYear_DisplayMessage()
        {
            var startDate = DateTime.ParseExact("01.02.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact("15.09.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var result = RangeCalculatorProgram.CalculateOutputFormat(startDate, endDate);
            StringAssert.Contains("01.02.2017-15.09.2018", result);
        }

        [Test]
        public void CalculateOutputFormat_StartDateMonthDifferentEndDateMonth_DisplayMessage()
        {
            var startDate = DateTime.ParseExact("01.02.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact("15.03.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var result = RangeCalculatorProgram.CalculateOutputFormat(startDate, endDate);
            StringAssert.Contains("01.02-15.03.2017", result);
        }

        [Test]
        public void CalculateOutputFormat_StartDateDayDifferentEndDateDay_DisplayMessage()
        {
            var startDate = DateTime.ParseExact("01.02.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact("15.02.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var result = RangeCalculatorProgram.CalculateOutputFormat(startDate, endDate);
            StringAssert.Contains("01-15.02.2017", result);
        }

        [Test]
        public void CalculateOutputFormat_StartDateSameEndDate_DisplayMessage()
        {
            var startDate = DateTime.ParseExact("01.02.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact("01.02.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var result = RangeCalculatorProgram.CalculateOutputFormat(startDate, endDate);
            StringAssert.Contains("01.02.2017", result);
        }
    }
}
