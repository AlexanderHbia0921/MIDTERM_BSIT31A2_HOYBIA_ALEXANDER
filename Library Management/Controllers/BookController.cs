using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var books = BookService.Instance.GetBooks();
            return View(books);
        }

        public IActionResult AddModal()
        {
            return PartialView("_AddBookPartial");
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("AddModal", vm); // Re-render the modal with validation errors
            }

            BookService.Instance.AddBook(vm);

            return RedirectToAction("Index");
        }


        public IActionResult EditModal(Guid id)
        {
            var editBookViewModel = BookService.Instance.GetBookById(id);
            if (editBookViewModel == null)
                return NotFound();

            return PartialView("_EditBookPartial", editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService.Instance.UpdateBook(vm);
            return Ok();
        }

        // DeleteModal and Delete
        public IActionResult DeleteModal(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            return PartialView("_DeleteBookPartial", book); // updated partial name
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            BookService.Instance.DeleteBook(id);
            return Ok(); // You can return a redirect if not using AJAX
        }

        public IActionResult Details(Guid id)
        {
            var book = BookService.Instance.GetBooks().FirstOrDefault(b => b.BookId == id);
            if (book == null)
                return NotFound();

            // Get all book copies for this book
            ViewBag.BookCopies = BookService.Instance.GetBookCopiesByBookId(id);
            
            return View(book);
        }
        private readonly BookService _bookService = BookService.Instance;
        // Show the Add Copy form
        [HttpGet]
        public IActionResult AddCopy(Guid bookId)
        {
            var vm = new AddCopyViewModel
            {
                BookId = bookId
            };
            return View(vm);
        }

        // Handle form submission
        [HttpPost]
        public IActionResult AddCopy(AddCopyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _bookService.AddBookCopy(vm);
            return RedirectToAction("Details", new { id = vm.BookId }); // Redirect to book details
        }

        [HttpGet]
        public IActionResult PulloutModal(Guid copyId)
        {
            var bookCopies = BookService.Instance.GetBookCopiesByBookId(Guid.Empty); // We need to find the copy first
            var bookCopy = _bookService.GetAllBookCopies().FirstOrDefault(bc => bc.Id == copyId);
            if (bookCopy == null)
                return NotFound();

            var model = new PulloutBookCopyViewModel
            {
                BookCopyId = copyId,
                BookTitle = bookCopy.Book?.Title,
                CoverImageUrl = bookCopy.CoverImageUrl,
                Condition = bookCopy.Condition
            };

            return PartialView("_PulloutBookCopyPartial", model);
        }

        [HttpPost]
        public IActionResult PulloutCopy(PulloutBookCopyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookService.PulloutBookCopy(model.BookCopyId, model.PulloutReason);
            TempData["Success"] = "Book copy pulled out successfully!";
            return Ok();
        }

        // Archive book
        [HttpPost]
        public IActionResult Archive(Guid id)
        {
            try
            {
                _bookService.ArchiveBook(id);
                TempData["Success"] = "Book archived successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        // Restore book
        [HttpPost]
        public IActionResult Restore(Guid id)
        {
            try
            {
                _bookService.RestoreBook(id);
                TempData["Success"] = "Book restored successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Archive");
        }

        public IActionResult Archive()
        {
            var archivedBooks = _bookService.GetArchivedBooks();
            return View(archivedBooks);
        }

    }
}
