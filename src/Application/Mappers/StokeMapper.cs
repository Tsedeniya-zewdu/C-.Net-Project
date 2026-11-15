using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Stoke;
using Domain.Entities;

namespace Application.Mappers
{
    public static class StokeMapper
    {
        public static StokeDto ToStokeDto(this Stoke stokemodel)
        {
            return new StokeDto
            {
                StokeId = stokemodel.StokeId,
  Symbol=stokemodel.Symbol,
  CompanyNAme=stokemodel.CompanyName,
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
        CompanyName = createDto.CompanyNAme,
        Purchase = createDto.Purchase,
        LastDiv = createDto.LastDiv,
        Industry = createDto.Industry,
        MarketCap = createDto.MarketCap,

    };
}

        // Copies the update request onto an existing entity (this used to live in the repository).
        public static void UpdateFromDto(this Stoke stoke, UpdateStokeRequestDto dto)
        {
            stoke.Symbol = dto.Symbol;
            stoke.CompanyName = dto.CompanyNAme;
            stoke.Purchase = dto.Purchase;
            stoke.LastDiv = dto.LastDiv;
            stoke.Industry = dto.Industry;
            stoke.MarketCap = dto.MarketCap;
        }
    }
}