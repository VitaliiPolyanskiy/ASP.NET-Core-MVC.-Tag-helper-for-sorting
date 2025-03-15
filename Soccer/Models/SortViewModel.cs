namespace Soccer.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; } // значение для сортировки по имени
        public SortState AgeSort { get; set; }    // значение для сортировки по возрасту
        public SortState PositionSort { get; set; }    // значение для сортировки по позиции
        public SortState TeamSort { get; set; }   // значение для сортировки по компании
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            NameSort = SortState.NameAsc;
            AgeSort = SortState.AgeAsc;
            PositionSort = SortState.PositionAsc;
            TeamSort = SortState.TeamAsc;
            Up = true;

            if (sortOrder == SortState.AgeDesc || sortOrder == SortState.NameDesc
                || sortOrder == SortState.PositionDesc || sortOrder == SortState.TeamDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;
                case SortState.AgeAsc:
                    Current = AgeSort = SortState.AgeDesc;
                    break;
                case SortState.AgeDesc:
                    Current = AgeSort = SortState.AgeAsc;
                    break;
                case SortState.PositionAsc:
                    Current = PositionSort = SortState.PositionDesc;
                    break;
                case SortState.PositionDesc:
                    Current = PositionSort = SortState.PositionAsc;
                    break;
                case SortState.TeamAsc:
                    Current = TeamSort = SortState.TeamDesc;
                    break;
                case SortState.TeamDesc:
                    Current = TeamSort = SortState.TeamAsc;
                    break;
                default:
                    Current = NameSort = SortState.NameDesc;
                    break;
            }
        }
    }
}
