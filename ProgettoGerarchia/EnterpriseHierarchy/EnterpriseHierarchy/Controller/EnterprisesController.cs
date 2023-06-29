using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseHierarchy.Controller
{
    public class EnterprisesController : ControllerBase
    {
        public IEnterpriseTreeService TreeService { get; set; }

        public EnterprisesController(IEnterpriseTreeService enterpriseTree)
        {
            TreeService = enterpriseTree;
        }

        [HttpGet("/get-tree")]
        public async Task<ActionResult<List<EnterpriseTree>>> GetEnterpriseTree()
        {
            try
            {
                return Ok(await TreeService.GetEnterpriseData());

            } catch (Exception ex)
            {
                return NotFound("Tree is null");
            }
        }
    }
}
