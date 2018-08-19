using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_schedule
{
    public class LinkedList : IEnumerable  // односвязный список
    {
        private Node head = null; // головной/первый элемент
        private Node tail = null; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        // добавление элемента
        public void Add(Train data)
        {
            Node node = new Node(data);

            if (head == null) head = node;
            else tail.next = node;
            tail = node;
            count++;
        }

        // Проверка заполнен ли список
        public bool ListIsEmpty()
        {
            Node current = head;
            if (current == null)
                return true;
            else
                return false;
        }

        public string ReadLettersFromConsole()
        {
            string result = "";
            while (true)
            {
                var k = Console.ReadKey(true);

                switch (k.Key)
                {
                    case ConsoleKey.Backspace:
                        if (result.Length > 0)
                        {
                            int cursorCol = Console.CursorLeft - 1;
                            result = result.Remove(startIndex: result.Length - 1, count: 1);
                            Console.CursorLeft = 0;
                            Console.Write(result + new String(' ', 1));
                            Console.CursorLeft = cursorCol;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        {
                            Console.Write(value: k.KeyChar);
                            result += k.KeyChar;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return result;
                    default:
                        if (char.IsLetter(c: k.KeyChar))
                        {
                            Console.Write(value: k.KeyChar);
                            result += k.KeyChar;
                        }
                        break;
                }
            }
        }

        // Проверка ввода: пропускаем только числа
        public int InputParseDigital()
        {
            int num;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out num) && num > 0)
                    break;
                else
                    Console.WriteLine("Неверный ввод");
            }
            return num;
        }

        // Проверка ввода: пропускаем только числа разделенных двоеточием
        public string InputParseTime()
        {
            char[] ch = { '0','1','2','3','4','5'};
            int i = 0;
            string str = "";
            //string result = "";
            do
            {
                var k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.Backspace:
                        if (str.Length > 0)
                        {
                            int cursorCol = Console.CursorLeft - 1;
                            str = str.Remove(startIndex: str.Length - 1, count: 1);
                            Console.CursorLeft = 0;
                            Console.Write(str + new String(' ', 1));
                            Console.CursorLeft = cursorCol;
                            i--;
                        }
                        break;
                    default:
                        
                        if (char.IsDigit(c: k.KeyChar) && i == 0 && (k.KeyChar == '0' || k.KeyChar == '1' || k.KeyChar == '2'))
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if ((str.Contains("0") || str.Contains("1")) && char.IsDigit(c: k.KeyChar) && i == 1)
                        {
                            Console.Write(value: k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (str.Contains("2") && char.IsDigit(c: k.KeyChar) && i == 1 && (k.KeyChar == '0' || k.KeyChar == '1' || k.KeyChar == '2' || k.KeyChar == '3' || k.KeyChar == '4'))
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (i == 2 && k.KeyChar == ':')
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (!str.Contains("24") && char.IsDigit(c: k.KeyChar) && i == 3 && ch.Contains(k.KeyChar))
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (str.Contains("24") && char.IsDigit(c: k.KeyChar) && k.KeyChar == '0' && (i == 3 || i == 4))
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (!str.Contains("24") && char.IsDigit(c: k.KeyChar) && i == 4)
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }

                        break;
                }
            } while (i != 5);
            return str;
        }

        // Ввод нового поезда
        public void InputData(Train train)
        {
            int n;
            //Train train2 = new Train();
            do
            {
                Console.WriteLine("Введите пункт назначения: ");
                train.destination = ReadLettersFromConsole();
                if (train.destination == "")
                    Console.WriteLine("Введено пустое поле! Повторите ввод.");
            } while (train.destination == "");

            do
            {
                Console.WriteLine("Введите номер поезда: ");
                n = InputParseDigital();
                if (Program.list.Contains(n))
                    Console.WriteLine("Нельзя вводить поезда с одинаковыми номерами! Повторите ввод.");
            } while (Program.list.Contains(n));
            train.number = n;
            Console.WriteLine("Введите время отправления (в формате чч:мм, где ч - часы, м - минуты): ");
            train.date = InputParseTime();
        }

        // Вывод всего списка
        public void PrintData(LinkedList list)
        {
            Console.WriteLine("Расписание поездов: ");
            foreach (Train item in list)
            {
                Console.WriteLine("\nПункт назначения: {0}", item.destination);
                Console.WriteLine("Номер поезда: {0}", Convert.ToString(item.number));
                Console.WriteLine("Время отправления: {0}", item.date);
            }
            Console.WriteLine();
        }

        // Поиск элемента
        public bool Contains(int Number)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.number.Equals(Number))
                {
                    return true;
                }
                current = current.next;
            }
            return false;
        }

        // Поиск элемента и передача данных о поезде в случае нахождения его в расписании
        public Node FindTrain(int Number)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.number.Equals(Number))
                {
                    return current;
                }
                current = current.next;
            }
            return null;
        }

        // Редактирование элемента 
        public bool EditTrain(int Number)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.number.Equals(Number))
                {
                    Program.list.InputData(current.Data);
                    return true;
                }
                current = current.next;
            }
            return false;
        }

        // Удаление элемента
        public bool Remove(int Number)
        {
            Node current = head;
            Node previous = null;

            while (current != null)
            {
                if (current.Data.number.Equals(Number))
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

        // Запись в файл всего списка
        public void WriteInFile(LinkedList list)
        {
            string text = "";
            // запись в файл
            using (FileStream fstream = new FileStream(@"E:\Unity\note.txt", FileMode.Create))
            {
                foreach (Train item in list)
                {
                    text = item.destination + " " + Convert.ToString(item.number) + " " + item.date + " ";
                    byte[] array = Encoding.Default.GetBytes(text);// преобразуем строку в байты
                    fstream.Write(array, 0, array.Length);// запись массива байтов в файл
                }
                Console.WriteLine("Текст записан в файл");
            }
        }

        // Чтение из фала всего списка
        public void ReadFile()
        {
            string text;
            string[] currentLine;
            // чтение из файла
            using (StreamReader fstr_in = new StreamReader(@"E:\Unity\note.txt", Encoding.GetEncoding(1251)))
            {
                while ((text = fstr_in.ReadLine()) != null)
                {
                    currentLine = text.Split(' ');

                    for (int element = 0; element < currentLine.Length - 1; element++)
                    {
                        Train train = new Train
                        {
                            destination = currentLine[element],
                            number = Int32.Parse(currentLine[element + 1]),
                            date = currentLine[element + 2]
                        };
                        if (Program.list.Contains(train.number))
                            element += 2;
                        else
                        {
                            element += 2;
                            Program.list.Add(train);
                        }
                    }
                }
            }
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
