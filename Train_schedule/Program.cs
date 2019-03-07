using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Train_schedule
{
    class Program
    {
        private const string EMPTY_LIST = "Список пуст! Заполните его.";
        private const string WAITING_FOR_ENTER = "Нажмите Enter для продолжения...";

        public static LinkedList list = new LinkedList();

        static void Main(string[] args)
        {
            int n = -1;

            while (n != 0)
            {
                Console.WriteLine("\n  *** Расписание поездов ***");
                Console.WriteLine("     ******* Меню *******\n");
                Console.WriteLine("1 - Добавить запись");
                Console.WriteLine("2 - Вывести список");
                Console.WriteLine("3 - Поиск поезда по его номеру");
                Console.WriteLine("4 - Удаление");
                Console.WriteLine("5 - Редактировать");
                Console.WriteLine("6 - Сохранить в файл");
                Console.WriteLine("7 - Считать из файла");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("Выберите нужный пункт меню: ");
                while (true)
                {
                    if (Int32.TryParse(Console.ReadLine(), out n))
                        break;
                    else
                        Console.WriteLine("Неверный ввод");
                }

                switch (n)
                {
                    case 1: //добавление записи
                    {
                        Train train = new Train();
                        InputData(train);
                        list.Add(train);
                        break;
                    }
                    case 2: //вывод всего списка (расписания)
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine(EMPTY_LIST);
                        else
                            PrintData(list);
                        Console.Write(WAITING_FOR_ENTER);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    case 3: //поиск позда по номеру
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine(EMPTY_LIST);
                        else
                        {
                            Console.WriteLine("Введите номер поезда для поиска: ");
                            int number = InputParseDigital();
                            Node nd = list.FindTrain(number);
                            if (nd != null)
                            {
                                Console.WriteLine("Найденный поезд: ");
                                Console.WriteLine("Пункт назначения: {0}", nd.Data.Destination);
                                Console.WriteLine("Номер поезда: {0}", Convert.ToString(nd.Data.Number));
                                Console.WriteLine("Время отправления: {0}", nd.Data.Date);
                            }
                            else
                                Console.WriteLine("Поезда с таким номером нет в списке!");
                        }
                        Console.Write(WAITING_FOR_ENTER);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    case 4: //удаление записи
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine(EMPTY_LIST);
                        else
                        {
                            Console.WriteLine("Введите номер поезда для удаления: ");
                            int number = InputParseDigital();
                            bool isPresent = list.Remove(number);
                            Console.WriteLine(isPresent == true ? "Запись удалена!" : "Поезда с таким номером нет в списке!");
                        }
                        Console.Write(WAITING_FOR_ENTER);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    case 5: //редактирование записи
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine(EMPTY_LIST);
                        else
                        {
                            Console.WriteLine("Введите номер поезда для редактирования: ");
                            int number = InputParseDigital();
                            if(!list.EditTrain(number))
                                Console.WriteLine("Поезда с таким номером нет в списке!");
                        }
                        Console.Write(WAITING_FOR_ENTER);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    case 6: //запись в файл
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine(EMPTY_LIST);
                        else
                            WriteInFile(list);
                        Console.Write(WAITING_FOR_ENTER);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    case 7: //чтение из файла
                    {
                        ReadFile();
                        Console.Clear();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Ввод данных
        /// </summary>
        /// <param name="train"></param>
        public static void InputData(Train train)
        {
            int n;

            do
            {
                Console.WriteLine("Введите пункт назначения: ");
                train.Destination = ReadLettersFromConsole();
                if (train.Destination == "")
                    Console.WriteLine("Введено пустое поле! Повторите ввод.");
            } while (train.Destination == "");

            do
            {
                Console.WriteLine("Введите номер поезда: ");
                n = InputParseDigital();
                if (list.Contains(n))
                    Console.WriteLine("Нельзя вводить поезда с одинаковыми номерами! Повторите ввод.");
            } while (list.Contains(n));
            train.Number = n;
            Console.WriteLine("Введите время отправления (в формате чч:мм, где ч - часы, м - минуты): ");
            train.Date = InputParseTime();
            Console.Clear();
        }

        /// <summary>
        /// Ввод только символов
        /// </summary>
        /// <returns></returns>
        private static string ReadLettersFromConsole()
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

        /// <summary>
        /// Ввод времени отправления
        /// </summary>
        /// <returns></returns>
        private static string InputParseTime()
        {
            char[] ch = { '0', '1', '2', '3', '4', '5' };
            int i = 0;
            string str = "";

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

        /// <summary>
        /// Ввод только цифр
        /// </summary>
        /// <returns></returns>
        private static int InputParseDigital()
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

        /// <summary>
        /// Выод всего списка
        /// </summary>
        /// <param name="list"> Список поездов </param>
        private static void PrintData(LinkedList list)
        {
            Console.WriteLine("\nРасписание поездов: ");
            foreach (Train item in list)
            {
                Console.WriteLine("\nПункт назначения: {0}", item.Destination);
                Console.WriteLine("Номер поезда: {0}", Convert.ToString(item.Number));
                Console.WriteLine("Время отправления: {0}", item.Date);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Запись в файл всего списка
        /// </summary>
        /// <param name="list"></param>
        private static void WriteInFile(LinkedList list)
        {
            string text = "";
            // запись в файл
            using (FileStream fstream = new FileStream(@"E:\Unity\note.txt", FileMode.Create))
            {
                foreach (Train item in list)
                {
                    text = item.Destination + " " + Convert.ToString(item.Number) + " " + item.Date + " ";
                    byte[] array = Encoding.Default.GetBytes(text);// преобразуем строку в байты
                    fstream.Write(array, 0, array.Length);// запись массива байтов в файл
                }
                Console.WriteLine("Текст записан в файл");
            }
        }

        /// <summary>
        /// Чтение из фала всего списка
        /// </summary>
        private static void ReadFile()
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
                            Destination = currentLine[element],
                            Number = Int32.Parse(currentLine[element + 1]),
                            Date = currentLine[element + 2]
                        };
                        if (list.Contains(train.Number))
                            element += 2;
                        else
                        {
                            element += 2;
                            list.Add(train);
                        }
                    }
                }
            }
        }
    }
}
