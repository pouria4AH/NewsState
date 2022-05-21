using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsState.DataLayer.Dtos
{
    public class ReadPostDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int ReadTime { get; set; }
        public string ImageName { get; set; }
        public string PostText { get; set; }
        public string Writer { get; set; }
        public string Date { get; set; }

    }
}
