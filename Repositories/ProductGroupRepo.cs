using InventoryManger.Data;
using InventoryManger.Interfaces;
using InventoryManger.Models;
using Microsoft.EntityFrameworkCore;
using dotnetsorting.Tools;
using System.Drawing.Printing;


namespace InventoryManger.Repositories
{
    public class ProductGroupRepo : IProductGroup
    {
        private readonly InventoryDbContext _context;
        public ProductGroupRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public ProductGroup Create(ProductGroup item)
        {
            _context.ProductGroups.Add(item);
            _context.SaveChanges();
            return item;
        }
        public ProductGroup Edit(ProductGroup item)
        {
            _context.ProductGroups.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }
        public ProductGroup Delete(ProductGroup item)
        {
            _context.ProductGroups.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
            return item;
        }

        private List<ProductGroup> DoSort(List<ProductGroup> items, string SortProperty, SortOrder sortOrder)
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

        public PaginatedList<ProductGroup> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<ProductGroup> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.ProductGroups.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.ProductGroups.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);


            PaginatedList<ProductGroup> retitems = new PaginatedList<ProductGroup>(items, pageIndex, pageSize);

            return retitems;
        }

        public ProductGroup GetItem(int id)
        {
            ProductGroup items = _context.ProductGroups.Where(u => u.Id == id).FirstOrDefault();
            return items;
        }

        public bool IsItemExist(string name)
        {
            int ct = _context.ProductGroups.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsItemExist(string name, int Id)
        {
            int ct = _context.ProductGroups.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
