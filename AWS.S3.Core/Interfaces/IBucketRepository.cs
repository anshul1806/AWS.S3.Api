using AWS.S3.Core.Communication.Bucket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.S3.Core.Interfaces
{
    public interface IBucketRepository
    {
        Task<bool> DoesS3BucketExists(string bucketName);
        Task<CreateBucketResponse> CreateBucket(string bucketName);
        Task<IEnumerable<ListS3BucketResponse>> ListS3Buckets();
    }
}
