using CampusEats.Api.Dtos;
using CampusEats.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CampusEats.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _svc;

    public MenuController(IMenuService svc)
        => _svc = svc;

    // GET /api/menu
    // Anyone can view all menu items
    [HttpGet]
    public ActionResult<IEnumerable<MenuItemDto>> GetAll()
        => Ok(_svc.GetAll());

    // GET /api/menu/1
    // Anyone can view a specific menu item
    [HttpGet("{id}")]
    public ActionResult<MenuItemDto> GetById(int id)
    {
        var item = _svc.GetById(id);

        return item is null
            ? NotFound()
            : Ok(item);
    }

    // POST /api/menu
    // Only Admin can create
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<MenuItemDto> Create(
        [FromBody] CreateMenuItemDto dto)
    {
        var created = _svc.Create(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // PUT /api/menu/1
    // Only Admin can update
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(
        int id,
        [FromBody] CreateMenuItemDto dto)
    {
        var ok = _svc.Update(id, dto);

        return ok
            ? NoContent()
            : NotFound();
    }

    // DELETE /api/menu/1
    // Only Admin can delete
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var ok = _svc.Delete(id);

        return ok
            ? NoContent()
            : NotFound();
    }
}