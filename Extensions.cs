using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    /*
    * This DTO is used to avoid doing the same thing over and over.
    * This function will be called rather than writing the same code twice.
    * This is used in ItemsController because the below functionality was repeated for get and get/id
    *           
                var items = repository.GetItems().Select( item => new ItemDto
                {
                    Id= item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    CreatedDate =  item.CreatedDate
                });
    */
    
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
           return new ItemDto
            {
               Id= item.Id,
               Name = item.Name,
               Price = item.Price,
               CreatedDate =  item.CreatedDate
            };
        }
    }
}