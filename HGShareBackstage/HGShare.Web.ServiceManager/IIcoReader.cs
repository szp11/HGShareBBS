namespace HGShare.Web.ServiceManager
{
    public interface IIcoReader
    {
        T Service<T>() where T : class;
        T Service<T>(string parameterName, object parameterValue) where T : class;
    }
}
