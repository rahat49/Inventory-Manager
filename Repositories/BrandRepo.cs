using InventoryManger.Data;
using InventoryManger.Interfaces;
using InventoryManger.Models;
using Microsoft.EntityFrameworkCore;
using dotnetsorting.Tools;
using System.Drawing.Printing;


namespace InventoryManger.Repositories
{
    public class BrandRepo : IBrand
    {
        private readonly InventoryDbContext _context;
        public BrandRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public Brand Create(Brand item)
        {
            _context.Brands.Add(item);
            _context.SaveChanges();
            return item;
        }
        public Brand Edit(Brand item)
        {
            _context.Brands.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }
        public Brand Delete(Brand item)
        {
            _context.Brands.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
            return item;
        }

        private List<Brand> DoSort(List<Brand> items, string SortProperty, SortOrder sortOrder)
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

        public PaginatedList<Brand> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Brand> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.Brands.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Brands.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);


            PaginatedList<Brand> retitems = new PaginatedList<Brand>(items, pageIndex, pageSize);

            return retitems;
        }

        public Brand GetItem(int id)
        {
            Brand items = _context.Brands.Where(u => u.Id == id).FirstOrDefault();
            return items;
        }

        public bool IsItemExist(string name)
        {
            int ct = _context.Brands.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsItemExist(string name, int Id)
        {
            int ct = _context.Brands.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
