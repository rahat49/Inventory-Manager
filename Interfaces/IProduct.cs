using InventoryManger.Models;
using dotnetsorting.Tools;
using Inventory_Manager.Models;

namespace InventoryManger.Interfaces
{
    public interface IProduct
    {
        PaginatedList<Product> GetItems(string SortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 5); //read all
        Product GetItem(string Code); //read particular item
        Product Create(Product item);
        Product Edit(Product item);
        Product Delete(Product item);

        public bool IsItemExist(string name);
        public bool IsItemExist(string name, string Code);//overloaded method


        public bool IsItemCodeExist(string itemCode);
        public bool IsItemCodeExist(string name, string itemCode);//overloaded method


    }
}
