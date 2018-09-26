using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public class HttpUtil
    {
        public static string GetHtmlData(int i)
        {
            var url = "http://hotel.alitrip.com/area.htm?domestic=1&enName=&page=" + i;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=GBK";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) return string.Empty;
            var result = "";
            Stream responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                using (var sr = new StreamReader(responseStream, Encoding.GetEncoding("gbk")))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
    }
}
