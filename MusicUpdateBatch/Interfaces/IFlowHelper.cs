using System;
using System.Collections.Generic;
using System.Text;

namespace MusicUpdateBatch.Interfaces
{
    interface IFlowHelper
    {
        List<string> GetValidSubFolders();

        List<string> GetValidFileNameInFolder(string folder);

        TagLib.Tag GetTagFromFileNameInFolder(string folder, string file);
    }
}
