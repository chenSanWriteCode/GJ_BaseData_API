using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;


namespace GJ_BaseData_API
{
    public partial class StartUp
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            #region 客户端模式

            //客户端模式最简需要配置这些
            var clientOptions = new OAuthAuthorizationServerOptions()
            {
                //允许http模式，debug模式为true，发布成网站时改为false，要用https才安全（数据安全性与完整性）
                AllowInsecureHttp = true,
                //允许修改request，修改redirect等，客户端模式可以为false
                AuthenticationMode = AuthenticationMode.Active,
                //客户端获取token的地址，比如 http://localhost:20522/ReportApi/Author/Token
                TokenEndpointPath = new PathString("/ReportApi/Author/Token"),
                //token的过期时长
                //AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Infrastructure.REFREST_INTERVAL),
                //token的相关验证授权等
                //Provider = new OAuthClientServerProvider()
            };
            /*
            app.UseOAuthBearerTokens(clientOptions);
            */
            #endregion

        }
    }
}
