﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Genres
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}