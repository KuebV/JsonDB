# JsonDB
JsonDB is a project that allows to store data in JSON format, while also having similarities to MongoDB

### Required Dependecies
```
- Newtonsoft.Json
```

## Code Example

### Making a new Database and Collection
```csharp
using JsonDB;

public static Database DB;
public static Collection Coll;

static void Main(string[] args)
{
  // Define the Database name, for the example it will be DatabaseName
  DB = new Database("DatabaseName");
  
  // Define the Collection, be sure also define the Database, also make a new Collection Name
  // StartupDictionary is when the Collection is initalized, it will load all the entrys into a Dictionary for easier access
  Coll = new Collection(DB, "ExampleCollection", true);
  
  // Makes sure that the Database is Loaded
  DB.CheckDB();
  
  // Initalize the Collection
  Coll.InitializeCollection();
}
```

### Inserting a new Document
**Defining the Cart for the new Document**
```csharp
public class Cart
{
  public string ID {get;set;}
  public int Total {get;set;}
}
```

**New Item**
```csharp
using JsonDB;

public static Database DB;
public static Collection Coll;
public static Item item;

static void Main(string[] args)
{
  DB = new Database("DatabaseName");
  Coll = new Collection(DB, "ExampleCollection", true);
  DB.CheckDB();
  Coll.InitializeCollection();
  
  var customerCart = new Cart
  {
    ID = "John",
    Total = 48
  };
  
  item = new Item(DB, Coll);
  item.AddItem("John", customerCart);
}
```

### Searching for an Item
**Finding an item using the ID**
```csharp
using JsonDB;

public static Database DB;
public static Collection Coll;
public static Item item;

static void Main(string[] args)
{
  DB = new Database("DatabaseName");
  Coll = new Collection(DB, "ExampleCollection", true);
  DB.CheckDB();
  Coll.InitializeCollection();
  
  // Define the Item Class
  item = new Item(DB, Coll);
  
  // Find the ID John in the Collection
  var person = item.FindItem("John");
  
  Console.WriteLine(person);
}
```

**Finding multiple items using a filter**
```csharp
using JsonDB;

public static Database DB;
public static Collection Coll;
public static Item item;

static void Main(string[] args)
{
  DB = new Database("DatabaseName");
  Coll = new Collection(DB, "ExampleCollection", true);
  DB.CheckDB();
  Coll.InitializeCollection();
  
  // Define Item Class
  item = new Item(DB, Coll);
  
  // If 48 is found in any of the Items in the Collection, return the Item
  Filter filter = new Filter(ItemValue:"48");
  
  // Use the predefined filter to create the List
  List<string> items = item.SearchMultiple(filter);
  
  // Print out each of the items that matches the filter
  foreach (var doc in items)
  {
    Console.WriteLIne(doc);
  }
}
```


