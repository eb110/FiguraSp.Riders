namespace FiguraSp.Riders.Entity;

public partial class Rider
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public DateOnly DoB { get; set; }

    public string PictureUrl { get; set; } = null!;
}
