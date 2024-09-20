using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageMicroservice.Dtos;
using StorageMicroservice.Model;
using StorageMicroservice.Services;

namespace StorageMicroservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SFilesController : ControllerBase
    {
        private readonly IStorageService _storageService; 
        private readonly IS3Service _s3Service;

        public SFilesController(IStorageService storageService , IS3Service s3Service)
        {
            _storageService = storageService;
            _s3Service = s3Service;
        }

        // GET: api/SFiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SFile>>> GetSFile()
        {
            var sfileList = await _storageService.GetSFileListAsync();
            if (sfileList == null)
            {
                return NotFound();
            }
            return Ok(sfileList);
        }

        // GET: api/SFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SFile>> GetSFile(int id)
        {
            var sfile = await _storageService.GetSFileByIdAsync(id);

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
            var sfile = new SFile()
            { 
                Name = sfiledata.Name , 
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

            var newsfile = _storageService.UploadSFileAsync(sfile);

            return CreatedAtAction("GetSFile", new { id = newsfile.Id }, newsfile);
        }

       

        // DELETE: api/SFiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSFile(int id)
        {
            var sfile = await _storageService.DeleteSFile(id);
            if (sfile == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
