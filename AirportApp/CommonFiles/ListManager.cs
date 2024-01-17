using System.Collections.Generic;
using System.Linq;

namespace AirportApp.CommonFiles;

public class ListManager<T> : IListManager<T>
{
    private List<T> itemList;

    /// <summary>
    /// This constructor intializes the new <see cref="ListManager{T}"/> object list.
    /// </summary>
    public ListManager()
    {
        itemList = new List<T>();
    }

    /// <summary>
    /// Adds <see cref="T"/> object to list.
    /// </summa
    public virtual bool Add(T item)
    {
        bool result = false;

        if (itemList != null && item != null)
        {
            itemList.Add(item);
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Removes all objects from list.
    /// </summary>
    public void DeleteAll()
    {
        itemList.Clear();
    }

    /// <summary>
    /// Returns <see cref="T"/> object from list.
    /// </summary>
    public T GetItem(int index)
    {
        return itemList[index];
    }

    /// <summary>
    /// Returns an array of string from list.
    /// </summary>
    public string[] GetItemArray()
    {
        return itemList.Select(item => item.ToString()).ToArray();
    }

    ///<summary>
    ///Returns and sets itemList
    /// </summary>
    public List<T> GetListItems
    {
        get { return itemList; }
    }

    /// <summary>
    /// Removes an item at index.
    /// </summary>
    public bool Remove(int index)
    {
        bool result = false;

        if (itemList != null && index >= 0)
        {
            itemList.RemoveAt(index);
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Removes all occurrences of string.
    /// </summary>
    public bool RemoveAll(string strIn)
    {
        bool result = false;

        if (itemList != null)
        {
            itemList.RemoveAll(item => item.ToString() == strIn);
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Removes all occurrences of string.
    /// </summary>
    public bool Contains(T item)
    {
        return itemList.Contains(item);
    }

    /// <summary>
    /// Returns length of list.
    /// </summary>
    public int Count
    {
        get { return itemList.Count; }
    }

    /// <summary>
    /// Replaces object in list.
    /// </summary>
    public bool Replace(int index, T item)
    {
        bool result = false;

        if (itemList != null && item != null && index < itemList.Count && itemList.Count != 0)
        {
            itemList[index] = item;
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Returns list of strings.
    /// </summary>
    public List<string> GetItemList()
    {
        return itemList.Select(item => item.ToString()).ToList();
    }

    /// <summary>
    /// Add items of a list to another
    /// </summary>
    /// <param name="items"></param>
    public void AddRange(List<T> items)
    {
        itemList.AddRange(items);
    }
}