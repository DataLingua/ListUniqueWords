using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListUniqueWords
{
   class ParseText : ParseCore
   {
      public bool ParseThisText()
      {
         List<string> myCollection = new List<string>();
         strBuildOut = new StringBuilder();
         strBuildTemp = new StringBuilder();
         int inPtr = 0;
         char ch = 'a';
         bool ignoreWitespace = true; // start by ignoring whitespace at beginning of file.
                                      // this is set false every time we find a text character,
                                      // then set true again when \r\n is written at the end of a text block.

         // Replace everything between ASCII / UTF-8 characters with a single \r\n
         // In the process, remove hyphens when followed by \r\n,
         // and remove . when followed by a space or \r\n.
         while (inPtr < strBuildIn.Length)
         {
            ch = strBuildIn[inPtr];
            if ((boolRemoveNumbers && Char.IsLetter(ch))
              || (boolRemoveNumbers == false && Char.IsLetterOrDigit(ch)))
            {
               // keeper
               strBuildTemp.Append(ch);
               ignoreWitespace = false; // next whitespace will trigger \r\n
            }
            else
            {
               // whitespace
               if (ignoreWitespace == false)
               {
                  // transitioned from reading stuff we want to whitespace, so:
                  myCollection.Add(strBuildTemp.ToString());
                  strBuildTemp.Clear();
                  ignoreWitespace = true;
               }
            }
            inPtr++;
         }
         // if input ends with stuff we want, get last bit:
         if (strBuildTemp.Length > 0)
            myCollection.Add(strBuildTemp.ToString());

         // Sort the resulting list.

         myCollection.Sort();
         foreach (var str in myCollection.Distinct())
         {
          //  if (Char.IsUpper(str[0]))  // only keep words that start with a capital letter.
               strBuildOut.Append(str + "\r\n");
         }

         // Remove duplicate lines.

         return true;
      }
   }
}
