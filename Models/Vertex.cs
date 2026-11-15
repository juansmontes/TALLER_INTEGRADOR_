
namespace CampusNet.Models;

public sealed class Vertex
{
    public string Id { get; }
    public string Name { get; private set; }
    public string Role { get; private set; }

    public Vertex(string id, string name, string role)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("El ID no puede estar vacío.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(name));

        Id = id.Trim();
        Name = name.Trim();
        Role = NormalizeRole(role);
    }

    public void Update(string? name, string? role)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name.Trim();

        if (!string.IsNullOrWhiteSpace(role))
            Role = NormalizeRole(role);
    }

    private static string NormalizeRole(string role) =>
        (role ?? "").Trim().ToLowerInvariant() switch
        {
            "estudiante" => "Estudiante",
            "profesor" => "Profesor",
            "egresado" => "Egresado",
            _ => throw new ArgumentException(
                "El rol debe ser Estudiante, Profesor o Egresado.", nameof(role))
        };

    public override string ToString() =>
        $"{Id} - {Name} ({Role})";
}
