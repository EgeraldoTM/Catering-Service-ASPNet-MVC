namespace Core.Models;

public class Category : IComparable<Category>
{
    public byte Id { get; set; }
    public string Name { get; set; } = null!;

	public int CompareTo(Category? other)
	{
		if (other == null)
			return 1;

		return this.Id.CompareTo(other.Id);
	}
}
