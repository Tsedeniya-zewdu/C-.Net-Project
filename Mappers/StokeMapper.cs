using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stoke;
using Npgsql.Replication;
using api.Models;

namespace api.Mappers
{
    public static class StokeMapper
    {
        public static StokeDto ToStokeDto(this Stoke stokemodel)
        {
            return new StokeDto
            {
                StokeId = stokemodel.StokeId,
  Symbol=stokemodel.Symbol,
  CompanyNAme=stokemodel.CompanyNAme,
  Purchase=stokemodel.Purchase,
  LastDiv=stokemodel.LastDiv,
  Industry=stokemodel.Industry,
  MarketCap=stokemodel.MarketCap,
  Comments=stokemodel.Comments.Select(c=> c.ToCommentDto()).ToList()
            };
        }
       public static Stoke ToStokeFromCreateDto(
    this CreateStokerequestDto createDto)
{
    return new Stoke
    {
        Symbol = createDto.Symbol,
        CompanyNAme = createDto.CompanyNAme,
        Purchase = createDto.Purchase,
        LastDiv = createDto.LastDiv,
        Industry = createDto.Industry,
        MarketCap = createDto.MarketCap,
        
    };
}
    }
}