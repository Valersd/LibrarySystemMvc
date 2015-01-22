using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemMvc.Web.ViewModels
{
    public class CategoryBooksViewModel
    {
        public string CategoryName { get; set; }
        public IEnumerable<BookTitleAuthorViewModel> Books { get; set; }
    }
}