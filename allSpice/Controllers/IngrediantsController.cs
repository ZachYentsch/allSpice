using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using allSpice.Models;

namespace allSpice.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class IngrediantsController : ControllerBase
    {
        private readonly IngrediantsService _iss;
        public IngrediantsController(IngrediantsService iss)
        {
            _iss = iss;
        }

        // ANCHOR GET ALL
        [HttpGet]
        public ActionResult<List<Ingrediant>> getAll()
        {
            try
            {
                return Ok(_iss.getAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR GET BY ID
        [HttpGet("{id}")]
        public ActionResult<Ingrediant> getById(int id)
        {
            try
            {
                return Ok(_iss.getById(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR CREATE
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Ingrediant>> create([FromBody] Ingrediant newIngrediant)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingrediant createdIngrediant = _iss.create(newIngrediant);
                return Ok(createdIngrediant);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR DELETE
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _iss.remove(id, userInfo.Id);
                return Ok("Ingrediant Gone");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR EDIT
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Ingrediant>> edit([FromBody] Ingrediant ingrediant, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                ingrediant.Id = id;
                return Ok(_iss.edit(ingrediant, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}