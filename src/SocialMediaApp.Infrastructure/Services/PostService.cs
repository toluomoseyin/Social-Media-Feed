using Newtonsoft.Json;
using SocialMediaApp.Application.DTOs;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Application.Messaging;
using SocialMediaApp.Model.Entities;

namespace SocialMediaApp.Infrastructure.Services
{
    public class PostService:IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ISQSQueue _sQSQueue;

        public PostService(IPostRepository postRepository, ISQSQueue sQSQueue)
        {
            _postRepository = postRepository;
            _sQSQueue = sQSQueue;
        }


        public async Task<(BaseResponse<string>, int)> CreatePost(AddPostDTO addPostDTO, int userId)
        {
            var newPost = new Post
            {
                Content = addPostDTO.Content,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _postRepository.Add(newPost);

            if (userId < 1)
            {
                return (BaseResponse<string>.Failure("An error occurred while creating post"), 400);
            }

            var message = JsonConvert.SerializeObject(newPost);

            await _sQSQueue.SendMessage(message);

            return (BaseResponse<string>.Success("Post successfully created"), 200);


        }
    }
}
