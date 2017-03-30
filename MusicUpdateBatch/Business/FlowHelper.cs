using Microsoft.Extensions.Configuration;
using MusicUpdateBatch.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicUpdateBatch.Business
{
    public class FlowHelper : IDisposable, IFlowHelper
    {
        private readonly log4net.ILog _logger;
        private readonly IConfiguration _configuration;
        private readonly string _fileMusicRoot;
        private readonly Regex _configuredFolderRegex;
        private readonly Regex _allowedExtensionRegex;

        public FlowHelper(log4net.ILog log, IConfiguration conf)
        {
            _logger = log;
            _configuration = conf;
            _logger.Debug("FlowHelper | Constructor: Object was built, start to load configurations from file...");
            // Retrieve root directory from configuration
            _fileMusicRoot = _configuration.GetSection("MusicFiles").GetValue<string>("RootDirectory");
            // Retrieve folder regex from configuration
            string configuredRegexString = _configuration.GetSection("MusicFiles").GetValue<string>("SubFolderRegex");
            _configuredFolderRegex = new Regex(configuredRegexString);
            // Build extensions regex retrieving allowed extensions
            string[] validExtensionList = _configuration.GetSection("MusicFiles").GetValue<string>("AvailableExtensions").Split(',');
            StringBuilder buildRegexString = new StringBuilder(@"(\.");
            buildRegexString.Append(string.Join(@"$|\.", validExtensionList)).Append("$)");
            _allowedExtensionRegex = new Regex(buildRegexString.ToString());
            _logger.Debug("FlowHelper | Constructor: Configurations correctly built from file.");
        }

        public List<string> GetValidSubFolders()
        {
            try
            {
                // Read all directories in the music root folder
                List<string> subFolders = Directory.GetDirectories(_fileMusicRoot).Select(Path.GetFileName).ToList();
                // Keep only directory with given regex
                subFolders.RemoveAll(c => !_configuredFolderRegex.Match(c).Success);
                return subFolders;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("FlowHelper | GetValidSubFolders: Can't read directories in /{0} due to exception [{1}]", _fileMusicRoot, ex.Message);
                return null;
            }
        }

        public List<string> GetValidFileNameInFolder(string folder)
        {
            try
            {
                // Retrieve file names
                List<string> fileNameList = Directory.GetFiles(Path.Combine(_fileMusicRoot, folder)).Select(Path.GetFileName).ToList();
                // Keep only file name with valid extensions
                fileNameList.RemoveAll(f => !_allowedExtensionRegex.Match(f).Success);
                return fileNameList;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("FlowHelper | GetValidFileNameInFolder: Can't read content of folder /{0} due to exception [{1}]", folder, ex.Message);
                return null;
            }
        }

        public TagLib.Tag GetTagFromFileNameInFolder(string folder, string file)
        {
            try
            {
                _logger.DebugFormat("FlowHelper | GetTagFromFileNameInFolder: Attempt to read tags from file <{0}>", file);
                FileStream stream = new FileStream(Path.Combine(_fileMusicRoot, folder, file), FileMode.Open);
                var fileTag = TagLib.File.Create(new TagLib.StreamFileAbstraction(file, stream, stream));
                var tags = fileTag.GetTag(TagLib.TagTypes.Id3v2);
                _logger.DebugFormat("FlowHelper | GetTagFromFileNameInFolder: Tags from <{0}> were correctly read", file);
                return tags;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("FlowHelper | GetTagFromFileNameInFolder: Can't read tag of file /{0}/{1} due to exception [{2}]", folder, file, ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
