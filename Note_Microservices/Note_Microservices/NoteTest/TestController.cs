using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTest
{
    [TestFixture]
    public class TestController
    {
        private NoteController _controller;
        private Mock<INoteService> _noteServiceMock;
        private Mock<HttpContext> _httpContextMock;

        [SetUp]
        public void Setup()
        {
            _noteServiceMock = new Mock<INoteService>();
            _httpContextMock = new Mock<HttpContext>();

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
}
