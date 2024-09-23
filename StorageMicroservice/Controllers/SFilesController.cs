using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.Domain.Entities;
using Storage.Domain.Models;
using Storage.Domain.Services;
using Storage.Infrastructure.Services;
using StorageMicroservice.Dtos;
using StorageMicroservice.Services;

namespace StorageMicroservice.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SFilesController : ControllerBase
    {
        private readonly ISFileService _sfileService;
        private readonly IS3Service _s3Service;
        private readonly IFileValidationService _fileValidationService;

        public SFilesController(ISFileService sfileService, IS3Service s3Service, IFileValidationService fileValidationService)
        {
            _sfileService = sfileService;
            _s3Service = s3Service;
            _fileValidationService = fileValidationService;
        }

        // GET: api/SFiles
        [HttpGet]
        public async Task<ActionResult<PagedData<SFile>>> GetSFile(int pageIndex , int pageSize)
        {
            var sfiles = await _sfileService.GetSFileList(pageIndex , pageSize);
            return sfiles;
        }

        // GET: api/SFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SFile>> GetSFile(int id)
        {
            var sfile = await _sfileService.GetSFileById(id);
            if (sfile == null)
            {
                return NotFound();
            }

            return sfile;
        }


        // POST: api/SFile
        [HttpPost]
        public async Task<ActionResult<SFile>> PostSFile([FromForm] SFileData sfiledata, IFormFile file)
        {
            //Validate File
            string ValidateFile = _fileValidationService.ValidateFile(file);
            if (!string.IsNullOrEmpty(ValidateFile))
                return BadRequest(ValidateFile);


            var sfile = new SFile()
            {
                Name = file.FileName,
                Description = sfiledata.Description
            };

            //save file to AWS S3
            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    sfile.FileUrl = await _s3Service.UploadFileAsync(stream, file.FileName);
                }
            }

            await _sfileService.UpdateSFile(sfile);

            return CreatedAtAction("GetSFile", new { id = sfile.Id }, sfile);

        }

        // PUT : api/SFiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, SFileData sfiledata)
        {
            var sfile = await _sfileService.GetSFileById(id);
            if (sfile == null)
            {
                return NotFound();
            }

            sfile.Description = sfiledata.Description;

            await _sfileService.UpdateSFile(sfile);
            return NoContent();
        }


        // DELETE: api/SFiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSFile(int id)
        {
            var sfile = await _sfileService.GetSFileById(id);
            if (sfile == null)
            {
                return NotFound();
            }

            // Delete file from S3
            await _s3Service.DeleteFileAsync(sfile.FileUrl);

            await _sfileService.DeleteSFile(id);


            return NoContent();
        }

    }
}
