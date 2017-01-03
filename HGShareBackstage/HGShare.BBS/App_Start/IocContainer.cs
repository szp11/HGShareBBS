using HGShare.Com.Interface;
using HGShare.Com.Tools.Mail;
using HGShare.Com.Tools.VerifyCode;
using HGShare.Container;
using HGShare.Log;
using HGShare.Web.Business;
using HGShare.Web.Business.DB;
using HGShare.Web.Interface;

namespace HGShare.BBS
{
    public class IocContainer
    {
        private static readonly IContainer Container = new AutofacAdapter();
        /// <summary>
        /// 注册实现
        /// </summary>
        public static void RegisterServices()
        {
            Container
                .Register(_ => Container)



                .Register<IArticleTypes, ArticleTypes>()
                
                .Register<IUsers, Users>()
                .Register<IRoles, Roles>()
               
                .Register<IUsersPublic, UserPublic>()
                .Register<ICommentsPublic, CommentsPublic>()
                .Register<IUpload, Upload>()
                .Register<IArticlesPublic, ArticlesPublic>()
                .Register<IVip, Vip>()
                .Register<ILogin, Login>()
                .Register<IEmailTemplates, EmailTemplates>()
                .Register<ISendMailLogsPublic, SendMailLogsPublic>()
                .Register<IUserActivateTokensPublic, UserActivateTokensPublic>()
                .Register<IDianZanLogsPublic, DianZanLogsPublic>()
                .Register<IUserAccessLogsPublic, UserAccessLogsPublic>()

                .Register<ILog, Log4Net>()
                .Register<IMail, SmtpMail>()
                .Register<IVerifyCode, SimpleVerifyCode>();

            if (Site.Config.EsIndexConfig.IsUseEsData)
            {
                Container
                    .Register<IArticles, Web.Business.ES.Articles>()
                    .Register<IComments, Web.Business.ES.Comments>();
            }
            else
            {
                Container
                    .Register<IArticles, Articles>()
                    .Register<IComments, Comments>();
            }

        }

        #region service
        /// <summary>
        /// 实现
        /// </summary>
        /// <returns></returns>
        public static T Service<T>() where T : class
        {
            return Container.Resolve<T>();
        }
        /// <summary>
        /// 实现
        /// </summary>
        /// <returns></returns>
        public static T Service<T>(string parameterName, object parameterValue) where T : class
        {
            return Container.Resolve<T>(parameterName,parameterValue);
        }
        #endregion
    }
}