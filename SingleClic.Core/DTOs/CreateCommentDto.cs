﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.DTOs
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public int BlogPostId { get; set; }
    }
}
