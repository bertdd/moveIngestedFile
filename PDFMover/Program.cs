using System;
using System.IO;
using System.Threading;

namespace DDigitIngest
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        if (args.Length < 1)
        {
          Console.WriteLine("Usage: IngestFileMover parentFolder [filesPerFolder] [fileOffset]");
          Console.WriteLine("Press any key");
          Console.ReadLine();
          return;
        }

        int filesPerFolder = 0;
        if (args.Length > 1)
        {
          if (!(int.TryParse(args[1], out filesPerFolder) || filesPerFolder <= 100))
          {
            filesPerFolder = 500;
          }
        }

        if (!(args.Length > 2 && int.TryParse(args[2], out int fileOffset)))
        {
          fileOffset = 399999;
        }

        Console.WriteLine($"Files per folder: {filesPerFolder}");
        Console.WriteLine($"File offset     : {fileOffset}");

        mover = new FileMover(args[0], filesPerFolder, fileOffset);

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
        Console.ReadLine();
      }
    }

    static FileMover mover;
  }
}
