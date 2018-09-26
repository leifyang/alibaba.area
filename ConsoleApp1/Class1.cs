using ConsoleApp1.DataBase;

using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class1
    {
        ConfigDBEntities _db = new ConfigDBEntities();
        public void m1()
        {
            try
            {
                using (var sr = new StreamReader("国内省份城市区域编码表.html", Encoding.GetEncoding("gbk")))
                {
                    Console.WriteLine("--->>>>>>");

                    string line;
                    var area = new Area();
                    while (null != (line = sr.ReadLine()))
                    {
                        if (line.StartsWith("<tr class=\"tr-prov"))
                        {
                            line = line.Replace("<tr class=\"tr-prov\">", "").Replace("</tr>", "").Replace("<td>", "").Replace("</td>", ",");
                            var tempLine = line.Split(',');
                            area.ProvinceNo = tempLine[0];
                            area.ProvenceName = tempLine[1];
                            area.CityNo = string.Empty;
                            area.CityName = string.Empty;
                            area.CountyNo = string.Empty;
                            area.CountyName = string.Empty;
                        }
                        if (line.StartsWith("<tr class=\"tr-city"))
                        {
                            line = line.Replace("<tr class=\"tr-city\">", "").Replace("</tr>", "").Replace("<td>", "").Replace("</td>", ",");
                            var tempLine = line.Split(',');
                            area.CityNo = tempLine[2];
                            area.CityName = tempLine[3];
                            area.CountyNo = string.Empty;
                            area.CountyName = string.Empty;
                        }
                        if (line.StartsWith("<tr class=\"tr-county"))
                        {
                            line = line.Replace("<tr class=\"tr-county\">", "").Replace("</tr>", "").Replace("<td>", "").Replace("</td>", ",");
                            var tempLine = line.Split(',');
                            area.CountyNo = tempLine[4];
                            area.CountyName = tempLine[5];
                        }

                        _db.Area.Add(area);
                        _db.SaveChanges();
                        Console.WriteLine(area.ProvenceName + "-" + area.CityName + "-" + area.CountyName);
                    }

                    Console.WriteLine("<<<<<-------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error：" + ex.Message);
            }
        }

        public void m2()
        {
            try
            {
                for (var i = 1; i <= 217; i++)
                {
                    Console.WriteLine("===开始拉取第" + i + "页===");
                    var data = HttpUtil.GetHtmlData(i);
                    using (var sr = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(data))))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Trim().StartsWith("<tr class=\"tr-city\">"))
                            {
                                line = line.Trim().Replace("<tr class=\"tr-city\">", "").Replace("</tr>", "").Replace("<td>", "").Replace("</td>", ",");
                                var tempLine = line.Split(',');

                                _db.Abroad.Add(new Abroad
                                {
                                    CountryNo = tempLine[0],
                                    CountryName = tempLine[1],
                                    CountryEnglish = tempLine[2],
                                    CityNo = tempLine[3],
                                    CityName = tempLine[4],
                                    CityEnglish = tempLine[5]
                                });
                                _db.SaveChanges();

                                Console.WriteLine(line);
                                Console.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                            }
                        }
                    }
                    Console.WriteLine("===第" + i + "页拉取结束===");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }
    }
}
