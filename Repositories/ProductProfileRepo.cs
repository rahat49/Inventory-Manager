using InventoryManger.Data;
using InventoryManger.Interfaces;
using InventoryManger.Models;
using Microsoft.EntityFrameworkCore;
using dotnetsorting.Tools;
using System.Drawing.Printing;


namespace InventoryManger.Repositories
{
    public class ProductProfileRepo : IProductProfile
    {
        private readonly InventoryDbContext _context;
        public ProductProfileRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public ProductProfile Create(ProductProfile item)
        {
            _context.ProductProfiles.Add(item);
            _context.SaveChanges();
            return item;
        }
        public ProductProfile Edit(ProductProfile item)
        {
            _context.ProductProfiles.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }
        public ProductProfile Delete(ProductProfile item)
        {
            _context.ProductProfiles.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
            return item;
        }

        private List<ProductProfile> DoSort(List<ProductProfile> items, string SortProperty, SortOrder sortOrder)
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

        public PaginatedList<ProductProfile> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<ProductProfile> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.ProductProfiles.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.ProductProfiles.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);


            PaginatedList<ProductProfile> retitems = new PaginatedList<ProductProfile>(items, pageIndex, pageSize);

            return retitems;
        }

        public ProductProfile GetItem(int id)
        {
            ProductProfile items = _context.ProductProfiles.Where(u => u.Id == id).FirstOrDefault();
            return items;
        }

        public bool IsItemExist(string name)
        {
            int ct = _context.ProductProfiles.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsItemExist(string name, int Id)
        {
            int ct = _context.ProductProfiles.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
