using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public class FileDownlaoder : IFileDownlaoder
    {
        public void DownloadFile(string url, string installPath)
        {
            var client = new WebClient();
            client.DownloadFile(url, installPath);
        }
    }
}
