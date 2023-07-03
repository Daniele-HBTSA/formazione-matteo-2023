using Xunit;

namespace ProvaUnitTests.UnitTests
{
    public class TestOperazioni
    {
        [Fact]
        public void testCalcFatt()
        {
            //arrange
            int n = 5;
            int resAtteso = 120;

            //act
            int resTest = new Operazioni().calcolaFattoriale(n);

            //assert
            Assert.Equal(resAtteso, resTest);
        }
    }
}
