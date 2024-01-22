using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saggi_timesheet_API.Data;
using Saggi_timesheet_API.Models;

namespace Saggi_timesheet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetEntriesController : ControllerBase
    {
        private readonly SaggiTSDbContext _context;

        public TimesheetEntriesController(SaggiTSDbContext context)
        {
            _context = context;
        }

        // GET: api/TimesheetEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimesheetEntry>>> GetTimesheetEntry()
        {
          if (_context.TimesheetEntry == null)
          {
              return NotFound();
          }
            return await _context.TimesheetEntry.ToListAsync();
        }

        // GET: api/TimesheetEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimesheetEntry>> GetTimesheetEntry(int id)
        {
          if (_context.TimesheetEntry == null)
          {
              return NotFound();
          }
            var timesheetEntry = await _context.TimesheetEntry.FindAsync(id);

            if (timesheetEntry == null)
            {
                return NotFound();
            }

            return timesheetEntry;
        }

        // PUT: api/TimesheetEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimesheetEntry(int id, TimesheetEntry timesheetEntry)
        {
            if (id != timesheetEntry.TimeSheetEntryId)
            {
                return BadRequest();
            }

            _context.Entry(timesheetEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimesheetEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TimesheetEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimesheetEntry>> PostTimesheetEntry(TimesheetEntry timesheetEntry)
        {
            if (_context.TimesheetEntry == null)
            {
                return Problem("Entity set 'SaggiTSDbContext.TimesheetEntry'  is null.");
            }
            _context.TimesheetEntry.Add(timesheetEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimesheetEntry", new { id = timesheetEntry.TimeSheetEntryId }, timesheetEntry);
        }


        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<TimesheetEntry>>> PostTimesheetEntries(IEnumerable<TimesheetEntry> timesheetEntries)
        //{
        //    if (timesheetEntries == null || !timesheetEntries.Any())
        //    {
        //        return BadRequest("No timesheet entries provided.");
        //    }

        //    if (_context.TimesheetEntry == null)
        //    {
        //        return Problem("Entity set 'SaggiTSDbContext.TimesheetEntry' is null.");
        //    }

        //    _context.TimesheetEntry.AddRange(timesheetEntries);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTimesheetEntry", timesheetEntries);
        //}

        // DELETE: api/TimesheetEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimesheetEntry(int id)
        {
            if (_context.TimesheetEntry == null)
            {
                return NotFound();
            }
            var timesheetEntry = await _context.TimesheetEntry.FindAsync(id);
            if (timesheetEntry == null)
            {
                return NotFound();
            }

            _context.TimesheetEntry.Remove(timesheetEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimesheetEntryExists(int id)
        {
            return (_context.TimesheetEntry?.Any(e => e.TimeSheetEntryId == id)).GetValueOrDefault();
        }
    }
}
