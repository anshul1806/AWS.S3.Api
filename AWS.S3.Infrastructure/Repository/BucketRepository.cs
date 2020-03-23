using Amazon.S3;
using Amazon.S3.Model;
using AWS.S3.Core.Communication.Bucket;
using AWS.S3.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.S3.Infrastructure.Repository
{
    public class BucketRepository : IBucketRepository
    {
        private readonly IAmazonS3 _client;
        public BucketRepository(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<CreateBucketResponse> CreateBucket(string bucketName)
        {
            var putbucketRequest = new PutBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = true,
            };
            var resp = await _client.PutBucketAsync(putbucketRequest);
            return new CreateBucketResponse()
            {
                RequestId = resp.ResponseMetadata.RequestId,
                BucketName = bucketName
            };
        }

        public async Task<bool> DoesS3BucketExists(string bucketname) {
            return await _client.DoesS3BucketExistAsync(bucketname);
        }

        public async Task<IEnumerable<ListS3BucketResponse>> ListS3Buckets()
        {
            var response = await _client.ListBucketsAsync();
            return response.Buckets.Select(bkt => new ListS3BucketResponse(){ BucketName = bkt.BucketName, CreatedOn = bkt.CreationDate});
        }
    }
}
