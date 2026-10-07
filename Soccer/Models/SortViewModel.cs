namespace Soccer.Models;

public class SortViewModel
{
    public SortState NameSort { get; private set; }     // значення для сортування за ім'ям
    public SortState AgeSort { get; private set; }      // значення для сортування за віком
    public SortState PositionSort { get; private set; } // значення для сортування за позицією
    public SortState TeamSort { get; private set; }     // значення для сортування за командою
    public SortState Current { get; private set; }      // значення властивості, обраної для сортування
    public bool Up { get; private set; }                // сортування за зростанням чи спаданням

    public SortViewModel(SortState sortOrder)
    {
        // Значення за замовчуванням
        NameSort = SortState.NameAsc;
        AgeSort = SortState.AgeAsc;
        PositionSort = SortState.PositionAsc;
        TeamSort = SortState.TeamAsc;

        // Використання логічного патерну (Pattern Matching)
        Up = sortOrder is not (SortState.AgeDesc or SortState.NameDesc
                            or SortState.PositionDesc or SortState.TeamDesc);

        // Оптимізований switch-вираз замість громіздкого switch-statement
        Current = sortOrder switch
        {
            SortState.NameDesc => NameSort = SortState.NameAsc,
            SortState.AgeAsc => AgeSort = SortState.AgeDesc,
            SortState.AgeDesc => AgeSort = SortState.AgeAsc,
            SortState.PositionAsc => PositionSort = SortState.PositionDesc,
            SortState.PositionDesc => PositionSort = SortState.PositionAsc,
            SortState.TeamAsc => TeamSort = SortState.TeamDesc,
            SortState.TeamDesc => TeamSort = SortState.TeamAsc,
            _ => NameSort = SortState.NameDesc
        };
    }
}