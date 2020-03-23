using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.S3.Core.Communication.Files;
using AWS.S3.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.S3.Api.Controller
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesRepository _filesRepo;
        public FilesController(IFilesRepository repo)
        {
            _filesRepo = repo;
        }
        [HttpPost]
        [Route("{bucketname}/add")]
        public async Task<ActionResult<AddFileResponse>> AddFiles(string bucketName, IList<IFormFile> formFiles) {
            if (null == formFiles) {
                return BadRequest("Request doens't containt any files to host into S3");
            }
            var response = await _filesRepo.UploadFiles(bucketName, formFiles);
            if (null == response)
                return BadRequest("something failed at the backend");
            return Ok(response);

        }
    }
}