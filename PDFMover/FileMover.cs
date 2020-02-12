using System.IO;

namespace DDigitIngest
{
  class FileMover
  {
    public FileMover(string path, int filesInFolder, int folderOffset)
    {
      this.path = path;
      this.filesInFolder = filesInFolder;
      this.folderOffset = folderOffset;

      if (!Directory.Exists(path))
      {
        throw new DirectoryNotFoundException(path);
      }
    }

    internal int MoveFiles()
    {
      int count = 0;
      var directoryInfo = new DirectoryInfo(path);
      foreach (var file in directoryInfo.GetFiles())
      {
        if (MoveFile(file))
        {
          count++;
        }
      }
      return count;
    }

    bool MoveFile(FileInfo file)
    {
      bool moved = false;
      var folderNumber = ComputeFolder(file.Name);
      if (folderNumber > 0)
      {
        var destination = $"{GetSubFolder(folderNumber)}{Path.DirectorySeparatorChar}{file.Name}";
        try
        {
          File.Move(file.FullName, destination);
          moved = true;
        }
        catch (IOException)  // ignore errors, they would be mostly file in use, ust retry next round.
        {
         
        }
      }
      return moved;
    }

    string GetSubFolder(int folderNumber)
    {
      var subFolder = $"{path}{Path.DirectorySeparatorChar}{folderNumber}";
      if (!Directory.Exists(subFolder))
      {
        Directory.CreateDirectory(subFolder);
      }
      return subFolder;
    }

    int ComputeFolder(string fileName)
      => int.TryParse(Path.GetFileNameWithoutExtension(fileName), out int fileNumber) ?
         (fileNumber / filesInFolder) - folderOffset : 0;

    readonly string path;
    readonly int filesInFolder;
    readonly int folderOffset;
  }
}
