using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService = AuthorService.Instance;

        public IActionResult Index()
        {
            var authors = _authorService.GetActiveAuthors();
            return View(authors);
        }

        public IActionResult Archive()
        {
            var archivedAuthors = _authorService.GetArchivedAuthors();
            return View(archivedAuthors);
        }

        public IActionResult Details(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new AddAuthorViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _authorService.AddAuthor(model);
            TempData["Success"] = "Author added successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound();

            var model = new EditAuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                ProfileImageUrl = author.ProfileImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _authorService.UpdateAuthor(model);
            TempData["Success"] = "Author updated successfully!";
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public IActionResult EditModal(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound();

            var model = new EditAuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                ProfileImageUrl = author.ProfileImageUrl
            };

            return PartialView("_EditAuthorPartial", model);
        }

        [HttpPost]
        public IActionResult EditModal(EditAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authorService.UpdateAuthor(model);
            return Ok();
        }

        [HttpGet]
        public IActionResult AddModal()
        {
            return PartialView("_AddAuthorPartial", new AddAuthorViewModel());
        }

        [HttpPost]
        public IActionResult AddModal(AddAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AddAuthorPartial", model);
            }

            _authorService.AddAuthor(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteModal(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return PartialView("_DeleteAuthorPartial", author);
        }

        [HttpPost]
        public IActionResult Archive(Guid id)
        {
            try
            {
                _authorService.ArchiveAuthor(id);
                TempData["Success"] = "Author archived successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Restore(Guid id)
        {
            try
            {
                _authorService.RestoreAuthor(id);
                TempData["Success"] = "Author restored successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Archive");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _authorService.DeleteAuthor(id);
                TempData["Success"] = "Author deleted successfully!";
                return Ok();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return BadRequest(ex.Message);
            }
        }
    }
}
