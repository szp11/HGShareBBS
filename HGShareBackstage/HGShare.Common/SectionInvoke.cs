using System.Configuration;

namespace HGShare.Common
{
    /// <summary>
    /// Config SectionInvoke
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SectionInvoke<T>
       where T : ConfigurationSection
    {
        public static T GetConfig(string sectionName)
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }
    }
}
