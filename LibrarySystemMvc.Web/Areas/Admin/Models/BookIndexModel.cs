using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibrarySystemMvc.Web.Areas.Admin.Models
{
    public class BookIndexModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [UIHint("_20SymbolsOnly")]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [UIHint("_20SymbolsOnly")]
        public string Author { get; set; }

        [StringLength(20)]
        [UIHint("_20SymbolsOnly")]
        public string ISBN { get; set; }

        [StringLength(100)]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        [Display(Name="Category")]
        [UIHint("_20SymbolsOnly")]
        public string CategoryName { get; set; }
    }
}