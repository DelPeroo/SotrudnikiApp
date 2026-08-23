

using System.ComponentModel.DataAnnotations;

public class Sotrudnik
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Имя сотрудника обязательно для заполнения")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата должна быть указана")]
    public DateOnly Date { get; set; }

}