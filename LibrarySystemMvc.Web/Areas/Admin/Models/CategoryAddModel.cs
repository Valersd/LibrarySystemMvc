﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibrarySystemMvc.Web.Areas.Admin.Models
{
    public class CategoryAddModel
    {

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}