using GymMovesWebAPI.Data.Models.RequestModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
            ClassRatingRequest rating = new ClassRatingRequest {
                username = "johncena",
                classId = 1,
                rating = 3
            };

            var ratingToString = await Task.Run(() => JsonConvert.SerializeObject(rating));

            var httpContent = new StringContent(ratingToString, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/ratings/class", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
