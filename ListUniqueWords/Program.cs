using System;

namespace ListUniqueWords
{
   class Program
   {
      // This onion has four layers:
      // ParseCore is the mother of all base classes.
      // ParseText derives from ParseCore adding code to manipulate text.
      // ParseFile derives from ParseText and adds code to manipulate files.
      // Sub Main (this Program file) reads command line arguments and
      //   passes the info to a new ParseFile object.
      static void Main(string[] args)
      {
         // Check command line arguments
         ParseFile parseFile = new ParseFile();
         parseFile.boolRemoveNumbers = true;  // TODO: allow user to define this.

         try
         {
            if (args.Length == 0)
            {
               // If none, presume redirection of stein and stdout.
               if (parseFile.ParseStdIO())
               {
                  // TODO: find another way to notify user. Perhaps stderr?
                  // don't call ShowMessageToUser("Success!"); 
                  // because stdout is redirected and is our output data file.
                  // For now, there is no user feedback on success for command-line run.
               }
               else
               {
                  ShowMessageToUser(parseFile.errMsg);
               }
            }
            else if (args.Length == 1)
            {
               // If only one argument, define output as base name + "_UniqueWords.txt"
               string arg2 = args[0].ToString();
               int ptr = arg2.LastIndexOf(".");
               if (ptr == -1) // not found
                  arg2 = args[0].ToString() + "_UniqueWords.txt";
               else
                  arg2 = args[0].ToString().Substring(0, ptr) + "_UniqueWords.txt";

               if (parseFile.ParseThisFile(args[0].ToString(), arg2))
               {
                  ShowMessageToUser("Success!");
               }
               else
               {
                  ShowMessageToUser(parseFile.errMsg);
               }
            }
            else if (args.Length == 2)
            {
               // If two arguments, use them as input and output file names.
               if (parseFile.ParseThisFile(args[0].ToString(), args[1].ToString()))
               {
                  ShowMessageToUser("Success!");
               }
               else
               {
                  ShowMessageToUser(parseFile.errMsg);
               }
            }
            else
            {
               // else show how to use this program.
               ShowMessageToUser("Usage");
            }
         }
         catch(Exception ex)
         {
            // TODO: put try/catch at lower levels to generate more granular error handling.
            // Also, use an error log or stderr.
            ShowMessageToUser(ex.ToString());
         }
      }

      static void ShowMessageToUser(string msg)
      {
         // NOTE: this message is going to stdout which has probably been redirected on the command line.
         if (msg == "" || msg == "Usage")
         {
            Console.WriteLine("Usage: ListUniqueWords {inputfile.txt} (same as drag-and-drop)\r\n" +
                              "   or: ListUniqueWords {inputfile.txt} {outputfile.txt}\r\n" +
                              "   or: ListUniqueWords < {inputfile.txt} > {outputfile.txt}\r\n" +
                              "Must be ASCII or UTF-8 encoding\r\n");
         }
         else
         {
            Console.WriteLine(msg);
         }
         Console.Read();
      }
   }
}
