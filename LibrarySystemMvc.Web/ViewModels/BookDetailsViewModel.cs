using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibrarySystemMvc.Web.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}