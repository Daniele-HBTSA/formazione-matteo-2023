using HBTSA.Libraries.ExtensionMethods;
using Xunit;

namespace ProvaUnitTests.UnitTests
{
    public class TestDateTimeExtension
    {
        //FirstDayMonthCurrentDate
        [Fact]
        public void Should_Return_First_Day_Month_Of_Current_Date()
        {
            DateTime currentDate = new DateTime(2020, 10, 10);
            DateTime expDate = new DateTime(2020, 10, 01);

            DateTime resDate = currentDate.FirstDayMonthCurrentDate();

            Assert.Equal(expDate, resDate);
        }

        //LastDayMonthCurrentDate
        [Fact]
        public void Should_Return_Last_Day_Month_Of_Current_Date()
        {
            DateTime currentDate = new DateTime(2020, 10, 10);
            DateTime expDate = new DateTime(2020, 10, 31);

            DateTime resDate = currentDate.LastDayMonthCurrentDate();

            Assert.Equal(expDate, resDate);
        }

        [Fact]
        public void Should_Return_Last_Day_Bissextile_Month_Date()
        {
            DateTime currentDate = new DateTime(2020, 02, 10);
            DateTime expDate = new DateTime(2020, 02, 29);

            DateTime resDate = currentDate.LastDayMonthCurrentDate();

            Assert.Equal(expDate, resDate);
        }

        [Fact]
        public void Should_Return_Last_Day_NotBissextile_Month_Date()
        {
            DateTime currentDate = new DateTime(2023, 02, 10);
            DateTime expDate = new DateTime(2023, 02, 28);
            
            DateTime resDate = currentDate.LastDayMonthCurrentDate();

            Assert.Equal(expDate, resDate);
        }

        [Fact]
        public void Should_Return_LastDay_Month_With_TimeSpan_Set_To_Last_Second_Of_LastDay()
        {
            DateTime actualDate = new DateTime(2020, 10, 10, 00, 00, 00);
            DateTime expDate = new DateTime(2020, 10, 31, 23, 59, 59);

            DateTime resDate = actualDate.LastDayMonthCurrentDate(true);

            Assert.Equal(expDate, resDate);
        }

        //AddMonthsSetLastDay
        [Fact]
        public void Should_Return_NewDateTime_Plus_NumberOfMonths_With_A_Set_LastDay()
        {
            DateTime actualDate = new DateTime(2020, 01, 10);
            int lastDay = 31;
            int numberOfMonths = 9;
            DateTime expDate = new DateTime(2020, 10, 31);

            DateTime resDate = actualDate.AddMonthsSetLastDay(numberOfMonths, lastDay);

            Assert.Equal(expDate, resDate); 
        }

        [Fact]
        public void Should_Return_NewDateTime_Plus_NumberOfMonths_With_A_Set_LastDay_Next_Year()
        {
            DateTime actualDate = new DateTime(2020, 10, 10);
            int lastDay = 31;
            int numberOfMonths = 3;
            DateTime expDate = new DateTime(2021, 01, 31);

            DateTime resDate = actualDate.AddMonthsSetLastDay(numberOfMonths, lastDay);

            Assert.Equal(expDate, resDate);
        }

        //ToYesterdayAtMidnight
        [Fact]
        public void Should_Return_Yesterday_With_Midnight_TimeSpan()
        {
            DateTime actualDate = new DateTime(2020, 10, 10);
            DateTime expDate = new DateTime(2020, 10, 09, 23, 59, 59);

            DateTime resDate = actualDate.ToYesterdayAtMidnight();

            Assert.Equal(expDate, resDate);
        }

        //ToDayAtMidnight
        [Fact]
        public void Should_Return_ToDay_With_Midnight_TimeSpan()
        {
            DateTime actualDate = new DateTime(2020, 10, 10);
            DateTime expDate = new DateTime(2020, 10, 10, 23, 59, 59);

            DateTime resDate = actualDate.ToDayAtMidnight(true);

            Assert.Equal(expDate, resDate);
        }

        //GetStartEndOfMonth
        [Fact]
        public void Should_Return_NewDateTime_SetTo_FirstDay_And_NewDateTime_SetTo_LastDay_Month()
        {
            DateTime actualDate = new DateTime(2020, 10, 10);
            (DateTime expDate1, DateTime expDate2) = (new DateTime(2020, 10, 01, 00, 00, 00), new DateTime(2020, 10, 31, 23, 59, 59));

            (DateTime resDate1, DateTime resDate2) = actualDate.GetStartEndOfMonth();
        
            Assert.Equal((expDate1, expDate2), (resDate1, resDate2));
        }

        //GetStartEndOfMonthString
        [Fact]
        public void Should_Return_Two_StringFormat_From_Single_DateTime()
        {
            DateTime actualDate = new DateTime(2020,10,10);
            (string expDate1, string expDate2) = ("2020-10-1", "2020-10-31");

            (string resDate1, string resDate2) = actualDate.GetStartEndOfMonthString();

            Assert.Equal((expDate1, expDate2), (resDate1, resDate2));
        }

        //MonthsDifference
        [Fact]
        public void Should_Return_Months_Difference_As_Int()
        {
            DateTime actualDate = new DateTime(2020, 01, 10);
            DateTime secondDate = new DateTime(2020, 11, 10);
            int expMonthDifference = 10;

            int resMonthDifference = actualDate.MonthsDifference(secondDate); 

            Assert.Equal(expMonthDifference, resMonthDifference);
        }

        [Fact]
        public void Should_Return_Months_Difference_As_Int_Between_Different_Years()
        {
            DateTime actualDate = new DateTime(2020, 01, 10);
            DateTime secondDate = new DateTime(2022, 01, 10);
            int expMonthDifference = 24;

            int resMonthDifference = actualDate.MonthsDifference(secondDate);

            Assert.Equal(expMonthDifference, resMonthDifference);
        }
    }
}
