using FluentAssertions;
using Xunit.Abstractions;
using System.Text.Json;
using FluentAssertions.Execution;
using System.Text;

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
        public async void SteamApiTest()
        {
            string baseUrl = "https://www.valvesoftware.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var response = await client.GetAsync("about/stats");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<SteamResponseModel>(content);
            var usersOnline = int.Parse(data.users_online, System.Globalization.NumberStyles.AllowThousands);
           
            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                usersOnline.Should().BeGreaterThan(0); 
            }

        }

        [Fact]
        public async void ApisApiCountMatchesNumOfEntries()
        {
            string baseUrl = "https://api.publicapis.org/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var response = await client.GetAsync("entries");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<ApisResponseModel>(content);
            int count = data.count;

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                count.Should().BeGreaterThan(0);
                data.entries.Length.Should().Be(count);
            }
        }

        [Fact]
        public async void JSONPlaceholder_GET()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("posts");
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Post[]>(content);            

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();                
                data.Length.Should().Be(100);
            }
        }

        [Fact]
        public async void JSONPlaceholder_GET_Params()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("comments?postId=1");
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Comment[]>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.Length.Should().Be(5);
                data.First().email.Should().Be("Eliseo@gardner.biz");
                data.Last().email.Should().Be("Hayden@althea.biz");
            }
        }

        [Fact]
        public async void JSONPlaceholder_POST()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    title = "foo",
                    body = "bar",
                    userId = 1
                }),
                Encoding.UTF8,
                "application/json"
            );


            var response = await client.PostAsync("posts", jsonContent);
            var content = await response.Content.ReadAsStringAsync();


            var data = JsonSerializer.Deserialize<Post>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.id.Should().Be(101);
                data.title.Should().Be("foo");
                data.body.Should().Be("bar");
                data.userId.Should().Be(1);
            }
        }

        [Fact]
        public async void JSONPlaceholder_PUT()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    id = 1,
                    userId = 1,
                    title = "foo",
                    body = "bar",
                }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync("posts/1", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Post>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.id.Should().Be(1);
                data.title.Should().Be("foo");
                data.body.Should().Be("bar");
                data.userId.Should().Be(1);
            }
        }

        [Fact]
        public async void JSONPlaceholder_PATCH()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    body = "bar",
                }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PatchAsync("posts/1", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Post>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                data.id.Should().Be(1);
                data.title.Should().Be("sunt aut facere repellat provident occaecati excepturi optio reprehenderit");
                data.body.Should().Be("bar");
                data.userId.Should().Be(1);
            }
        }

        [Fact]
        public async void JSONPlaceholder_DELETE()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.DeleteAsync("posts/1");
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Post>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeTrue();
                content.Should().Be("{}");
            }
        }

        [Fact]
        public async void JSONPlaceholder_Negative_Invalid_Method()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.DeleteAsync("posts/");
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Post>(content);

            using (new AssertionScope())
            {
                response.IsSuccessStatusCode.Should().BeFalse();
                content.Should().Be("{}");
            }
        }
    }
}