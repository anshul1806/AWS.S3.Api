using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.S3.Core.Communication.Bucket
{
    public class ListS3BucketResponse
    {
        public String BucketName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
