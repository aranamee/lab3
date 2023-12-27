using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab3
{
    class Catalog
    {
        private List<Composition> compositions = new List<Composition>();

        public void AddComposition(Composition composition)
        {
            // Проверка наличия композиции с таким ID в списке
            while (compositions.Any(c => c.Id == composition.Id))
            {
                composition = new Composition(); // Увеличиваем ID, если уже существует композиция с таким ID
            }

            compositions.Add(composition);
        }

        public void RemoveComposition(Composition composition)
        {
            compositions.Remove(composition);
        }

        public List<Composition> Search(string criteria)
        {
            // Пример простого поиска по критерию в названии или исполнителе
            return compositions.FindAll(c =>
                c.Artist.Contains(criteria, StringComparison.OrdinalIgnoreCase) ||
                c.Title.Contains(criteria, StringComparison.OrdinalIgnoreCase));
        }

        public List<Composition> Search(string artistCriteria, string TitleCriteria)
        {
            // Пример простого поиска по критерию в названии или исполнителе
            return compositions.FindAll(c =>
                c.Artist.Contains(artistCriteria, StringComparison.OrdinalIgnoreCase) ||
                c.Title.Contains(TitleCriteria, StringComparison.OrdinalIgnoreCase));
        }

        public void DisplayAllCompositions()
        {
            if (compositions.Count > 0)
            {
                Console.WriteLine("All composiotion in catalog:");
                foreach (var composition in compositions)
                {
                    Console.WriteLine(composition);
                }
            }
            else
            {
                Console.WriteLine("Catalog empty");
            }
        }

        public void ExportToSql()
        {
            using (var dbContext = new AppDbContext())
            {
                var allContacts = dbContext.Compositions.ToList();
                dbContext.Compositions.RemoveRange(allContacts);
                dbContext.SaveChanges();

                dbContext.Compositions.AddRange(compositions);
                dbContext.SaveChanges();
            }
        }
        public void ExportToJson()
        {
            string jsonString = JsonSerializer.Serialize(compositions);
            File.WriteAllText("Composition.json", jsonString);
        }
        public void ExportToXml()
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Composition>));
            StreamWriter myWriter = new StreamWriter("Composition.xml");
            mySerializer.Serialize(myWriter, compositions);
            myWriter.Close();
        }
        public void ImportFromJson()
        {
            string jsonString = File.ReadAllText("Composition.json");
            compositions = JsonSerializer.Deserialize<List<Composition>>(jsonString);
        }
        public void ImportFromXml()
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Composition>));
            using var myFileStream = new FileStream("Composition.xml", FileMode.Open);
            compositions = (List<Composition>)mySerializer.Deserialize(myFileStream);
        }
        public void ImportFromSql()
        {
            using (var dbContext = new AppDbContext())
            {
                compositions = dbContext.Compositions.ToList();

            }
        }
    }
}
