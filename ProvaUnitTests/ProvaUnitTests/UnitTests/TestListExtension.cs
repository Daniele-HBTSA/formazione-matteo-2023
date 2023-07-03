using HBTSA.Libraries.ExtensionMethods;
using Xunit;

namespace ProvaUnitTests.UnitTests
{
    public class TestListExtension
    {
        //ContainsAllItems
        [Fact]
        public void Should_Return_True_If_Two_Lists_Contains_All_Items()
        {
            List<int> input = new List<int>()
            {
                1,2,3,4,5
            };

            List<int> inputToCompare = new List<int>()
            {
                1,2,3,4,5
            };

            bool resInput = input.ContainsAllItems<int>(inputToCompare);

            Assert.True(resInput);
        }

        [Fact]
        public void Should_Return_False_If_Two_Lists_Doesnt_Contain_All_Items()
        {
            List<int> input = new List<int>()
            {
                1,2,3,4,5
            };

            List<int> inputToCompare = new List<int>()
            {
                2
            };

            bool resInput = input.ContainsAllItems<int>(inputToCompare);

            Assert.False(resInput);
        }

        //ToFlat List<int>
        [Fact]
        public void Should_Convert_A_Nullable_IntList_Into_A_StringList_Case_ListNotNull()
        {
            List<int> input = new List<int>()
            {
                1, 2, 3, 4, 5
            };

            string expRes = "1,2,3,4,5";

            string resInput = input.ToFlat();

            Assert.Equal(expRes, resInput);
        }
    }
}
