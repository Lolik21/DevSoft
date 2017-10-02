using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEpam
{
    public interface IListItem //элемент списка
    {
        IListItem Prev(); //предыдущий элемент
        IListItem Next(); //следующий элемент
        object Value { get; } //значение, хранимое в элементе
    }

    public interface ILinkedList //двусвязный список
    {
        void AddFirst(IListItem item); //добавить элемент в начало
        void AddLast(IListItem item); //добавить элемент в конец
        void Insert(IListItem item, int index); //вставить элемент по индексу или в начало, если список пуст
        bool IsEmpty(); //проверка пустой ли список
        IListItem GetFirstItem(); //получить первый "корневой" элемент списка
        IEnumerable<IListItem> GetAll(); //получить все элементы, кроме корневого
        void Clear(); //очистить список
        void Reverse(); //отсортировать список в обратном порядке и перестроить связи
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            ListItem item = new ListItem("1");
            ListItem item1 = new ListItem("2");
            ListItem item2 = new ListItem("3");
            ListItem item3 = new ListItem("4");
            ListItem item4 = new ListItem("5");
            ListItem item5 = new ListItem("6");


            linkedList.AddFirst(item);
            linkedList.AddFirst(item1);
            linkedList.AddFirst(item2);
            linkedList.AddFirst(item3);
            linkedList.AddFirst(item4);
            linkedList.AddFirst(item5);

            IEnumerable<IListItem> list = linkedList.GetAll();

            foreach(IListItem i in list)
            {
                Console.WriteLine(i.Value);
            }


            Console.ReadLine();
        }
    }

    public class LinkedList : ILinkedList
    {
        private ListItem ListHead;

        //добавить элемент в начало
        public void AddFirst(IListItem item)
        {
            if (ListHead == null)
            {
                ListHead = new ListItem(item.Value);
            }
            else
            {
                ListItem NewItem = new ListItem(item.Value);
                ListHead.PrevElem = item;
                NewItem.NextElem = ListHead;
                ListHead = NewItem;
            }
        }

        //добавить элемент в конец
        public void AddLast(IListItem item)
        {
            if (ListHead == null)
            {
                ListHead = new ListItem(item.Value);
            }
            else
            {
                IListItem CurrItem = ListHead;
                while (CurrItem != null)
                {
                    if (CurrItem.Next() == null)
                    {
                        ListItem NewItem = new ListItem(item.Value, CurrItem);                        
                        ListItem Item = CurrItem as ListItem;
                        Item.NextElem = NewItem;
                    }
                    CurrItem = CurrItem.Next();
                }
            }
        }

        //вставить элемент перед элементом с указанным индексом
        //если элемента нет - вставить в конец
        public void Insert(IListItem item, int index)
        {
            if (index < 0 )
            {
                return;
            }
            if (index == 0)
            {
                AddFirst(item);
                return;
            }
            IListItem CurrItem = ListHead;
            for (int i = 0; i < index - 1; i++)
            {
                CurrItem = CurrItem.Next();
            }
            if ((CurrItem == null) || (CurrItem.Next() == null))
            {
                AddLast(item);
                return;
            }

            ListItem NewItem = new ListItem(item.Value, CurrItem);
            NewItem.NextElem = CurrItem.Next();

            ListItem PrevItem = CurrItem as ListItem;
            ListItem NextItem = CurrItem.Next() as ListItem;
            PrevItem.NextElem = NewItem;
            NextItem.PrevElem = NewItem;             
        }

        //проверка есть ли эелементы в списке
        public bool IsEmpty()
        {
            if (ListHead == null)
            {
                return true;
            }
            return false;
        }

        //вернуть первый эелемент в списке
        public IListItem GetFirstItem()
        {
            return ListHead;
        }

        //вернуть все элементы списка, кроме первого
        public IEnumerable<IListItem> GetAll()
        {
            List<IListItem> Items = new List<IListItem>();
            IListItem CurrItem = ListHead;
            while (CurrItem != null)
            {
                Items.Add(CurrItem);
                CurrItem = CurrItem.Next();
            }
            return Items;
        }

        //очистить список
        public void Clear()
        {
            ListHead = null;
        }

        //сортировка списка в обратном порядке
        public void Reverse()
        {
            IListItem CurrItem = ListHead;
            while (CurrItem != null)
            {
                ListItem item = CurrItem as ListItem;
                IListItem Temp = item.NextElem;
                item.NextElem = item.PrevElem;
                item.PrevElem = Temp;
                if (CurrItem.Next() == null)
                {
                    ListHead = CurrItem as ListItem;
                }
                CurrItem = CurrItem.Next();
            }
        }
    }

    //элемент связанного списка
    public class ListItem : IListItem
    {
        public IListItem PrevElem { get; set; }
        public IListItem NextElem { get; set; }

        public ListItem(object obj, IListItem prev = null)
        {
            this.Value = obj;
            PrevElem = prev;
            NextElem = null;
        }

        //предыдущий связанный элемент списка
        public IListItem Prev()
        {
            return PrevElem;
        }

        //следующий связанный элемент списка
        public IListItem Next()
        {
            return NextElem;
        }

        //хранимое значение
        public object Value { get; }
    }

}
