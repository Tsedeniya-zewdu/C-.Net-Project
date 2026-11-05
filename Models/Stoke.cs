using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stoke
    {
        public int StokeId { get; set; }
        public string Symbol { get; set; } =string.Empty;
        public string CompanyNAme { get; set; }=string.Empty;

        [Column(TypeName = "numeric(18,2)")]
        public decimal Purchase { get; set; }

 [Column(TypeName = "numeric(18,2)")]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; }=string.Empty;
        public long MarketCap { get; set; }
        public List<Comment> Comments { get; set; } =new List<Comment>();

    }
}