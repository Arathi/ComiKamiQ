using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace ComiKamiQ
{
    static class DUtilFactory
    {
        private static Regex reHostName = new Regex("http://([^/]+)/");

        public static DUtil CreateDUtil(string url)
        {
            DUtil dutil=null;
            Match match = reHostName.Match(url);
            string hostname=null;
            if (match.Success && match.Groups.Count != 0)
                hostname = match.Groups[1].Value;
            else
                return null;
            switch (hostname)
            {
            	case "www.dmzj.com":
            		dutil = new DUtilDmzj(url);
            		break;
//                case "manhua.178.com":
//                    dutil = new DUtil178(url);
//                    break;
//                case "www.imanhua.com":
//                    dutil = new DUtilImanhua(url);
//                    break;
                case "www.bengou.com":
                    dutil = new DUtilBengou(url);
                    break;
                default:
                    break;
            }
            return dutil;
        }
    }
}
