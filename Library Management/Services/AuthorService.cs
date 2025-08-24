using Library_Management.Models;
using Library_Management_Domain.Entities;

namespace Library_Management.Services
{
    public class AuthorService
    {
        private readonly BookService _bookService;

        public AuthorService()
        {
            _bookService = BookService.Instance;
        }

        public IEnumerable<AuthorListViewModel> GetActiveAuthors()
        {
            return _bookService.GetAllAuthors()
                .Where(a => !a.IsArchived)
                .Select(a => new AuthorListViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                    BirthDate = a.BirthDate,
                    ProfileImageUrl = a.ProfileImageUrl,
                    BookCount = a.Books.Count(b => !b.IsArchived),
                    IsArchived = a.IsArchived
                });
        }

        public IEnumerable<AuthorListViewModel> GetArchivedAuthors()
        {
            return _bookService.GetAllAuthors()
                .Where(a => a.IsArchived)
                .Select(a => new AuthorListViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                    BirthDate = a.BirthDate,
                    ProfileImageUrl = a.ProfileImageUrl,
                    BookCount = a.Books.Count,
                    IsArchived = a.IsArchived
                });
        }

        public AuthorDetailsViewModel? GetAuthorById(Guid id)
        {
            var author = _bookService.GetAllAuthors().FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            var books = _bookService.GetBooks()
                .Where(b => b.AuthorName == author.Name)
                .ToList();

            return new AuthorDetailsViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                ProfileImageUrl = author.ProfileImageUrl,
                IsArchived = author.IsArchived,
                Books = books
            };
        }

        public void AddAuthor(AddAuthorViewModel authorViewModel)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = authorViewModel.Name,
                Biography = authorViewModel.Biography,
                BirthDate = authorViewModel.BirthDate,
                ProfileImageUrl = authorViewModel.ProfileImageUrl,
                Books = new List<Book>()
            };

            _bookService.AddAuthor(author);
        }

        public void UpdateAuthor(EditAuthorViewModel authorViewModel)
        {
            _bookService.UpdateAuthor(authorViewModel);
        }

        public void ArchiveAuthor(Guid id)
        {
            _bookService.ArchiveAuthor(id);
        }

        public void RestoreAuthor(Guid id)
        {
            _bookService.RestoreAuthor(id);
        }

        public void DeleteAuthor(Guid id)
        {
            // Check if author has books
            var author = _bookService.GetAllAuthors().FirstOrDefault(a => a.Id == id);
            if (author?.Books.Any() == true)
            {
                throw new InvalidOperationException("Cannot delete an author who has books. Archive the author instead or remove all books first.");
            }

            _bookService.DeleteAuthor(id);
        }

        // Singleton pattern
        private static AuthorService? _instance;
        public static AuthorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AuthorService();
                }
                return _instance;
            }
        }
    }
}
