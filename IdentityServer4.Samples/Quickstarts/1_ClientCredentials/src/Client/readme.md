﻿在配置好 IdentityServer 服务器 以及 Api 服务后，
建立客户端：
请求访问令牌的客户端，然后使用此令牌访问API。
为此，请向您的解决方案添加一个控制台项目（请参阅此处的完整代码）。

IdentityServer的令牌端点实现OAuth 2.0协议，您可以使用原始HTTP来访问它。
但是，我们有一个名为IdentityModel的用于发现端点的客户端库，它将协议交互封装在一个易于使用的API中。
(这样您只需要知道IdentityServer的基地址 - 可以从元数据中读取实际的端点地址;)

1. 引入将IdentityModel NuGet包添加到您的应用程序。
2. 配置 令牌请求环境和利用请求到的令牌访问需要认证的api。
  （默认情况下，访问令牌将包含有关范围，生命周期（nbf和exp），客户端ID（client_id）和颁发者名称（iss）的声明。）
3. 使用Postman调试

您现在可以尝试激发错误以了解系统的行为，例如

尝试在未运行时连接到IdentityServer（不可用）
尝试使用无效的客户端ID或机密来请求令牌
尝试在令牌请求期间请求无效范围
尝试在API未运行时调用API（不可用）
不要将令牌发送到API
将API配置为需要与令牌中的范围不同的范围