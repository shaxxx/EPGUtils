using ConfigMaker.Model;
using ConfigMaker.Properties;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Commands
{
    public class SiteIniPackUpdater
    {

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        public event StatusChangedEventHandler StatusChanged;

        public event AsyncCompletedEventHandler DownloadFileCompleted;
        public DirectoryInfo DownloadDirectory { get; }
        public DirectoryInfo WorkingDirectory { get; }

        public SiteIniPackUpdater(DirectoryInfo workingDirectory)
        {
            var tempDir = Utils.CreateTemporaryDirectory();
            DownloadDirectory = new DirectoryInfo(tempDir);
            WorkingDirectory = workingDirectory;
        }

        public SiteIniPackUpdater(DirectoryInfo workingDirectory, DirectoryInfo downloadDirectory)
        {
            this.DownloadDirectory = downloadDirectory;
            this.WorkingDirectory = workingDirectory;
        }

        public async Task Execute()
        {

            var zipFile = await DownloadFileAsync();
            var targetDirectory = new DirectoryInfo(Path.Combine(DownloadDirectory.FullName, Settings.Default.SiteIniPack));
            Utils.SafelyDeleteFolder(targetDirectory);
            Unzip(zipFile, DownloadDirectory);
            Utils.SafelyDeleteFile(zipFile.FullName);
            var sitePackDirLocation = Path.Combine(WorkingDirectory.FullName, Settings.Default.SiteIniPack);
            var sitePackDir = new DirectoryInfo(sitePackDirLocation);
            if (sitePackDir.Exists)
            {
                var sitePackBackupDir = new DirectoryInfo(Path.Combine(WorkingDirectory.FullName, Settings.Default.SiteIniPack + ".backup"));
                if (sitePackBackupDir.Exists)
                {
                    sitePackBackupDir.Delete(true);
                }
                sitePackDir.MoveTo(sitePackBackupDir.FullName);
            }
            targetDirectory.MoveTo(sitePackDirLocation);
        }

        private async Task<FileInfo> DownloadFileAsync()
        {
            try
            {
                ReportStatus("Downloading siteini.pack");
                string url = Settings.Default.SitePackUrl;
                string filename = Path.GetFileName(url);
                string zipFileName = Path.Combine(DownloadDirectory.FullName, filename);
                Log.Debug(string.Format("Downloading {0} to {1}", url, zipFileName));
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                    await wc.DownloadFileTaskAsync(new Uri(url), zipFileName);
                    Log.Debug(string.Format("Downloaded {0} to {1}", url, zipFileName));
                    return new FileInfo(zipFileName);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to download siteini.pack{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadFileCompleted?.Invoke(this, e);
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged?.Invoke(sender, e);
            ReportStatus("Downloading siteini.pack", $"{e.ProgressPercentage} / 100 %");
        }

        private void Unzip(FileInfo zipFileName, DirectoryInfo destination)
        {
            try
            {
                ReportStatus("Extracting archive", " ");
                Log.Debug(string.Format("Extracting {0} to {1}", zipFileName, destination.FullName));
                using (var zip = ZipFile.Read(zipFileName.FullName))
                {
                    foreach (var entry in zip.Entries.ToList())
                    {
                        var extension = Path.GetExtension(entry.FileName).ToLower();
                        if (extension == ".xml" || extension == ".ini")
                        {
                            if (entry.FileName.ToLower().StartsWith(Settings.Default.SiteIniPack))
                            {
                                if (Utils.IsValidFilename(entry.FileName))
                                {
                                    ///https://github.com/SilentButeo2/webgrabplus-siteinipack/pull/505
                                    if (!entry.FileName.StartsWith("siteini.pack/Israel/yes.co.il."))
                                    {
                                        entry.Extract(destination.FullName, ExtractExistingFileAction.OverwriteSilently);
                                    }
                                    
                                }
                            }
                        }
                    }
                }
                Log.Debug(string.Format("Extracted {0} to {1}", zipFileName, destination.FullName));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to unzip siteini.pack{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }
    }
}
