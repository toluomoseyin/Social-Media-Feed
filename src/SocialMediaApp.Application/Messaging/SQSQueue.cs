using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;

namespace SocialMediaApp.Application.Messaging
{
    public class SQSQueue : ISQSQueue
    {
        private readonly IConfiguration _configuration;

        public SQSQueue(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private  IAmazonSQS CreateClient()
        {          
            var accessKey = _configuration.GetValue<string>("AccessKey");
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var region = RegionEndpoint.USEast1;

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            return new AmazonSQSClient(credentials, region);
        }

        private async Task<string> GetQueueUrl(IAmazonSQS client)
        {
            var queueName = _configuration.GetValue<string>("QueueName");
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
                var response = await client.CreateQueueAsync(new CreateQueueRequest
                {
                    QueueName = queueName
                });

                return response.QueueUrl;
            }
        }

        public  async Task SendMessage(string message)
        {
            var client = CreateClient();
            var queueUrl = await GetQueueUrl(client);
            await client.SendMessageAsync(new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = message
            });
        }
    }
}
