using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonDB
{
    public class Item
    {
        private Database Database;
        private Collection Collection;

        public Item(Database DB, Collection Collect)
        {
            Database = DB;
            Collection = Collect;
        }

        /// <summary>
        /// Find a single item in the collection that matches the ID given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JObject FindItem(string ID = null)
        {
            string IDFile = Path.Combine(Path.Combine(Database.DBLocation, Collection.DBCollectionName), string.Format("{0}.json", ID));
            if (!File.Exists(IDFile))
                return null;

            string textFile = File.ReadAllText(IDFile);
            JObject rss = JObject.Parse(textFile);
            return rss;
        }

        public List<string> SearchMultiple(Filter filter, bool KeepSearchingAfterFound = true)
        {
            string collectionFile = Path.Combine(Database.DBLocation, Collection.DBCollectionName);
            List<string> list = new List<string>();
            foreach (var file in Directory.EnumerateFiles(collectionFile))
            {

                // Look through each file to find the document that contains the specified filter
                if (filter.Field == null)
                {
                    string fileinfo = File.ReadAllText(file);
                    if (fileinfo.Contains(filter.FieldValue))
                    {
                        list.Add(fileinfo);
                    }
                }

            }
            return list;

        }

        /// <summary>
        /// Add an item to the selected Collection, by giving an ID to the Document, and a JsonDocument
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="JsonDoc"></param>
        public void AddItem(string ID, object JsonDoc)
        {
            string jsonSeralizer = JsonConvert.SerializeObject(JsonDoc);
            if (!File.Exists(Path.Combine(Path.Combine(Database.DBLocation, Collection.DBCollectionName), string.Format("{0}.json", ID))))
                File.WriteAllText(Path.Combine(Path.Combine(Database.DBLocation, Collection.DBCollectionName), string.Format("{0}.json", ID)), jsonSeralizer);
                
        }
    }
}
