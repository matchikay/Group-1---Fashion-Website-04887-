using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaFlor.Data;
using LaFlor.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LaFlor.Controllers
{
    public class FlowersController : Controller
    {
        private readonly CustomerContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FlowersController(CustomerContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Flowers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flowers.ToListAsync());
        }

        // GET: Flowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flowers = await _context.Flowers
                .FirstOrDefaultAsync(m => m.flower_id == id);
            if (flowers == null)
            {
                return NotFound();
            }

            return View(flowers);
        }

        // GET: Flowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string?name,int? price,string? details,IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {

                        string uniqueFileName = name + "_" + image.FileName;


                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);


                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }


                        Flowers flower = new Flowers
                        {
                            flower_name = name,
                            flower_price = price,
                            flower_details = details,
                            flower_image = "/uploads/" + uniqueFileName,
             
                        };


                        _context.Add(flower);

                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {

                        ModelState.AddModelError("image", "Please select an image file.");
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "An error occurred while saving the apartment information.");

                    return View();
                }
            }

            return View();
        }

        // GET: Flowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flowers = await _context.Flowers.FindAsync(id);
            if (flowers == null)
            {
                return NotFound();
            }
            return View(flowers);
        }

        // POST: Flowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("flower_id,flower_name,flower_price,flower_details,flower_image")] Flowers flowers)
        {
            if (id != flowers.flower_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flowers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowersExists(flowers.flower_id))
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
            return View(flowers);
        }

        // GET: Flowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flowers = await _context.Flowers
                .FirstOrDefaultAsync(m => m.flower_id == id);
            if (flowers == null)
            {
                return NotFound();
            }

            return View(flowers);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var flowers = await _context.Flowers.FindAsync(id);
            if (flowers != null)
            {
                _context.Flowers.Remove(flowers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowersExists(int? id)
        {
            return _context.Flowers.Any(e => e.flower_id == id);
        }
    }
}
