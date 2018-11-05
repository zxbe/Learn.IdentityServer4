using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            // 用于发现端点的客户端
            // 根据IdentityServer的基地址 - 从元数据中读取实际的端点地址
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine($"从元数据中读取实际的端点地址出错：{disco.Error}");
                return;
            }

            // request token
            // 使用TokenClient类来请求令牌。(要创建实例，您需要传递令牌端点地址，客户端ID和密码。)
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"请求令牌失败：{tokenResponse.Error}");
                return;
            }

            // 将访问令牌从控制台复制并粘贴到jwt.io(https://jwt.io/)以检查原始令牌。
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // 请求到令牌后，就取访问api
            // call api
            var client = new HttpClient();
            // 要将访问令牌发送到API，通常使用HTTP Authorization标头。这是使用SetBearerToken扩展方法完成的：
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadKey();
        }
    }
}
