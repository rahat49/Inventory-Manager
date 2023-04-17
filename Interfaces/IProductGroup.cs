using dotnetsorting.Tools;
using InventoryManger.Models;

namespace InventoryManger.Interfaces
{
    public interface IProductGroup
    {
        PaginatedList<ProductGroup> GetItems(string SortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 5); //read all
        ProductGroup GetItem(int id); //read particular item
        ProductGroup Create(ProductGroup item);
        ProductGroup Edit(ProductGroup item);
        ProductGroup Delete(ProductGroup item);

        public bool IsItemExist(string name);
        public bool IsItemExist(string name, int Id);//overloaded method
    }
}
