using dotnetsorting.Tools;
using Inventory_Manager.Models;
using InventoryManger.Models;

namespace InventoryManger.Interfaces
{
    public interface ICurrency
    {
        PaginatedList<Currency> GetItems(string SortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 5); //read all
        Currency GetItem(int id); //read particular item
        bool Create(Currency item);
        bool Edit(Currency item);
        bool Delete(Currency item);
        public bool IsItemExist(string name);
        public bool IsItemExist(string name, int id);//overloaded method

        public bool IsCurrencycombExist(int srcCurrencyId,int excurrencyId);
        public string GetError();
    }
}
