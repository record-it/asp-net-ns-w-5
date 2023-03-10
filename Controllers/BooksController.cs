using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wykład_4.Migrations;
using wykład_4.Models;

namespace wykład_4.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,EditionYear,Created")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,EditionYear,Created")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'AppDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public string GetDetails([FromQuery] int id)
        {
            //Book? book = _context.Books.Include(book => book.BookDetails).FirstOrDefault(b => b.Id == id);
            Book? book = _context.Books.Find(id); 
            if (book is not null)
            {
                _context.Entry<Book>(book)
                .Navigation("BookDetails")            
                .Load();
                return book?.BookDetails?.Description??"No details";
            } else
            {
                return "Book not found";
            }
        }

        public string GetAuthors([FromQuery] int id)
        {
            Book? book = _context.Books.Find(id);
            if (book is not null)
            {
                _context.Entry(book)
                    .Collection(a => a.Authors)
                    .Load();
                string authorsAsString = string.Join(", ", book.Authors);
                return authorsAsString.Length == 0 ? "No Authors": authorsAsString;
            }
            return "Book not found";

        }

        public string AddPublisher([FromQuery] int bookId, [FromQuery] int publisherId)
        {
            Book? book = _context.Books.Find(bookId);
            var pub = _context.Publishers.Find(publisherId);
            if (book is not null && pub is not null)
            {
                book.Publisher = pub;
                _context.SaveChanges();
                return "Publisher added to book";
            }
            return "Fail, book or publisher not found";
        }

        public string AddAuthor([FromQuery] int bookId, [FromQuery] int authorId)
        {
            Book? book = _context.Books.Find(bookId);
            var author = _context.Authors.Find(authorId);
            if (book is not null && author is not null)
            {
                book.Authors.Add(author);
                _context.SaveChanges();
                return "Author added to book";
            }
            return "Fail, book or author not found";
        }
        private bool BookExists(int id)
        {
          return _context.Books.Any(e => e.Id == id);
        }
    }
}
