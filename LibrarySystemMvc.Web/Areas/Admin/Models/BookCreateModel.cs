using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LibrarySystem.Models;

namespace LibrarySystemMvc.Web.Areas.Admin.Models
{
    public class BookCreateModel
    {

        [Required]
        [StringLength(150)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [StringLength(20)]
        [DataType(DataType.Text)]
        public string ISBN { get; set; }

        [StringLength(100)]
        [DataType(DataType.Url)]
        [RegularExpression(@"^([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+.*)$", ErrorMessage = "Not valid URL")]
        [Display(Name="Web site")]
        public string Url { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int Category { get; set; }
    }
}