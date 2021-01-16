using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Resolution { get; set; }
        public int Size { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
