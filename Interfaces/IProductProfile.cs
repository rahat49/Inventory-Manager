using InventoryManger.Models;
using dotnetsorting.Tools;
namespace InventoryManger.Interfaces
{
    public interface IProductProfile
    {
        PaginatedList<ProductProfile> GetItems(string SortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 5); //read all
        ProductProfile GetItem(int id); //read particular item
        ProductProfile Create(ProductProfile item);
        ProductProfile Edit(ProductProfile item);
        ProductProfile Delete(ProductProfile item);

        public bool IsItemExist(string name);
        public bool IsItemExist(string name, int Id);//overloaded method

    }
}
