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
        }
    }
}
