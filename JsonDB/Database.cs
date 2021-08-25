using System;
using System.Collections.Generic;
using System.IO;

namespace JsonDB
{
    public class Database
    {
        public string DBLocation;
        public string Name;
        public bool DatabaseOutput;

        /// <summary>
        /// Location of where the Database is in the files
        /// Defaults to the location of the current directory
        /// </summary>
        /// <param name="DatabaseLocation"></param>
        public Database(string DatabaseName, string DatabaseLocation = null, bool DatabaseConsoleOutput = true)
        {
            if (DatabaseLocation != null)
                DBLocation = DatabaseLocation;
            else
                DBLocation = Path.Combine(Directory.GetCurrentDirectory(), DatabaseName);

            DatabaseOutput = DatabaseConsoleOutput;
            Name = DatabaseName;
        }

        public void CheckDB()
        {
            if (!Directory.Exists(DBLocation))
                Directory.CreateDirectory(DBLocation);
        }
    }
}
