using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Infrastructure.Helpers;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Projects
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            string cause,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var projects = from p in _context.Projects.Include(p => p.Cause).Include(p => p.Manager) select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(p => p.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(cause))
            {
                projects = projects.Where(p => p.Cause!.Slug == cause);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    projects = projects.OrderByDescending(p => p.Name);
                    break;
                case "Date":
                    projects = projects.OrderBy(p => p.Created);
                    break;
                case "date_desc":
                    projects = projects.OrderByDescending(p => p.Created);
                    break;
                default:
                    projects = projects.OrderBy(p => p.Name);
                    break;
            }
            int pageSize = 12;
            return View(await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Cause)
                .Include(p => p.Proposal)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            ViewData["CauseId"] = new SelectList(_context.Causes, "Id", "Name");

            return View(project);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhotoUpload,CauseId,CityId")] Project project)
        {
            if (ModelState.IsValid)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/projects");
                string photoName = Guid.NewGuid().ToString() + "_" + project.PhotoUpload!.FileName;
                string photoFilePath = Path.Combine(uploadsDir, photoName);
                FileStream fs = new FileStream(photoFilePath, FileMode.Create);
                await project.PhotoUpload.CopyToAsync(fs);
                fs.Close();
                project.Photo = photoName;
                project.Slug = FriendlyUrlHelper.GetFriendlyTitle(project.Name);
                project.ManagerId = HttpContext.User.FindFirst("UserId")!.Value;
                _context.Add(project);
                await _context.SaveChangesAsync();

                _context.Proposals.Add(new Proposal { ProjectId = project.Id });
                _context.Journeys.Add(new Journey { ProjectId = project.Id });
                _context.ProjectSupporters.Add(new ProjectSupporter
                {
                    ProjectId = project.Id,
                    SupporterId = project.ManagerId,
                    ProjectRole = "Manager",
                    Position = "Project Manager"
                });
                _context.SaveChanges();

                return RedirectToAction(nameof(MembersController.Projects), "Members", new { id = HttpContext.User.FindFirst("UserId")!.Value });
            }
            ViewData["CauseId"] = new SelectList(_context.Causes, "Id", "Id", project.CauseId);
            return View(project);
        }

        [Route("{id}/Edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["CauseId"] = new SelectList(_context.Causes, "Id", "Id", project.CauseId);
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Id", project.ManagerId);
            return View(project);
        }

        [Route("{id}/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Slug,Name,Photo,Closed,Published,CauseId,ManagerId")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CauseId"] = new SelectList(_context.Causes, "Id", "Id", project.CauseId);
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Id", project.ManagerId);
            return View(project);
        }

        [Route("{id}/Delete")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [Route("{id}/Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("{id}/Expenditures")]
        public async Task<ActionResult> Expenditures(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Expenditures)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("{id}/Positions")]
        public async Task<ActionResult> Positions(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Positions)
                    .ThenInclude(p => p.Applicants)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("{id}/Journey")]
        public async Task<ActionResult> Journey(long? id)
        {
            if (id == null || _context.Journeys == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .Include(p => p.Journey)
                    .ThenInclude(p => p!.Scenes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            
            ViewData["JourneyId"] = project.Journey!.Id;
            return View(project);
        }

        [Route("{id}/Events")]
        public async Task<ActionResult> Events(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .Include(p => p.Events)
                    .ThenInclude(e => e.Invitations)
                        .ThenInclude(i => i.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        private bool ProjectExists(long id)
        {
          return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
