using dz1chernovik.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace dz1chernovik;

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
    public IActionResult GetAll()
    {
        var employees = _repository.GetAll();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var employee = _repository.GetById(id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Sotrudnik employee)
    {
        _repository.AddSotrudnik(employee);
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Sotrudnik employee)
    {
        var existing = _repository.GetById(id);
        if (existing == null)
            return NotFound();

        _repository.ChangeSotrudnik(id, employee.Name, employee.Date);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = _repository.GetById(id);
        if (existing == null)
            return NotFound();

        _repository.DeleteSotrudnik(id);
        return Ok();
    }
}