using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Comment;

namespace Application.Dtos.Stoke
{
    public class StokeDto
    {
   public int StokeId { get; set; }
        public string Symbol { get; set; } =string.Empty;
        [Required]
        public string CompanyNAme { get; set; }=string.Empty;

        
        public decimal Purchase { get; set; }

[Required]
[Range(0.01,100)]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; }=string.Empty;
        public long MarketCap { get; set; } 
        public List<CommentDto> Comments{get; set;} =new List<CommentDto>();
    }
}