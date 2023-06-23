using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Services.Implementations;
using EnterpriseHierarchy.Services.Interfaces;
using Xunit;

namespace TestsHierarchy
{
    public class TestCreateTreeStructure
    {
        //CreateTreeStruct
        [Fact]
        public void Should_Return_New_TreeStruct_With_Provided_FatherDTO()
        {
            EnterpriseDTO paramFather = new EnterpriseDTO()
            {
                IdEnterprise = 1,
                EnterpriseCode = "A001",
                EnterpriseName = "Test1",
                EnterpriseAddress = "Test1",
                ParentIDs = new[] { 2, 3 }
            };

            EnterpriseTree expRes = new EnterpriseTree(paramFather);

            EnterpriseTree actualRes = ITreeService.

        }




    }
}
