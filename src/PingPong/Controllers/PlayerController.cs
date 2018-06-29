using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PingPong.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingPong.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PingPongContext context;

        public PlayerController(PingPongContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetAll()
        {
            return await context.Players.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetById(int id)
        {
            Player result = await context.Players.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Player>> Create([FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await context.Players.AddAsync(player);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = player.PlayerId }, player);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, [FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Player playerToUpdate = await context.Players.FindAsync(id);
            if (playerToUpdate == null)
            {
                return NotFound();
            }

            context.Players.Update(playerToUpdate).CurrentValues.SetValues(player);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            Player player = await context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            context.Players.Remove(player);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}