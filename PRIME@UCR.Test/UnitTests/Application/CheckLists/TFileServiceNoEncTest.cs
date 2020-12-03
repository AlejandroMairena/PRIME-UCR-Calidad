using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PRIME_UCR.Application.Implementations.CheckLists;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;
using Xunit;
using System;
using PRIME_UCR.Application.Implementations.Multimedia;
using System.IO;
using System.Text;

namespace PRIME_UCR.Test.UnitTests.Application.CheckLists
{
    public class TFileServiceNoEncTest
    {
        [Fact]
        public async Task StoreFileTest()
        {
            // arrange
            var testStream = new MemoryStream(Encoding.UTF8.GetBytes(""));
            var service = new TempFileServiceNoEncryption();

            // act
            var result = await service.StoreFile("testFile", testStream);

            // assert
            Assert.True(result);

        }
    }
}
