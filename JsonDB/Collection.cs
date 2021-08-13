using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDB
{
    public class Collection
    {
        private Database DB;
        public string DBCollectionName;
        private bool StartupDictionaryEnabled;

        private Dictionary<string, string> DBDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Reference a new Collection, referencing the Database Class, make a new Collection Name, along with the optional variable of the StartupDictionary
        /// </summary>
        /// <param name="Database"></param>
        /// <param name="CollectionName"></param>
        /// <param name="StartupDictionary"></param>
        public Collection(Database Database, string CollectionName, bool StartupDictionary = false)
        {
            DB = Database;
            DBCollectionName = CollectionName;
            StartupDictionaryEnabled = StartupDictionary;
        }

        /// <summary>
        /// Initalize the Collection, will create the collection if it doesn't already exist at the specfied location
        /// </summary>
        public void InitializeCollection()
        {
            Log Log = new Log(DB.Name, DBCollectionName);
            string CollectionLocation = Path.Combine(DB.DBLocation, DBCollectionName);

            if (!Directory.Exists(CollectionLocation)){
                Log.Information("Creating Collection : " + DBCollectionName);
                Directory.CreateDirectory(CollectionLocation);
            }

            if (StartupDictionaryEnabled)
            {
                Log.Information("Adding Data to Dictionary, this may take a while...");
                foreach (var file in Directory.EnumerateFiles(CollectionLocation))
                {
                    string fileText = File.ReadAllText(file);
                    DBDictionary.Add(file.ToString(), fileText);
                }
            }
        }

        /// <summary>
        /// Retreive the Dictionary of the Database
        /// If the Dictionary is empty it will return an error
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> DatabaseDictionary()
        {
            Log log = new Log(DB.Name, DBCollectionName);
            if (DBDictionary.Count < 1)
            {
                log.Error("Dictionary is empty!");
                return null;
            }

            return DBDictionary;
        }
    }
}
