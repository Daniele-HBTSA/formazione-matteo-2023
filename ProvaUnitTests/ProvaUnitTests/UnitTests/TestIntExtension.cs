using HBTSA.Libraries.ExtensionMethods;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ProvaUnitTests.UnitTests
{
    public class TestIntExtension
    {
        [Fact]
        public void Should_Convert_ExcelDateFormatNumber_Into_DateTime()
        {
            //Arrange
            long excelDateFormatNumber = 42144; //Numbero di giorni che usa Excel per calcolare le date
            DateTime expDate = new DateTime(2015, 05, 20);

            //Act
            DateTime resDate = excelDateFormatNumber.ToDateFromExcelFormatNumber();

            //Assert
            Assert.Equal(expDate, resDate);
        }

        [Fact]
        public void Should_Convert_ExcelDateFormatNumber_Into_DateTime_Plus_One_day()
        {
            //Arrange
            long excelDateFormatNumber = 42145;
            DateTime expDate = new DateTime(2015, 05, 21);

            //Act
            DateTime resDate = excelDateFormatNumber.ToDateFromExcelFormatNumber();

            //Assert
            Assert.Equal(expDate, resDate);
        }
    }
}
