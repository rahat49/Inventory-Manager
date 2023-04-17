using dotnetsorting.Tools;
using Inventory_Manager.Models;
using InventoryManger.Models;

namespace InventoryManger.Interfaces
{
    public interface ISupplier
    {
        PaginatedList<Supplier> GetItems(string SortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 5); //read all
        Supplier GetItem(int id); //read particular item
        Supplier Create(Supplier item);
        Supplier Edit(Supplier item);
        Supplier Delete(Supplier item);

        public bool IsItemExist(string name);
        public bool IsItemExist(string name, int Id);//overloaded method

        public bool IsSupplierCodeExists(string Code);
        public bool IsSupplierCodeExists(string Code, int Id);

        public bool IsSupplierEmailExists(string email);
        public bool IsSupplierEmailExists(string email, int Id);
    }
}
