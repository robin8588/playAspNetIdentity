using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public class SiteUrl
    {
        static string version = null;
        static SiteUrl()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            version = assembly.GetName().Version.ToString();
        }

        private static string Domain
        {
            get
            {
                return "http://localhost:{0}";
            }
        }
        private static string API
        {
            get
            {

                return string.Format(Domain, 30807);
            }
        }
        private static string Auth
        {
            get
            {
                return string.Format(Domain, 15671);
            }
        }
        private static string Html
        {
            get
            {
                return string.Format(Domain, 36233);
            }
        }

        /// <summary>
        /// 静态内容路径
        /// </summary>
        /// <param name="visualPath">虚拟路径</param>
        /// <returns></returns>
        public static string Content(string visualPath)
        {
            if (visualPath.StartsWith("~"))
            {
                return Html + visualPath.TrimStart('~');
            }
            else
            {
                throw new Exception("路径不是虚拟路径");
            }
        }

        /// <summary>
        /// 版本号
        /// </summary>
        /// <returns></returns>
        public static string VersionNumber()
        {
            return version;
        }

        /// <summary>
        /// 带版本号的静态内容路径
        /// </summary>
        /// <param name="visualPath">虚拟路径</param>
        /// <returns></returns>
        public static string ContentV(string visualPath)
        {
            return Content(visualPath) + "?v=" + VersionNumber();
        }
    }
}
