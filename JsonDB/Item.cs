using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonDB
{
    public class Item
    {
        private Database Database;
        private Collection Collection;

        private string ItemHomeFolder => Path.Combine(Database.DBLocation, Collection.DBCollectionName);

        private Log Log;

        public Item(Database DB, Collection Collect)
        {
            Database = DB;
            Collection = Collect;

            Log = new Log(DB.Name, Collect.DBCollectionName);
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
            {
                Log.Error($"[Item : {ID}] does not exist in Collection!");
                return null;
            }

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
                else
                {
                    string text = File.ReadAllText(file);
                    JObject obj = JObject.Parse(text);
                    if (obj[filter.Field].Equals(filter.FieldValue))
                        list.Add(text);
                }
            }
            return list;

        }


        /// <summary>
        /// Update a Item when given an ID
        /// Example : item.UpdateItem("439742", UpdateOptions.Set, "Age", 23)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Options"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldValue"></param>
        public void UpdateItem(string ID, UpdateOptions Options, string FieldName, object FieldValue)
        {
            if (!File.Exists(Path.Combine(ItemHomeFolder, string.Format("{0}.json", ID))))
                return;

            JObject jobj = JObject.Parse(Path.Combine(ItemHomeFolder, string.Format("{0}.json", ID)));

            // We know that the Item exists in the Collection
            switch (Options)
            {
                case UpdateOptions.Set:
                    jobj[FieldName] = (JToken)FieldValue;
                    string serializeFile = JsonConvert.SerializeObject(jobj, Formatting.Indented);
                    File.WriteAllText(Path.Combine(ItemHomeFolder, string.Format("{0}.json", ID)), serializeFile);
                    break;
                case UpdateOptions.Append:
                    JArray ja = (JArray)jobj[FieldName];
                    ja.Add(FieldValue);

                    string file = JsonConvert.SerializeObject(jobj, Formatting.Indented);
                    File.WriteAllText(Path.Combine(ItemHomeFolder, string.Format("{0}.json", ID)), file);
                    break;

            }

        }


        /// <summary>
        /// Add Multiple Items into a Collection
        /// </summary>
        /// <param name=""></param>
        public void MassAddItem(Dictionary<string, object> Items)
        {
            foreach (var item in Items)
            {
                if (!File.Exists(Path.Combine(ItemHomeFolder, string.Format("{0}.json", item.Key))))
                {
                    string jsonData = JsonConvert.SerializeObject(item.Value);
                    File.WriteAllText(Path.Combine(ItemHomeFolder, string.Format("{0}.json", item.Key)), jsonData);
                }

                Log.Error($"Item with ID: {item.Key} already exists in collection!");
                    
            }
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

    public enum UpdateOptions
    {
        Set = 1,
        Append = 2
    }
}
