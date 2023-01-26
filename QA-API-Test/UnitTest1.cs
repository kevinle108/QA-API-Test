using FluentAssertions;
using Xunit.Abstractions;
using System.Text.Json;
using FluentAssertions.Execution;

namespace QA_API_Test
{

    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public async void Test1()
        {
            string baseUrl = "https://www.valvesoftware.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var response = await client.GetAsync("about/stats");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<ResponseModel>(content);
            var usersOnline = int.Parse(data.users_online, System.Globalization.NumberStyles.AllowThousands);
           
            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                usersOnline.Should().BeGreaterThan(0); 
            }

        }
    }
}