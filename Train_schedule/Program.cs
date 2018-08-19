using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_schedule
{
    class Program
    {
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
                        list.InputData(train);
                        list.Add(train);
                        break;
                    }
                    case 2: //вывод всего списка (расписания)
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine("Список пуст! Заполните его.");
                        else
                            list.PrintData(list);
                        Console.ReadLine();    
                        break;
                    }
                    case 3: //поиск позда по номеру
                    {
                        
                        if (list.ListIsEmpty())
                            Console.WriteLine("Список пуст! Заполните его.");
                        else
                        {
                            Console.WriteLine("Введите номер поезда для поиска: ");
                            int number = list.InputParseDigital();
                            Node nd = list.FindTrain(number);
                            if (nd != null)
                            {
                                Console.WriteLine("Найденный поезд: ");
                                Console.WriteLine("Пункт назначения: {0}", nd.Data.destination);
                                Console.WriteLine("Номер поезда: {0}", Convert.ToString(nd.Data.number));
                                Console.WriteLine("Время отправления: {0}", nd.Data.date);
                            }
                            else
                                Console.WriteLine("Поезда с таким номером нет в списке!");
                        }
                        Console.ReadLine();
                        break;
                    }
                    case 4: //удаление записи
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine("Список пуст! Заполните его.");
                        else
                        {
                            Console.WriteLine("Введите номер поезда для удаления: ");
                            int number = list.InputParseDigital();
                            bool isPresent = list.Remove(number);
                            Console.WriteLine(isPresent == true ? "Запись удалена!" : "Поезда с таким номером нет в списке!");
                        }
                        Console.ReadLine();
                        break;
                    }
                    case 5: //редактирование записи
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine("Список пуст! Заполните его.");
                        else
                        {
                            Console.WriteLine("Введите номер поезда для редактирования: ");
                            int number = list.InputParseDigital();
                            if(!list.EditTrain(number))
                                Console.WriteLine("Поезда с таким номером нет в списке!");
                        }
                        Console.ReadLine();
                        break;
                    }
                    case 6: //запись в файл
                    {
                        if (list.ListIsEmpty())
                            Console.WriteLine("Список пуст! Заполните его.");
                        else
                            list.WriteInFile(list);
                        Console.ReadLine();
                        break;
                    }
                    case 7: //чтение из файла
                    {
                        list.ReadFile();
                        break;
                    }
                }
            }
        }
    }
}
