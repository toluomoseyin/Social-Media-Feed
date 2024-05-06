using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using SocialMediaApp.Application.Interfaces.Services;

namespace SQSBackgroundService
{
    public class SqsBackgroundService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;



        public SqsBackgroundService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = CreateClient();

            var queueName = _configuration.GetValue<string>("QueueName");
            var queueUrl = await GetQueueUrl(client, queueName);

            await Start(client, queueUrl, stoppingToken);
        }

        private  async Task Start(IAmazonSQS client, string queueUrl, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Starting polling queue");

            var message = $"New test message put on ther queue at {DateTime.Now.ToLongTimeString()}";

            //Send a test message
            await SendMessage(client, queueUrl, message);

            while (!stoppingToken.IsCancellationRequested)
            {
                var messages = await ReceiveMessageAsync(client, queueUrl, 10);

                if (messages.Any())
                {
                    Console.WriteLine($"{messages.Count} messages received");

                    foreach (var msg in messages)
                    {
                        var result =await ProcessMessage(msg);

                        if (result)
                        {
                            Console.WriteLine($"{msg.MessageId} processed with success");
                            await DeleteMessageAsync(client, queueUrl, msg.ReceiptHandle);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No message available");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }

        private static async Task SendMessage(IAmazonSQS client, string queueUrl, string message)
        {
            await client.SendMessageAsync(new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = message
            });
        }

        private  async Task<bool> ProcessMessage(Message msg)
        {
            Console.WriteLine(msg.Body);

            using var scope = _serviceProvider.CreateScope();

            var scopedServices = scope.ServiceProvider;


            var _processPostCreatedQueue = scopedServices.GetRequiredService<IProcessPostCreatedQueue>();

            await _processPostCreatedQueue.Process(msg.Body);

            return true;


        }

        private IAmazonSQS CreateClient()
        {
            //This is a simple scenario, you might want to use DI instead. Please refere to documentation for options

            var accessKey = _configuration.GetValue<string>("AccessKey");
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var region = RegionEndpoint.USEast1;

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            return new AmazonSQSClient(credentials, region);
        }

        private static async Task<string> GetQueueUrl(IAmazonSQS client, string queueName)
        {
            try
            {
                var response = await client.GetQueueUrlAsync(new GetQueueUrlRequest
                {
                    QueueName = queueName
                });

                return response.QueueUrl;
            }
            catch (QueueDoesNotExistException)
            {
                //You might want to add additionale exception handling here because that may fail
                var response = await client.CreateQueueAsync(new CreateQueueRequest
                {
                    QueueName = queueName
                });

                return response.QueueUrl;
            }
        }

        private static async Task<List<Message>> ReceiveMessageAsync(IAmazonSQS client, string queueUrl, int maxMessages = 1)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = maxMessages
            };

            var messages = await client.ReceiveMessageAsync(request);

            return messages.Messages;
        }

        private static async Task DeleteMessageAsync(IAmazonSQS client, string queueUrl, string id)
        {
            var request = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = id
            };

            await client.DeleteMessageAsync(request);
        }
    }
}
