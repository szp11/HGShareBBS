using HGShare.Container;
using HGShare.Log;
using HGShare.Utils.Interface;
using HGShare.Utils.Mail;
using HGShare.Utils.VerifyCode;
using HGShare.Web.Business;
using HGShare.Web.Business.DB;
using HGShare.Web.Interface;

namespace HGShare.Web.ServiceManager
{
    public class IocContainer:IIcoReader
    {
        private static IContainer _container;
        /// <summary>
        /// 注册实现
        /// </summary>
        public IocContainer()
        {
            if (_container!=null)
                return;
            _container = new AutofacAdapter();
            _container
                .Register(_ => _container)

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
                _container
                    .Register<IArticles, Web.Business.ES.Articles>()
                    .Register<IComments, Web.Business.ES.Comments>();
            }
            else
            {
                _container
                    .Register<IArticles, Articles>()
                    .Register<IComments, Comments>();
            }

        }

        #region service
        /// <summary>
        /// 实现
        /// </summary>
        /// <returns></returns>
        public T Service<T>() where T : class
        {
            return _container.Resolve<T>();
        }
        /// <summary>
        /// 实现
        /// </summary>
        /// <returns></returns>
        public T Service<T>(string parameterName, object parameterValue) where T : class
        {
            return _container.Resolve<T>(parameterName, parameterValue);
        }
        #endregion
    }
}
