using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.S3.Core.Communication.Files
{
    public class AddFileResponse
    {
        public IList<String> PreSignedUrl { get; set; }
    }
}
