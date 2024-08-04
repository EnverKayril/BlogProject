﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class ArticleWithUserDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public string AppUserId { get; set; }
        public string CategoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
