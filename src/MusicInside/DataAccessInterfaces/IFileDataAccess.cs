using MusicInside.Models;

namespace MusicInside.DataAccessInterfaces
{
    public interface IFileDataAccess
    {
        /// <summary>
        /// Return a MusicInside.File identified by its id
        /// </summary>
        /// <param name="id">The File identifier</param>
        /// <returns>A MusicInside.File</returns>
        File GetFileById(int id);

        /// <summary>
        /// Access to file system and retrieve file bytes of a particular physical file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        byte[] GetFileBytesById(int id);
    }
}
