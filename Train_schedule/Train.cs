namespace Train_schedule
{
    /// <summary>
    /// Поезд
    /// </summary>
    public class Train
    {
        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Номер поезда
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Время отправления
        /// </summary>
        public string Date { get; set; }
    }
}
