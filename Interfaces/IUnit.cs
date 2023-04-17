using InventoryManger.Models;
using dotnetsorting.Tools;
namespace InventoryManger.Interfaces
{
    public interface IUnit
    {
       PaginatedList<Unit> GetItems(string SortProperty,SortOrder sortOrder, string searchText="", int pageIndex=1, int pageSize=5); //read all
        Unit GetUnit(int id); //read particular item
        Unit Create(Unit unit);
        Unit Edit(Unit unit);
        Unit Delete(Unit unit);

        public bool IsUnitNameExist(string name);
        public bool IsUnitNameExist(string name, int Id);


    }
}
