using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record  createGamesDto (
 [Required] [StringLength(20)]string Name,
   [Required] [StringLength(50)] string Genre,
   [Required] [Range(1 , 100)] decimal Price,
   [Required]  DateOnly ReleaseDate
);