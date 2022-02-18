using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using allSpice.Models;
using allSpice.Services;

namespace allSpice.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _rs;
        public RecipesController(RecipesService rs)
        {
            _rs = rs;
        }

        // ANCHOR GET ALL RECIPES
        [HttpGet]
        public ActionResult<List<Recipe>> getAll()
        {
            try
            {
                return Ok(_rs.getAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR GET RECIPE BY ID
        [HttpGet("{id}")]
        public ActionResult<Recipe> getRecipeById(int id)
        {
            try
            {
                return Ok(_rs.getRecipeById(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR Create RECIPE
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Recipe>> create([FromBody] Recipe newRecipe)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newRecipe.CreatorId = userInfo.Id;
                Recipe createdRecipe = _rs.create(newRecipe);
                createdRecipe.Chef = userInfo;
                return Ok(createdRecipe);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _rs.remove(id, userInfo.Id);
                return Ok("Recipe Deleted");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> edit([FromBody] Recipe recipe, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipe.Id = id;
                return Ok(_rs.edit(recipe, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}