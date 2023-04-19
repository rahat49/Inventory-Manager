using InventoryManger.Data;
using InventoryManger.Interfaces;
using InventoryManger.Models;
using Microsoft.EntityFrameworkCore;
using dotnetsorting.Tools;
using Inventory_Manager.Models;

namespace InventoryManger.Repositories
{
    public class CurrencyRepo : ICurrency
    {
        private readonly InventoryDbContext _context;
        private string _errors = "";
        public CurrencyRepo(InventoryDbContext context)
        {
            _context = context;
        }
        bool ICurrency.Create(Currency item)
        {
            try
            {
                //1. rules
                if (!IsDescriptionValid(item)) return false;

                //2. rules
                if (IsItemExist(item.Name)) return false;

                _context.Currency.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                _errors = "Sql Exception Occured, Error Info : " + ex.Message;
                return false;
            }
           
          
        }

        bool ICurrency.Edit(Currency item)
        {
            try
            {
                //1. rules
                if (!IsDescriptionValid(item)) return false;

                //2. rules
                if (IsItemExist(item.Name,item.Id)) return false;

                _context.Currency.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured, Error Info : " + ex.Message;
                return false;
            }
           
        }

        bool ICurrency.Delete(Currency item)
        {

            try
            {
                _context.Currency.Attach(item);
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured, Error Info : " + ex.Message;
                return false;
            } 
        }

        private List<Currency> DoSort(List<Currency> items, string SortProperty, SortOrder sortOrder)
        {
            //List<Unit> units = _context.Units.ToList();

            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)

                    items = items.OrderBy(x => x.Name).ToList();
                else

                    items = items.OrderByDescending(x => x.Name).ToList();

            }
            else
            {
                if (sortOrder == SortOrder.Ascending)

                    items = items.OrderBy(d => d.Description).ToList();
                else

                    items = items.OrderByDescending(d => d.Description).ToList();

            }
            return items;
        }

        public PaginatedList<Currency> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Currency> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.Currency.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
                items = _context.Currency.ToList();
            
            items = DoSort(items, SortProperty, sortOrder);


            PaginatedList<Currency> retitems = new PaginatedList<Currency>(items, pageIndex, pageSize);

            return retitems;
        }

        public Currency GetItem(int id)
        {
            Currency items = _context.Currency.Where(u => u.Id == id).FirstOrDefault();
            return items;
        }

        public bool IsItemExist(string name)
        {
            int ct = _context.Currency.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
            {
                _errors = "Name" + name + "Exists ALready";
                return true;
            }
               
            else
                return false;
        }
        public bool IsItemExist(string name, int id)
        {
            int ct = _context.Currency.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsCurrencycombExist(int srcCurrencyId, int excurrencyId)
        {
            int ct = _context.Currency.Where(n => n.Id==srcCurrencyId && n.ExchangeCurrencyId==excurrencyId).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public string GetError()
        {
            return _errors;
        }

        //Rules List
        private bool IsDescriptionValid(Currency item)
        {
            if(item.Description.Length<4 || item.Description==null)
            {
                _errors = "Description Must be atleast 4 Character";
                return false;
            }
            return true;
        }

          
    }
}
