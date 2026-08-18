using System.ComponentModel.DataAnnotations;

namespace GameStore.Api;

public record updateGameDto (
 [Required] [StringLength(20)]string Name,
    [Range(1, 50)] int GenreId,
   [Required] [Range(1 , 100)] decimal Price,
   [Required]  DateOnly ReleaseDate
);