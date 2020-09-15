using GymMovesWebAPI.Data.Models.RequestModels;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GymMovesWebAPI.Tests {
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Startup>> {
        private readonly HttpClient httpClient;

        public IntegrationTest(WebApplicationFactory<Startup> factory) {
            httpClient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task Test() {
            Assert.True(true);
        }
    }
}
