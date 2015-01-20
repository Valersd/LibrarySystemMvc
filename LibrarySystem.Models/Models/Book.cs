using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    [MetadataType(typeof(BookMetadata))]
    public partial class Book
    {
    }

    public class BookMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Author { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        [StringLength(100)]
        public string Url { get; set; }

        public string Description { get; set; }
    }
}
