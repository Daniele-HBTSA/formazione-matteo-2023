using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
                return NotFound(new List<EnterpriseTree>());
            }
        }

        [HttpPost("/select-tree/{fatherId}")]
        public async Task<ActionResult<List<EnterpriseTree>>> SelectWholeTree([FromRoute]int fatherId)
        {
            List<EnterpriseTree> tree = await TreeService.GetEnterpriseData();
            if (tree == null)
            {
                return NotFound("Tree is null");
            }

            try
            {
                return Ok(TreeService.SelectSpecificEnterpriseTree(tree, fatherId));

            } catch (Exception ex)
            {
                return NotFound("Father tree not found");
            }
        }

        [HttpPost("/select-enterprise/{enterpreiseId}")]
        public async Task<ActionResult<List<EnterpriseTree>>> SelectSingleEnterpriseTree([FromRoute] int enterpreiseId)
        {
            List<EnterpriseTree> tree = await TreeService.GetEnterpriseData();
            if (tree == null)
            {
                return NotFound("Tree is null");
            }

            try
            {
                return Ok(TreeService.SelectSingleEnterprise(tree, enterpreiseId));

            }
            catch (Exception ex)
            {
                return NotFound("Father tree not found");
            }
        }

        [HttpPost("/add-child/{fatherId}")]
        public async Task<ActionResult<List<EnterpriseTree>>> AddNewEnterprise([FromBody] EnterpriseTree newChild, [FromRoute] int fatherId)
        {
            try
            {
                return Ok(await TreeService.AddNewEnterprise(newChild, fatherId));

            } catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
