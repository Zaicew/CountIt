using CountIt.App.Abstract;
using CountIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                lastId = 1;
            }
            return lastId;
        }
    }
}
