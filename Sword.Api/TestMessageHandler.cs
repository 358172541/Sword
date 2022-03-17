using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageContexts;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.IO;
using System.Threading.Tasks;
using Config = Senparc.Weixin.Config;

namespace Api
{
    public class TestMessageHandler : MessageHandler<DefaultMpMessageContext>
    {
        public TestMessageHandler(
            Stream inputStream,
            PostModel postModel,
            int maxRecordCount = 0,
            IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, false, null, serviceProvider) { }

        public override async Task<IResponseMessageBase> OnTextOrEventRequestAsync(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            await Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendTextAsync(Config.SenparcWeixinSetting.MpSetting.WeixinAppId, OpenId, $"这是一条异步的客服消息");//注意：只有测试号或部署到正式环境的正式服务号可用此接口
            responseMessage.Content = $"你发送了文字：{requestMessage.Content}\r\n\r\n你的OpenId：{OpenId}";
            return responseMessage;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"欢迎来到我的公众号！当前时间：{SystemTime.Now}";
            return responseMessage;
        }
    }
}
