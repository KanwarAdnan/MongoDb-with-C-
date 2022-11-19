using System;
using MongoDB.Driver;
using MongoDB.Bson;

public class Database{
    private string url = "";
    MongoClient? client = null;
    MongoDB.Driver.IMongoDatabase? database = null;
    MongoDB.Driver.IMongoCollection<BsonDocument>? collection = null;

    public Database(string connectionUrl = "mongodb://127.0.0.1:27017" , string database =  "database" , string collection = "person"){
        this.url = connectionUrl;
        this.client = new MongoClient(this.url);
        this.database = this.client.GetDatabase(database);
        this.collection = this.database.GetCollection<BsonDocument>(collection);
    }

    public List<BsonDocument> fetchAll(){
        List<BsonDocument> result = this.collection.Find(new BsonDocument()).ToList();  
        return result;
    }

    public void print(){
        List<BsonDocument> result = this.fetchAll();
            foreach (var item in result){  
                Console.WriteLine(item.ToString()); 
            }
    }

    private BsonDocument getRecord(string name , int age){
        BsonDocument record = new BsonDocument();
        record["name"] = name;
        record["age"] = age;
        return record;
    }

    public void InsertOne(string name , int age){
        if (this.collection != null){
            BsonDocument record = getRecord(name , age);
            this.collection.InsertOne(record); 
        }
        else {
            Console.WriteLine("Specify the collection first!");
        }
    }

    public void deleteOne(string name , int age){
        if (this.collection != null){
            BsonDocument record = getRecord(name , age);
            this.collection.DeleteOne(record);
        }
        else {
            Console.WriteLine("Specify the collection first!");
        }
    }
    public void updateOne(BsonDocument key , string name , int age){
        // Sorry I don't really for now how to update database in C#.
        // But I will figure out it soon.
        /*
        if (this.collection != null){
            BsonDocument record = getRecord(name , age);
            this.collection.UpdateOne(key,record);
        }
        else {
            Console.WriteLine("Specify the collection first!");            
        }
        */
    }


}

public class CURD{
    public static void Main(string [] args){
        // Creating Database
        Database database = new Database();

        // Adding
        database.InsertOne("Adnan",22);
        database.InsertOne("Kanwar",22);

        // Reading
        database.print();

        // Deleting
        database.deleteOne("Adnan",22);
        Console.WriteLine("Document deleted!");

        database.print();
   }
}
