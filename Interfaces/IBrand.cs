using dotnetsorting.Tools;
using InventoryManger.Models;

namespace InventoryManger.Interfaces
{
    public interface IBrand
    {
        PaginatedList<Brand> GetItems(string SortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 5); //read all
        Brand GetItem(int id); //read particular item
        Brand Create(Brand item);
        Brand Edit(Brand item);
        Brand Delete(Brand item);

        public bool IsItemExist(string name);
        public bool IsItemExist(string name, int Id);//overloaded method
    }
}
