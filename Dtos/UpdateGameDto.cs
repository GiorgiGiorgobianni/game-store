using System.ComponentModel.DataAnnotations;

namespace GameStore.api.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(50)]string Name,
    [Required][StringLength(15)]string Genre,
    [Required][Range(1, 100)]decimal Price,
    [Required]DateOnly ReleaseDate
);

