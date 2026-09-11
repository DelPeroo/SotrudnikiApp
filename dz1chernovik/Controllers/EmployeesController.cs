using dz1chernovik.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeesController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _repository.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Sotrudnik employee)
    {
        await _repository.AddSotrudnikAsync(employee);
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Sotrudnik employee)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        employee.Id = id;
        await _repository.UpdateSotrudnikAsync(employee);
        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _repository.DeleteSotrudnikAsync(id);
        return Ok();
    }
}