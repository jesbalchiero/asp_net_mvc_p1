using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto
    {
        [Required(ErrorMessage = "O campo título é obrigatório!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Diretor é obrigatório!")]
        public string Diretor { get; set; }

        [StringLength(30, ErrorMessage = "O gênero é inválido!")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "Duração inválida!")]
        public int Duracao { get; set; }
    }
}
