namespace MyCrm.Shared.Blazor.Models
{
    public class PagedResultDto<T>
    {
        public long TotalCount { get; set; }
        public IReadOnlyList<T>? Items { get; set; }
    }
}
