using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Interfaces.Repositories;
using SocialMediaApp.Application.Interfaces.Services;
using SocialMediaApp.Application.Messaging;
using SocialMediaApp.Infrastructure.Persistence.Context;
using SocialMediaApp.Infrastructure.Persistence.Repository;
using SocialMediaApp.Infrastructure.Services;
using SQSBackgroundService;
using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<SqsBackgroundService>();

builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(
               builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
{
    string connectionString = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(connectionString);
});

builder.Services.AddScoped<IFeedRepository, FeedRepository>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFeedService, FeedService>();
builder.Services.AddScoped<ISQSQueue, SQSQueue>();
builder.Services.AddScoped<IRedisRepository, RedisRepository>();
builder.Services.AddScoped<IProcessPostCreatedQueue, ProcessPostCreatedQueue>();

var host = builder.Build();
host.Run();
