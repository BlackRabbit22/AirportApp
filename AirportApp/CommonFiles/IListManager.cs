using System.Collections.Generic;

namespace AirportApp.CommonFiles;

/// <summary>
///  IListManager interface
/// </summary>
internal interface IListManager<T>
{
    public List<T> GetListItems { get; }

    public bool Add(T item);

    public bool Remove(int index);

    public bool RemoveAll(string index);

    public bool Replace(int index, T item);

    public T GetItem(int index);

    public bool Contains(T item);

    public void DeleteAll();

    public string[] GetItemArray();

    public List<string> GetItemList();

    public void AddRange(List<T> items);
}