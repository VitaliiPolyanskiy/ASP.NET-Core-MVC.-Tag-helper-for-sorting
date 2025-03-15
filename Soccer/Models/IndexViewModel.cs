namespace Soccer.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Players> Players { get; set; } = new List<Players>();
        public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.NameAsc);
    }
}
