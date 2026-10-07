using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soccer.Models;

namespace Soccer.Controllers;

// Використання первинного конструктора (Primary Constructor) для впровадження залежностей
public class PlayersController(SoccerContext context) : Controller
{
    // GET: Players
    public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
    {
        var currentYear = DateTime.Now.Year;
        IQueryable<Player> players = context.Players.Include(x => x.Team);

        players = sortOrder switch
        {
            SortState.NameDesc => players.OrderByDescending(s => s.Name),
            SortState.AgeAsc => players.OrderBy(s => currentYear - s.BirthYear),
            SortState.AgeDesc => players.OrderByDescending(s => currentYear - s.BirthYear),
            SortState.PositionAsc => players.OrderBy(s => s.Position),
            SortState.PositionDesc => players.OrderByDescending(s => s.Position),
            SortState.TeamAsc => players.OrderBy(s => s.Team!.Name),
            SortState.TeamDesc => players.OrderByDescending(s => s.Team!.Name),
            _ => players.OrderBy(s => s.Name),
        };

        var viewModel = new IndexViewModel
        {
            Players = await players.ToListAsync(),
            SortViewModel = new SortViewModel(sortOrder)
        };

        return View(viewModel);
    }

    // GET: Players/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null || context.Players is null) return NotFound();

        var player = await context.Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(m => m.Id == id);

        return player is null ? NotFound() : View(player);
    }

    // GET: Players/Create
    public IActionResult Create()
    {
        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name");
        return View();
    }

    // POST: Players/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,BirthYear,Position,TeamId")] Player player)
    {
        if (DateTime.Now.Year - player.BirthYear <= 0)
            ModelState.AddModelError("Age", "Вік повинен бути більшим за нуль");

        if (ModelState.IsValid)
        {
            context.Add(player);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name", player.TeamId);
        return View(player);
    }

    // GET: Players/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null || context.Players is null) return NotFound();

        var player = await context.Players.FindAsync(id);
        if (player is null) return NotFound();

        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name", player.TeamId);
        return View(player);
    }

    // POST: Players/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BirthYear,Position,TeamId")] Player player)
    {
        if (id != player.Id) return NotFound();

        if (DateTime.Now.Year - player.BirthYear <= 0)
            ModelState.AddModelError("Age", "Вік повинен бути більшим за нуль");

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(player);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersExists(player.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name", player.TeamId);
        return View(player);
    }

    // GET: Players/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || context.Players is null) return NotFound();

        var player = await context.Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(m => m.Id == id);

        return player is null ? NotFound() : View(player);
    }

    // POST: Players/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (context.Players is null)
            return Problem("Entity set 'SoccerContext.Players' is null.");

        var player = await context.Players.FindAsync(id);
        if (player is not null)
        {
            context.Players.Remove(player);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlayersExists(int id) => context.Players.Any(e => e.Id == id);
}