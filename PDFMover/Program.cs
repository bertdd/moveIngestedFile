using System;
using System.IO;
using System.Threading;

namespace DDigitIngest
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 1)
      {
        Console.WriteLine("Usage: IngestFileMover parentFolder [filesPerFolder] [fileOffset]");
        return;
      }

      if (!(args.Length > 2 && int.TryParse(args[1], out int filesInFolder) && filesInFolder > 100))
      {
        filesInFolder = 500;
      }

      if (!(args.Length > 3 && int.TryParse(args[2], out int fileOffset) && fileOffset > 0))
      {
        fileOffset = 399999;
      }


      try
      {
        mover = new FileMover(args[0], filesInFolder, fileOffset);

        while (true)
        {
          Console.WriteLine($"{DateTime.Now} check");
          var count = mover.MoveFiles();
          if (count > 0)
          {
            Console.WriteLine($"{count} files moved");
          }
          Thread.Sleep(10000);
        }
      }
      catch (DirectoryNotFoundException ex)
      {
        Console.WriteLine($"Ingest directory '{ex.Message}' not found!");
      }
    }

    static FileMover mover;
  }
}
