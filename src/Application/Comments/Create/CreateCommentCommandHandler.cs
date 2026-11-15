using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.Create
{
    public sealed class CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IStokeRepository stokeRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken ct)
        {
            if (!await stokeRepository.ExistsAsync(request.StokeId, ct))
            {
                throw new NotFoundException($"Stock with id {request.StokeId} was not found.");
            }

            var comment = new Comment
            {
                Title = request.Title,
                Content = request.Content,
                StokeId = request.StokeId,
                AppUserId = request.UserId
            };

            await commentRepository.AddAsync(comment, ct);
            await unitOfWork.SaveChangesAsync(ct);

            // Load the comment again so its AppUser (the author's name) is filled in.
            var created = await commentRepository.GetByIdAsync(comment.Id, ct) ?? comment;
            return created.ToCommentDto();
        }
    }
}
