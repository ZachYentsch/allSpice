using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using allSpice.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using allSpice.Services;

namespace allSpice.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StepsController : ControllerBase
    {
        private readonly StepsController _ss;
        public StepsController(StepsService ss)
        {
            _ss = ss;
        }

        // ANCHOR GET ALL
        [HttpGet]
        public ActionResult<List<Step>> getAll()
        {
            try
            {
                return Ok(_ss.getAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR GET BY ID
        [HttpGet("{id}")]
        public ActionResult<Step> getById(int id)
        {
            try
            {
                Step foundStep = _ss.getById(id);
                return foundStep;
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ANCHOR CREATE
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Step>> create([FromBody] Step newStep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Step createdStep = _ss.create(newStep);
                return Ok(createdStep);
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
                _ss.remove(id, userInfo.Id);
                return Ok("Step Removed");
            }
            catch (System.Exception e)
            {
                return Task.FromResult(BadRequest(e.Message));
            }
        }

        // ANCHOR EDIT
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Step>> edit([FromBody] Step updated, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updated.Id = id;
                return Ok(_ss.edit(updated, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return Task.FromResult(Task.FromResult(BadRequest(e.Message)));
            }
        }
    }
}