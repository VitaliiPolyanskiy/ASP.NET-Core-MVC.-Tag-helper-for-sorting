namespace Soccer.Models;

public class IndexViewModel
{
    // Використання сучасного виразу колекції [] замість new List<Players>()
    public IEnumerable<Player> Players { get; set; } = [];
    public SortViewModel SortViewModel { get; set; } = new(SortState.NameAsc);
}