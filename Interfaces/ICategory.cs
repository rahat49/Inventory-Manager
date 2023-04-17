using InventoryManger.Models;
using dotnetsorting.Tools;
namespace InventoryManger.Interfaces
{
    public interface ICategory
    {
        PaginatedList<Category> GetItems(string SortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 5); //read all
        Category GetItem(int id); //read particular item
        Category Create(Category category);
        Category Edit(Category category);
        Category Delete(Category category);

        public bool IsItemExist(string name);
        public bool IsItemExist(string name, int Id);//overloaded method


    }
}
