using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationApiV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocationApiV2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly locationContext _context;

        public CountryController(locationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Countries>>> Get()
        {
            return await _context.Countries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Countries>> GetCountry(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult> GetbyName(string name)
        {
            return Ok(_context.Countries.Where(m => m.Name == name));
        }

        [HttpGet("getBySortName/{sortname}")]
        public async Task<ActionResult<Countries>> GetBySortName(string sortname)
        {
            return Ok(_context.Countries.Where(m => m.Sortname == sortname));
        }

        [HttpPost]
        public async Task<ActionResult<Countries>> PostCountry(Countries countries)
        {
            _context.Countries.Add(countries);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCountry), new { id = countries.Id}, countries);
        }

    }
}
