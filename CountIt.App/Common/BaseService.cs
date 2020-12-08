using CountIt.App.Abstract;
using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CountIt.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get; set; }
        public BaseService()
        {
            Items = new List<T>();
        }
        public T GetItemById(int id)
        {
            var entity = Items.FirstOrDefault(s => s.Id == id);
            return entity;
        }
        public int AddItem(T item)
        {
            Items.Add(item);
            return item.Id;
        }
        public List<T> GetAllItems()
        {
            return Items;
        }
        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }
        public int UpdateItem(T item)
        {
            var entity = Items.FirstOrDefault(s => s.Id == item.Id);
            if (entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }
        public int GetLastId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(s => s.Id).LastOrDefault().Id;
            }
            else
            {
                lastId = 0;
            }
            return lastId;
        }

        public void SaveListToXml(string name, string path)
        {
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute();
            xmlRootAttribute.ElementName = name;
            xmlRootAttribute.IsNullable = true;
            var serializer = new XmlSerializer(typeof(List<T>), xmlRootAttribute);

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                serializer.Serialize(streamWriter, Items);
            }
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //path += "\\" + "xxxxxxxDDDDD";
            //Directory.CreateDirectory(path);
            ////string path1 = Environment.GetFolderPath(Environment.SpecialFolderOption.Create);
            //string fileName = "itemslisttested";
            //string filePath2 = path + "\\" + fileName + ".xml";
            //var writer = new StreamWriter(filePath, false);
            //serializer1.Serialize(writer, listtest);
            //writer.Close();
            //Console.WriteLine(path);
        }

        public IEnumerable<T> ReadFromXml(string name, string path)
        {
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute();
            xmlRootAttribute.ElementName = name;
            xmlRootAttribute.IsNullable = true;
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), xmlRootAttribute);

            if (!File.Exists(path)) { return new List<T>(); }

            string xmlData = File.ReadAllText(path); 
            StringReader stringReader = new StringReader(xmlData);

            var items = (IEnumerable<T>)serializer.Deserialize(stringReader);
            return items;


            ////data for download
            //var listtestdownload = new List<Item>();
            //listtestdownload = listtest;

            ////empty data!
            //listtest.Clear();

            //foreach (var element in listtest)
            //{
            //    Console.WriteLine($"Name: {element.Name}, id: { element.Id}");
            //}
            //Console.WriteLine("2---------------------------------");

            ////download 

            ////static XmlRootAttribute SetNameOfNode(string nameOfNode)
            ////{
            ////    XmlRootAttribute xmlRootAttribute = new XmlRootAttribute();
            ////    xmlRootAttribute.ElementName = nameOfNode;
            ////    xmlRootAttribute.IsNullable = true;
            ////    return xmlRootAttribute;
            ////}
            ////XmlRootAttribute xmlRootAttribute = new XmlRootAttribute();
            ////xmlRootAttribute.ElementName = typeof(Item).Name;
            ////xmlRootAttribute.IsNullable = true;
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Item>), SetNameOfNode("costamdodano"));
            //string xml;
            //List<Item> objectsXml = new List<Item>();
            ////string pathdownload = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            //string pathdownload = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //pathdownload += "\\" + "xxxxxxxDDDDD";

            //string fileNamedownload = "itemslisttested";


            //xml = File.ReadAllText(pathdownload + @"\" + fileNamedownload + ".xml");

            //StringReader stringReader = new StringReader(xml);

            //objectsXml = (List<Item>)xmlSerializer.Deserialize(stringReader);

            //stringReader.Close();


            ////objectsXml = ConvertEnemiesByCategory(objectsXml);

            //foreach (var item in objectsXml)
            //{
            //    listtest.Add(new Item(item.Id, item.Name, item.Kcal, item.Fat, item.Protein, item.Carb, item.CategoryId));
            //}
            //foreach (var element in listtest)
            //{
            //    Console.WriteLine($"Name: {element.Name}, id: { element.Id}");
            //}
            //Console.WriteLine("3---------------------------------");

        }
    }
}
