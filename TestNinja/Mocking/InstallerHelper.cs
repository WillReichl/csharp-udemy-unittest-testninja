using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private IFileDownlaoder _fileDownloader;

        public InstallerHelper(IFileDownlaoder fileDownlaoder)
        {
            _fileDownloader = fileDownlaoder;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {               
            try
            {
                var url = string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName);
                _fileDownloader.DownloadFile(url, _setupDestinationFile);
                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}