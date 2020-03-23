using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.S3.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AWS.S3.Core.Communication.Bucket;

namespace AWS.S3.Api.Controller
{
    [Route("api/bucket")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        public IBucketRepository _bucketRepo;
        public BucketController(IBucketRepository ib)
        {
            _bucketRepo = ib;
        }
        [HttpPost]
        [Route("create/{bucketName}")]
        public async Task<ActionResult<CreateBucketResponse>> CreateS3Bucket([FromRoute] string bucketName)
        {
            bool bucketExists = await _bucketRepo.DoesS3BucketExists(bucketName);
            if (bucketExists)
                return BadRequest("S3 bucket already exists.");
            var result = _bucketRepo.CreateBucket(bucketName);
            if (result == null)
                return BadRequest();
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<ListS3BucketResponse>>> ListS3Buckets()
        {
            var result = await _bucketRepo.ListS3Buckets();
            if (null == result)
                return NotFound("No buckets were found as part of current credential requests");
            else
            {
                return Ok(result);
            }
            
        }
    }
}