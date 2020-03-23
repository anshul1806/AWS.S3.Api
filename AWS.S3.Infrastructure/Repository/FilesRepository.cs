using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AWS.S3.Core.Communication.Files;
using AWS.S3.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.S3.Infrastructure.Repository
{
    public class FilesRepository : IFilesRepository
    {
        private readonly IAmazonS3 _client;
        public FilesRepository(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<AddFileResponse> UploadFiles(string bucketName, IList<IFormFile> formFiles)
        {
            var response = new List<string>();
            foreach (var file in formFiles)
            {
                var uplaodRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = file.OpenReadStream(),
                    Key = file.FileName,
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.NoACL

                };
                using (var fTransfer = new TransferUtility(_client))
                {
                    await fTransfer.UploadAsync(uplaodRequest);
                }
                var expUrlReq = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = file.FileName,
                    Expires = DateTime.Now.AddDays(1)
                };
                var url = _client.GetPreSignedURL(expUrlReq);
                response.Add(url);
            }
            return new AddFileResponse() { PreSignedUrl = response };
        }

        
    }
}
