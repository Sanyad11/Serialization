using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Serialization
{
    public class Serializing
    {
        public void SerialiazingJSON()
        {
            try
            {

            Animal animal1 = new Animal("Tom", 5, new AnimalType("Cat"));
            Animal animal2 = new Animal("Guffy", 8, new AnimalType("Dog"));
            Animal[] animals = new Animal[] { animal1, animal2 };

            JsonSerializer serializer = new JsonSerializer();

            serializer.NullValueHandling = NullValueHandling.Ignore;
            DirectoryInfo[] cDirs = new DirectoryInfo(Directory.GetCurrentDirectory()).GetDirectories();
            using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\animal.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, animals);
            }

            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
        }

        public void DeserializationJSON()
        {
            try { 
            JsonSerializer serializer = new JsonSerializer();

            DirectoryInfo[] cDirs = new DirectoryInfo(Directory.GetCurrentDirectory()).GetDirectories();
           using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\animal.json"))
            {
                Animal[] animal = JsonConvert.DeserializeObject<Animal[]>(sr.ReadToEnd());
                for (int i = 0; i < animal.Length; i++)
                {
                    Console.WriteLine($"Name: {animal[i].Name}\tAge: {animal[i].Age}\tType: {animal[i].AnimalType.Name}");
                }
                
            }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
        }

        public void SerializingXML()
        {
            try { 
            Animal animal1 = new Animal("Jack", 2, new AnimalType("Sparrow"));
            Animal animal2 = new Animal("Simba", 4, new AnimalType("Lion"));
            Animal[] animals = new Animal[] { animal1, animal2 };

            XmlSerializer formatter = new XmlSerializer(typeof(Animal[]));

            using (FileStream fs = new FileStream("animal.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, animals);
            }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
        }

        public void DeserializationXML()
        {
            try { 
            XmlSerializer formatter = new XmlSerializer(typeof(Animal[]));

            using (FileStream fs = new FileStream("animal.xml", FileMode.OpenOrCreate))
            {
                Animal[] newAnimal = (Animal[])formatter.Deserialize(fs);

                foreach (Animal a in newAnimal)
                {
                    Console.WriteLine($"Name: {a.Name} --- Age: {a.Age} --- Type: {a.AnimalType.Name}");
                }
            }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
        }
    }

    [Serializable]
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public AnimalType AnimalType { get; set; }

        public Animal()
        { }

        public Animal(string Name, int Age, AnimalType AnimalType)
        {
            this.Name = Name;
            this.Age = Age;
            this.AnimalType = AnimalType;
        }
    }

    [Serializable]
    public class AnimalType
    {
        public string Name { get; set; }
        
        public AnimalType() { }

        public AnimalType(string Name)
        {
            this.Name = Name;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool isNotExit = true;
            Serializing serializing = new Serializing();

            while (isNotExit)
            {
                Console.WriteLine("1. Save as xml");
                Console.WriteLine("2. Save as json");
                Console.WriteLine("3. Load from xml");
                Console.WriteLine("4. Load from json");
                Console.WriteLine("5. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        serializing.SerializingXML();
                        break;
                    case "2":
                        serializing.SerialiazingJSON();
                        break;
                    case "3":
                        serializing.DeserializationXML();
                        break;
                    case "4":
                        serializing.DeserializationJSON();
                        break;
                    case "5":
                        isNotExit = false;
                        break;
                    default:
                        Console.WriteLine("Wrong answer!");
                        break;
                }
            }
        }
    }
}