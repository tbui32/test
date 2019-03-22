using System.IO;
using System.Net;
using DongABank.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DongABank.Controllers
{
    public class TyGiaController : Controller
    {
        public IActionResult Index()
        {
            string siteContent = string.Empty;
            string url = "http://www.dongabank.com.vn/exchange/export";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers["User-Agent"] = "Mozilla/5.0 ( compatible ) ";
            request.Headers["Accept"] = "*/*";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string data = reader.ReadToEnd();
            data = data.Replace(")", "").Replace("(", "");
            TyGiaDongA tigia = (TyGiaDongA)JsonConvert.DeserializeObject(data, typeof(TyGiaDongA));
            return View(tigia.items);
        }
    }
}