using InventoryManger.Data;
using InventoryManger.Interfaces;
using InventoryManger.Models;
using Microsoft.EntityFrameworkCore;
using dotnetsorting.Tools;
using System.Drawing.Printing;
using Inventory_Manager.Models;
using System.Xml.Linq;

namespace InventoryManger.Repositories
{
    public class SupplierRepo : ISupplier
    {
        private readonly InventoryDbContext _context;
        public SupplierRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public Supplier Create(Supplier item)
        {
            _context.Suppliers.Add(item);
            _context.SaveChanges();
            return item;
        }
        public Supplier Edit(Supplier item)
        {
            _context.Suppliers.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }
        public Supplier Delete(Supplier item)
        {
            _context.Suppliers.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
            return item;
        }

        private List<Supplier> DoSort(List<Supplier> items, string SortProperty, SortOrder sortOrder)
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

                    items = items.OrderBy(d => d.Code).ToList();
                else

                    items = items.OrderByDescending(d => d.Code).ToList();

            }
            return items;
        }

        public PaginatedList<Supplier> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Supplier> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.Suppliers.Where(n => n.Name.Contains(SearchText) || n.Code.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Suppliers.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);


            PaginatedList<Supplier> retitems = new PaginatedList<Supplier>(items, pageIndex, pageSize);

            return retitems;
        }

        public Supplier GetItem(int id)
        {
            Supplier items = _context.Suppliers.Where(u => u.Id == id).FirstOrDefault();
            return items;
        }

        public bool IsItemExist(string name)
        {
            int ct = _context.Suppliers.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsItemExist(string name, int Id)
        {
            int ct = _context.Suppliers.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsSupplierEmailExists(string email)
        {
            int ct = _context.Suppliers.Where(n => n.Email.ToLower() == email.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsSupplierEmailExists(string email, int Id)
        {
            int ct = _context.Suppliers.Where(n => n.Email.ToLower() == email.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsSupplierCodeExists(string Code)
        {
            int ct = _context.Suppliers.Where(n => n.Code.ToLower() == Code.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsSupplierCodeExists(string Code, int Id)
        {
            int ct = _context.Suppliers.Where(n => n.Code.ToLower() == Code.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
