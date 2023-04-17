using System.Drawing.Printing;

namespace InventoryManger.Models
{
    public class PaginatedList<T>: List<T>//
    {
        public int TotalRecord { get; private set; }

        public PaginatedList(List<T>source, int pageIndex,int pageSize) 
        {
            TotalRecord = source.Count;
            var items=source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            this.AddRange(items);

        }
    }
}
