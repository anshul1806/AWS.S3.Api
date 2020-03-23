using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.S3.Core.Communication.Bucket
{
   public class CreateBucketResponse
    {
        public string RequestId { get; set; }
        public string BucketName { get; set; }
    }
}
