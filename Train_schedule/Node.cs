namespace Train_schedule
{
    /// <summary>
    /// Запись в списке
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Создание новой записи
        /// </summary>
        /// <param name="data"></param>
        public Node(Train data) => Data = data;

        /// <summary>
        /// Наполнение записи
        /// </summary>
        public Train Data { get; set; }

        /// <summary>
        /// Указатель на следующий элемент
        /// </summary>
        public Node next;
    }
}
