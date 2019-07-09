using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Repositories
{
    public class ProductList : IEnumerable<ProductViewModel>
    {
        //建構值，初始化
        public List<ProductViewModel> ProductItems { get; set; }

        public ProductList()
        {
            this.ProductItems = new List<ProductViewModel>();
        }

        public int Count => this.ProductItems.Count;

        //新增一筆Product
        public void AddProduct(ProductViewModel Item)
        {
            var item = new ProductViewModel()
            {
                Salepage_id = Item.Salepage_id,
                SkuId = Item.SkuId,
                ProductName = Item.ProductName,
                UnitPrice = Item.UnitPrice,
                Keyword = Item.Keyword
            };

            ProductItems.Add(item);
        }

        //刪除一筆
        public void DeleteProduct(int skuId)
        {
            var item = ProductItems.Find(x => x.SkuId == skuId);
            ProductItems.Remove(item);
        }

        public IEnumerator<ProductViewModel> GetEnumerator()
        {
            IEnumerable<ProductViewModel> nothing = ProductItems;
            return nothing.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}