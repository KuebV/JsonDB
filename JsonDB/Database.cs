using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDB
{
    public class Database
    {
        public string DBLocation;
        public string Name;

        /// <summary>
        /// Location of where the Database is in the files
        /// Defaults to the location of the current directory
        /// </summary>
        /// <param name="DatabaseLocation"></param>
        public Database(string DatabaseName, string DatabaseLocation = null)
        {
            if (DatabaseLocation != null)
                DBLocation = DatabaseLocation;
            else
                DBLocation = Path.Combine(Directory.GetCurrentDirectory(), DatabaseName);

            Name = DatabaseName;
        }

        public void CheckDB()
        {
            if (!Directory.Exists(DBLocation))
                Directory.CreateDirectory(DBLocation);
        }
    }
}
