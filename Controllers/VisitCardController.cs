using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VisitCardEfCore.Data;
using VisitCardEfCore.Models;
using VisitCardEfCore.ViewModel;

namespace VisitCardEfCore.Controllers
{
    [ApiController]
    [Route("v1")]
    public class VisitCardController : ControllerBase

    {
        /**
     * Recebe a requisição e devolve pra tela
     */
        [HttpGet]
        [Route("visitcards")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var visitCards = await context
                .VisitCards
                .AsNoTracking()
                .ToListAsync();
            return Ok(visitCards);
        }
        [HttpGet]
        [Route("visitcards/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var visitCard = await context
                .VisitCards
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == id);
            return visitCard == null 
                ? NotFound() 
                : Ok(visitCard);
        }

        [HttpPost]
        [Route("visitcards")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody]CreateVisitCardViewModel modelVisitCard)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var visitCard = new VisitCard
            {
                Name = modelVisitCard.Name,
                PhoneNumber = modelVisitCard.PhoneNumber,
                Email = modelVisitCard.Email
            };

            try
            {
                await context.VisitCards.AddAsync(visitCard);
                await context.SaveChangesAsync();
                return Created($"v1/visitcards/{visitCard.Id}", visitCard);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
            
        }

        // UPDATE
        [HttpPut("visitcards/{id}")]
        public async Task<IActionResult> PutVisitCard(
            [FromServices] AppDbContext context,
            [FromRoute]int id, 
            [FromBody]CreateVisitCardViewModel modelVisitCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var visitCard = await context.VisitCards.FirstOrDefaultAsync(x => x.Id == id);

            if (visitCard == null)
                return NotFound();
            try
            {
                visitCard.Name = modelVisitCard.Name;
                visitCard.PhoneNumber = modelVisitCard.PhoneNumber;
                visitCard.Email = modelVisitCard.Email;

                context.VisitCards.Update(visitCard);
                await context.SaveChangesAsync();
                return Ok(visitCard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
                throw;
            }
            
        }

        // DELETE
        [HttpDelete("visitcards/{id}")]
        public async Task<ActionResult<VisitCard>> DeleteVisitCard(
            [FromServices] AppDbContext context,
            [FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var visitCard = await context.VisitCards.FirstOrDefaultAsync(x => x.Id == id);

            if (visitCard == null)
                return NotFound();
            try
            {

                context.VisitCards.Remove(visitCard);
                await context.SaveChangesAsync();
                return Ok(visitCard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
                throw;
            }
        }



    }
}