在设置好 IdentityServer4 服务器后
1. 将IdentityServer4.AccessTokenValidation NuGet包添加到项目中。
	（这里使用了Microsoft.AspNetCore.Authentication.JwtBearer包来替换 AccessTokenValidation）
2. 配置 认证 服务。
	Authenticatiuon: 认证; 身份验证; 证明，鉴定; 密押。
	Authorization: 授权，批准; 批准（或授权）的证书。
3. 如果您使用浏览器导航到控制器（http://localhost:5001/identity），您应该获得401状态代码作为回报。
   这意味着您的API需要凭据。

就是这样，API现在受到IdentityServer的保护。