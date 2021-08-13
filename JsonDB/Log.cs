using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDB
{
    public class Log
    {
        private static void WriteLine(string text, ConsoleColor? Color = null)
        {
            if (text == null) return;
            if (Color != null)
                Console.ForegroundColor = Color.Value;

            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Log(string Database, string Collection)
        {
            DB = Database;
            CollectionName = Collection;
        }


        private string DB;
        private string CollectionName;


        public void Error(string text) => WriteLine(string.Format("[{0}.{1}] [Error] {2}", DB, CollectionName, text), ConsoleColor.Red);
        public void Information(string text) => WriteLine(string.Format("[{0}.{1}] [Info] {2}", DB, CollectionName, text), ConsoleColor.Green);
        
    }
}
