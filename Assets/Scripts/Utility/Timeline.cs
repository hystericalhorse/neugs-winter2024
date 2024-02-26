using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Timeline<T>
{
	[SerializeField] protected List<T> timeline = new();
	protected uint index = 0;

    public T TryGetFirst(bool dequeItem = false)
    {
        try
        {
            var item = timeline.First();

            if (dequeItem)
				Remove(item);

			return item ?? default;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return default;
        }
    }

	public T TryGetLast(bool dequeItem = false)
	{
		try
		{
			var item = timeline.Last();

			if (dequeItem)
				Remove(item);

			return item ?? default;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			return default;
		}
	}

	public List<T> Get() => timeline;
	public void Clear() => (timeline as ICollection<T>)?.Clear();
	public void Add(T item) => (timeline as ICollection<T>)?.Add(item);
	public void Remove(T item) => (timeline as ICollection<T>)?.Remove(item);
	public bool IsEmpty() => (timeline as ICollection<T>)?.Count == 0;
}