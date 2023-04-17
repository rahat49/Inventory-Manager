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
    public class ProductRepo : IProduct
    {
        private readonly InventoryDbContext _context;
        public ProductRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public Product Create(Product item)
        {
            _context.Grossary.Add(item);
            _context.SaveChanges();
            return item;
        }
        public Product Edit(Product item)
        {
            _context.Grossary.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }
        public Product Delete(Product item)
        {
           
            item = pGetItem(item.Code);
            _context.Grossary.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
            return item;
        }

        private List<Product> DoSort(List<Product> items, string SortProperty, SortOrder sortOrder)
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

        public PaginatedList<Product> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Product> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.Grossary.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText))
                    .Include(u => u.Units).ToList();
            }
            else
            {
                items = _context.Grossary.Include(u => u.Units).ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);


            PaginatedList<Product> retitems = new PaginatedList<Product>(items, pageIndex, pageSize);

            return retitems;
        }
      

        public Product GetItem(string Code)
        {
            Product items = _context.Grossary.Where(u => u.Code == Code)
                .Include(u=>u.Units).FirstOrDefault();
            return items;
        }
        public Product pGetItem(string Code)
        {
            Product items = _context.Grossary.Where(u => u.Code == Code)
                .FirstOrDefault();

            items.BriefPhotoName = GetBriefPhotoName(items.PhotoUrl);
            return items;
        }
        private string GetBriefPhotoName(string filename)
        {
            if (filename == null)
                return "";

            string[] words = filename.Split('_');
            return words[words.Length - 1];
        }
        public bool IsItemExist(string name)
        {
            int ct = _context.Grossary.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsItemExist(string name, string Code)
        {
            int ct = _context.Grossary.Where(n => n.Name.ToLower() == name.ToLower() && n.Code != Code).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemCodeExist(string itemCode)
        {
            int ct = _context.Grossary.Where(n => n.Code.ToLower() == itemCode.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemCodeExist(string name, string itemCode)
        {
            int ct = _context.Grossary.Where(n => n.Code.ToLower() == itemCode.ToLower() && n.Name != name).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
