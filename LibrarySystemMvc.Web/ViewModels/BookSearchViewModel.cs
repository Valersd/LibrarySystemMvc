using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemMvc.Web.ViewModels
{
    public class BookSearchViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}