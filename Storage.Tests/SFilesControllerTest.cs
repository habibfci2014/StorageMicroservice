using Microsoft.AspNetCore.Mvc;
using Moq;
using Storage.Domain.Entities;
using Storage.Domain.Models;
using Storage.Domain.Services;
using Storage.Infrastructure.Services;
using StorageMicroservice.Controllers;

namespace Storage.Tests
{
    public class SFilesControllerTest
    {
        private readonly SFilesController _controller;
        private readonly Mock<ISFileService> _sfileService;
        private readonly Mock<IS3Service> _s3Service;
        private readonly Mock<IFileValidationService> _fileValidationService;

        public SFilesControllerTest()
        {
            _sfileService = new Mock<ISFileService>();
            _controller = new SFilesController(_sfileService.Object , _s3Service.Object , _fileValidationService.Object);

        }

        [Fact]
        public async Task GetSFile_ReturnsOkResult_WithValidData()
        {
            // Arrange
            var pagedData = new PagedData<SFile>
            {
                Result = new List<SFile> { new SFile { Id = 1, Name = "File1" } },
                TotalRecords = 1
            };
            _sfileService.Setup(service => service.GetSFileList(0, 10)).ReturnsAsync(pagedData);

            // Act
            var result = await _controller.GetSFile(0, 10);

            // Assert
            var okResult = Assert.IsType<ActionResult<PagedData<SFile>>>(result);
            Assert.NotNull(okResult);
            Assert.Equal(pagedData, okResult.Value);
        }

        [Fact]
        public async Task GetSFileById_ReturnsNotFound_WhenSFileDoesNotExist()
        {
            // Arrange
            _sfileService.Setup(service => service.GetSFileById(1)).ReturnsAsync((SFile)null);

            // Act
            var result = await _controller.GetSFile(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
