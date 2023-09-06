using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinkedListLib
{
    public class MyLinkedList<T> : ICollection<T>, IEnumerable<T>, IComparable, ICloneable
    {
        public Item<T> Head { get; private set; }
        public Item<T> Tail { get; private set; }
        public int Count { get; private set; }

        public MyLinkedList()
        {
            Head = null;
            Tail = null;
            Count = 0;

        }

        public MyLinkedList(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                AddFirst(arr[i]);
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void AddLast(T value)
        {
            Item<T> item = new Item<T>(value);
            AddLast(item);
        }

        public void AddLast(Item<T> item)
        {
            if (Tail == Head && Tail != null)
            {
                Tail = item;
                Head.Next = Tail;
                Tail.Prev = Head;
                Tail.Next = null;
            }
            else if (Tail == null)
            {
                Tail = item;
                Tail.Next = null;
                Head = item;
                Head.Prev = null;
            }
            else
            {
                Item<T> PreviousLast = Tail;
                Tail = item;
                PreviousLast.Next = item;
                item.Prev = PreviousLast;
            }
            Count++;
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        public void AddFirst(T value)
        {
            Item<T> item = new Item<T>(value);
            AddFirst(item);
        }
        public void AddFirst(Item<T> item)
        {
            if (Head == Tail && Head != null)
            {
                Head = item;
                Head.Next = Tail;
                Tail.Prev = Head;
                Head.Prev = null;
                Tail.Next = null;
            }
            else if (Head == null)
            {
                Head = item;
                Tail = item;
                Head.Prev = null;
                Tail.Next = null;
            }
            else
            {
                Item<T> PreviousFirst = Head;
                Head = item;
                PreviousFirst.Prev = item;
                item.Next = PreviousFirst;
            }
            Count++;
        }

        public void RemoveFirst()
        {
            if (Head != null)
            {
                Head = Head.Next;
                Head.Prev = null;
                Count--;
            }
        }

        public void RemoveLast()
        {
            if (Tail != null)
            {
                Tail = Tail.Prev;
                Tail.Next = null;
                Count--;
            }
        }

        public void AddBefore(Item<T> item, T value)
        {
                Item<T> new_item = new Item<T>(value);
                new_item.Next = item;
                item.Prev = new_item;
                if (Head != item)
                {
                    Item<T> old_previous = item.Prev;
                    old_previous.Next = new_item;
                    new_item.Prev = old_previous;
                }
                else
                    Head = new_item;
                Count++;
        }

        public Item<T> Find(T data)
        {
            var item = Head;

            while (item.Next != null)
            {
                if (item.Data.Equals(data))
                {
                    return item;
                }
                else
                {
                    item = item.Next;
                }
            }

            if (item.Data.Equals(data))
            {
                return item;
            }

            return null;
        }

        public bool Contains(T data)
        {
            var item = Head;

            while (item != null)
            {
                if (item.Data.Equals(data))
                {
                    return true;
                }
                else
                {
                    item = item.Next;
                }
            }

            return false;
        }

        public bool Remove(Item<T> item)
        {
            if (item != null)
            {
                item.Next.Prev = item.Prev;
                item.Prev.Next = item.Next;
                Count--;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Remove(T value)
        {
            if (Find(value) == null)
                return false;
            else
            {
                Remove(Find(value));
                return true;
            }
        }

        public Item<T> FindLast(T data)
        {
            var item = Tail;

            while (item.Prev != null)
            {
                if (item.Data.Equals(data))
                {
                    return item;
                }
                else
                {
                    item = item.Prev;
                }
            }

            if (item.Data.Equals(data))
            {
                return item;
            }

            return null;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public void AddAfter(Item<T> item, T value)
        {
                Item<T> new_item = new Item<T>(value);
                new_item.Prev = item;
                item.Next = new_item;
                if (Tail != item)
                {
                    Item<T> old_next = item.Next;
                    new_item.Next = old_next;
                    old_next.Prev = new_item;
                }
                else
                    Tail = new_item;
                Count++;
        }

        public object Clone()
        {
            MyLinkedList<T> ClonedList = new MyLinkedList<T>();
            foreach (var value in this)
            {
                ClonedList.AddLast(value);
            }
            return ClonedList;
        }

        public int CompareTo(object obj)
        {
            if (obj == null || (obj.GetType() != this.GetType()))
            {
                throw new Exception();
            }
            MyLinkedList<T> obj1 = obj as MyLinkedList<T>;
            if (Count < obj1.Count) return -1;
            if (Count > obj1.Count) return 1;
            return 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Head == null)
                yield break;
            Item<T> item = Head;
            do
            {
                yield return item.Data;
                item = item.Next;
            } while (item != null);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + Count)
            {
                throw new IndexOutOfRangeException();
            }
            foreach (var value in this)
            {
                array[arrayIndex] = value;
                arrayIndex++;
            }
        }

        public void Sort(IComparer<T> comparer)
        {
            T[] array = new T[Count];
            CopyTo(array, 0);
            Array.Sort(array, comparer);
            Item<T> item = Head;
            for (int i = 0; i < Count; i++)
            {
                item.Data = array[i];
                item = item.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
