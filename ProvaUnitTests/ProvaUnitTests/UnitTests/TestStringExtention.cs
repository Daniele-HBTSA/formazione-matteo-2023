using HBTSA.Libraries.ExtensionMethods;
using System.Runtime.CompilerServices;
using Xunit;

namespace ProvaUnitTests.UnitTests
{
    public class TestStringExtention
    {
        //ConvertNullToEmpty
        [Fact]
        public void Should_Return_Empty_String_From_Null_String()
        {
            //Arrange
            string? input = null;
            string expRes = String.Empty;

            //Act
            string resInput = input.ConvertNullToEmpty();

            //Assert
            Assert.Equal(expRes, resInput);
        }

        //FirstCharToUpper
        [Fact]
        public void Should_Return_First_Char_To_Uppercase()
        {
            //Arrange
            string input = "pippo";
            string expRes = "Pippo";

            //Act
            string resInput = input.FirstCharToUpper();

            //Assert
            Assert.Equal(expRes, resInput);
        }

        [Fact]
        public void Should_Return_Empty_String_If_Input_Is_Null()
        {
            //Arrange
            string? input = null;
            string expRes = String.Empty;

            //Act
            string resInput = input.FirstCharToUpper();

            //Assert
            Assert.True(resInput == expRes);
        }

        //TrimTolower
        [Fact]
        public void Should_Trim_String_And_Return_It_To_Lowercase()
        {
            //Arrange
            string input = " PippO e PlUTo ";
            string expRes = "pippo e pluto";

            //Act
            string resInput = input.TrimTolower();

            //Assert
            Assert.Equal(expRes , resInput);
        }

        //ContainsIgnoreCase
        [Fact]
        public void Should_Return_True_If_Element_Is_Contained_In_Input_List_String_Ignoring_Cases()
        {
            //Arrange
            string element = " GIAnni ";
            List<string> strings = new List<string>();
            strings.Add("piNO");
            strings.Add("gIanNI");
            strings.Add("FrgNAno");
            strings.Add("aCMenTule");
            
            //Act
            bool res = strings.ContainsIgnoreCase(element);

            //Assert
            Assert.True(res);
        }

        [Fact]
        public void Should_Return_True_If_Element_Is_Contained_In_Input_String_Ignoring_Cases()
        {
            //Arrange
            string element = " GIAnni ";
            string input = "giaNNI";

            //Act
            bool res = input.ContainsIgnoreCase(element);

            //Assert
            Assert.True(res);
        }

        //EqualsIgnoreCase
        [Fact]
        public void Should_Return_True_If_Two_Strings_Are_Equal_Ignoring_Cases_And_Trimming_Spaces_Around()
        {
            string input = "piNOtto";
            string inputToCompare = " Pinotto";

            bool resInput = input.EqualsIgnoreCase(inputToCompare);

            Assert.True(resInput);
        }

        //StartsWithIgnoreCase
        [Fact]
        public void Should_Return_True_If_Two_Strings_Begins_With_The_Same_Word_Ignoring_Cases_And_Trimming_Spaces_Around()
        {
            string input = " Gianni e piNoTTO";
            string inputToCompare = "gianNI e pInoTTo";

            bool resInput = input.StartsWithIgnoreCase(inputToCompare); 

            Assert.True(resInput);
        }

        //Truncate
        [Fact]
        public void Should_Return_Empty_String_If_Input_Is_Empty()
        {
            //Arrange
            string input = "";
            int maxLength = 5;
            string expRes = String.Empty;

            //Act
            string resInput = input.Truncate(maxLength);

            //Assert
            Assert.True(resInput == expRes);
        }

        [Fact]
        public void Should_Return_New_String_With_Blank_Spaces_Before_String_If_Its_Length_Is_Less_Than_N()
        {
            //Arrange
            string input = "Sghifrizzi";
            int n = 15;
            string expRes = "     Sghifrizzi   ";

            //Act
            string resInput = input.Truncate(n);

            //Assert
            Assert.Equal(expRes, resInput);
        }

        [Fact]
        public void Should_Return_New_String_With_Dots_After_The_String_If_Its_Length_Is_Greater_Than_N()
        {
            //Arrange
            string input = "Pippo";
            int n = 3;
            string expRes = "Pip...";

            //Act
            string resInput = input.Truncate(n);

            //Assert
            Assert.Equal(expRes, resInput);
        }

        //Convert
        [Fact]
        public void Should_Convert_ValueType_With_ParamType()
        {
            string actualType = "2020-10-10";
            Type targetType = typeof(DateTime);
            DateTime expType = new DateTime(2020, 10, 10);

            DateTime resValueType = (DateTime)actualType.Convert(targetType);

            Assert.Equal(expType, resValueType);
        }

        //ComposeQueryStringUrl<T> troppo incasinato

        //Similarity
        [Fact]
        public void Should_Return_A_Value_Between_0_And_1_If_Strings_Are_Similar()
        {
            string input = "agatanga";
            string inputToCompare = "agatonga";
            decimal expRes = 0.875M;

            decimal resInput = input.Similarity(inputToCompare);

            Assert.Equal(expRes, resInput);
        }
    }
}
