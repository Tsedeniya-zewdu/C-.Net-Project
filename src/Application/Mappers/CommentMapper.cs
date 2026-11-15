
using System;
using Application.Dtos.Comment;
using Domain.Entities;

namespace Application.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment CommentModel)
        {
            return new CommentDto
            {
                Id = CommentModel.Id,
                Title = CommentModel.Title,
                Content = CommentModel.Content,
                CreatedOn = CommentModel.CreatedOn,
                 CreatedBy =CommentModel.AppUser.UserName,
                StokeId = CommentModel.StokeId
            };
        }
         public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stokeId)
        {
            return new Comment
            {
                
                Title = commentDto.Title,
                Content = commentDto.Content,
             
                StokeId = stokeId
            };
        }
         public static Comment ToCommentFromUpate(this UpdateCommentRequestDto commentDto)
        {
            return new Comment
            {
                
                Title = commentDto.Title,
                Content = commentDto.Content,
              
            };
        }
    }
}
