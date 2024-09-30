public record UpdateCategoryDto
{
  public required string CategoryName { get; set; }
  public string Description { get; set; } = string.Empty;
}