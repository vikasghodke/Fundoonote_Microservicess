using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Note_Microservices.Controllers;
using Note_Microservices.IService;
using Note_Microservices.Model;
using Note_Microservices.NoteEntity;
using System.Security.Claims;

namespace Testing
{
    [TestFixture]
    public class Tests
    {
        private NoteController _controller;
        private Mock<INoteService> _noteServiceMock;
        private Mock<HttpContext> _httpContextMock;

        [SetUp]
        public void Setup()
        {
            _noteServiceMock = new Mock<INoteService>();
            _httpContextMock = new Mock<HttpContext>();

            // Mock HttpContext
            _httpContextMock.SetupGet(h => h.User)
                            .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                            {
                                new Claim("UserID", "1") // Mock UserID claim
                            }, "mock")));

            _controller = new NoteController(_noteServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }
        [Test]
        public async Task AddNote_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var noteModel = new NoteModel();
            _noteServiceMock.Setup(x => x.AddNote(It.IsAny<NoteModel>(), It.IsAny<int>(), It.IsAny<string>()))
                            .ReturnsAsync(new NoteEntity1());

            // Act
            var result = await _controller.AddNote(noteModel);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Note created successfully.", result.Message);
            Assert.IsNotNull(result.Data);
        }
        [Test]
        public async Task GetNotes_WithValidUserId_ReturnsNotes()
        {
            // Arrange
            var userId = 1;
            var notesList = new List<NoteEntity1> { new NoteEntity1(), new NoteEntity1() };
            _noteServiceMock.Setup(x => x.GetNotes(userId)).Returns(notesList);

            // Act
            var result = _controller.GetNotes();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Notes retrived successfully.", result.Message);
            Assert.AreEqual(notesList, result.Data);
        }
        [Test]
        public async Task EditNote()
        {

        }

    }
}