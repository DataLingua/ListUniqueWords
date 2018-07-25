using System;
using System.IO;
using System.Text;

namespace ListUniqueWords
{
   class ParseFile : ParseText
   {
      // This module defines and reads the strBuildIn,
      // calls ParseThisText to do the work, then
      // writes the output file.
      public bool ParseThisFile(string inFile, string outFile)
      {
         StreamReader streamReader = new StreamReader(inFile,true); // detectEncodingFromByteOrderMarks
         strBuildIn = new StringBuilder();  // declared in ParseCore.cs
         strBuildIn.Append(streamReader.ReadToEnd());
         StreamWriter streamWriter = new StreamWriter(outFile);

         if (ParseThisText())
         {
            streamWriter.Write(strBuildOut.ToString());
            streamWriter.Close();
            return true;
         }
         else
            return false;
      }

      public bool ParseStdIO()
      {
         strBuildIn = new StringBuilder(); // declared in ParseCore.cs
         while (Console.KeyAvailable)
            strBuildIn.AppendLine(Console.ReadLine());

         if (ParseThisText())
         {
            TextWriter textWriter = Console.Out;
            textWriter.Write(strBuildOut.ToString());
            return true;
         }
         else
            return false;
      }
   }
}
