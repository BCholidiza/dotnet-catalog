using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalgo.Dtos;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // Get /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            // this tells the system to first run GetItemsAsync asynchnously and then do the select part
            var items = (await repository.GetItemsAsync())
                        .Select( item => item.AsDto());
            return items;
        }


        // Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            //ActionResult allows us to return more than 1 type in function
            // eg function initial returned Item but after ActionResult it allows return of NotFound()
            var item = (await repository.GetItemAsync(id)).AsDto();

            if (item is null)
            {
                return NotFound();
            }
            return item;
        }

        // Post /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.CreateItemAsync(item);

            // I think this function will call GetItem function, give it item id and then ensure it is AsDto
            // CreatedAtAction(name of function to call, item to pass to url, format used/model used)
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        //API recognise id so order is not important
        // then it know to take itemDto from body
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null){

                return NotFound();
            }

            // With creates a copy of the existing item with modified Name and Price
            // With goes with records - it is named a with-expression
            Item updatedItem = existingItem with 
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null){

                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}