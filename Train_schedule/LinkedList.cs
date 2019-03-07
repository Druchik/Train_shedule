using System.Collections;

namespace Train_schedule
{
    /// <summary>
    /// Односвязный список
    /// </summary>
    public class LinkedList : IEnumerable  // односвязный список
    {
        /// <summary>
        /// Первый элемент
        /// </summary>
        private Node head = null;

        /// <summary>
        /// Последний элемент
        /// </summary>
        private Node tail = null;

        /// <summary>
        /// Количество элементов в списке
        /// </summary>
        int count; 

        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="data"></param>
        public void Add(Train data)
        {
            Node node = new Node(data);

            if (head == null) head = node;
            else tail.next = node;
            tail = node;
            count++;
        }

        /// <summary>
        /// Проверка списка на пустоту
        /// </summary>
        /// <returns></returns>
        public bool ListIsEmpty()
        {
            Node current = head;
            if (current == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Поиск элемента
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public bool Contains(int num)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.Number.Equals(num))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }

        /// <summary>
        /// Поиск элемента и передача данных о поезде в случае нахождения его в расписании
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Node FindTrain(int num)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.Number.Equals(num))
                {
                    return current;
                }
                current = current.next;
            }
            return null;
        }

        /// <summary>
        /// Редактирование элемента
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool EditTrain(int num)
        {
            var item = FindTrain(num);

            if (item != null)
            {
                Program.InputData(item.Data);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool Remove(int num)
        {
            Node current = head;
            Node previous = null;

            while (current != null)
            {
                if (current.Data.Number.Equals(num))
                {
                    // Если узел в середине или в конце
                    if (previous != null)
                    {
                        previous.next = current.next;
                        if (current.next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.next;
                        if (head == null)
                            tail = null;
                    }
                    count--;
                    return true;
                }
                previous = current;
                current = current.next;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.next;
            }
        }
    }
}
