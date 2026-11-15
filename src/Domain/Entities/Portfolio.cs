using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Portfolio
    {
        public string AppUserId { get; set; } 
        public int StokeId { get; set; } 
        public AppUser AppUser { get; set; }
        public Stoke Stoke { get; set; } 
    }
}