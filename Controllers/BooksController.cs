using Microsoft.AspNetCore.Mvc;
using PBL3_DUTLibrary.Data;
using PBL3_DUTLibrary.Models;
using System.Data.Entity;
using X.PagedList;
using X.PagedList.Extensions;

namespace PBL3_DUTLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext dbLibrary;

        public BooksController(LibraryContext dbLibrary)
        {
            this.dbLibrary = dbLibrary;
        }
        //public IActionResult Index(List<string> loai, int? page)
        public IActionResult Index(List<string> loai, int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var books = dbLibrary.Books.AsQueryable();

            if (loai != null && loai.Any())
            {
                books = books.Where(p => dbLibrary.Genres
                    .Any(g => g.BookId == p.BookId && loai.Contains(g.Genre1)));
            }

            var result = books.Select(p => new Book
            {
                BookId = p.BookId,
                Title = p.Title,
                Image = p.Image,
                Author = p.Author,
                Available = p.Available
            });

            // Áp dụng phân trang
            var pagedBooks = result.ToPagedList(pageNumber, pageSize);

            ViewBag.Loai = loai;
            return View(pagedBooks);
        }

        public IActionResult Search(string? query, List<string> loai, int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var book = dbLibrary.Books.AsQueryable();

            // Debug information
            ViewBag.DebugInfo = $"Query: {query ?? "none"}, Genres: {(loai != null ? string.Join(", ", loai) : "none")}";

            if (loai != null && loai.Any())
            {
                book = book.Where(p => dbLibrary.Genres
                            .Any(g => g.BookId == p.BookId && loai.Contains(g.Genre1)));
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                // Search in both title and author fields
                book = book.Where(p => p.Title.Contains(query) || p.Author.Contains(query));
            }

            var results = book.Select(p => new Book
            {
                BookId = p.BookId,
                Title = p.Title,
                Image = p.Image,
                Author = p.Author,
                Available = p.Available
            });

            var pagedBooks = results.ToPagedList(pageNumber, pageSize);
            ViewBag.Query = query;
            ViewBag.Loai = loai;
            return View(pagedBooks);
        }

        public IActionResult PhanTrang(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var books = dbLibrary.Books
                .OrderBy(b => b.Title)
                .Select(p => new Book
                {
                    BookId = p.BookId,
                    Title = p.Title,
                    Image = p.Image,
                    Author = p.Author,
                    Available = p.Available
                });

            // Áp dụng phân trang
            var pagedBooks = books.ToPagedList(pageNumber, pageSize);
            return View(pagedBooks);
        }

        public IActionResult DetailBooks(int id)
        {
            var data = dbLibrary.Books
                .Include(p => p.Genres)
                .SingleOrDefault(p => p.BookId == id);
            if (data == null)
            {
                //TempData["Message"] = "Không tìm thấy sách";
                return Redirect("/404");
            }

            return View(data);
        }
    }
}
