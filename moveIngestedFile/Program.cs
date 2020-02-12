using System;
using System.IO;

namespace moveIngestedFile
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length != 2)
      {
        Console.WriteLine("moveIngestedFile source destination");
      }
      else
      {
        using (var streamWriter = new StreamWriter("move.log"))
        {
          streamWriter.WriteLine($"{args[0]} move to {args[1]}");
          streamWriter.WriteLine(File.Exists(args[0]));
        }
      }
    }
  }
}
