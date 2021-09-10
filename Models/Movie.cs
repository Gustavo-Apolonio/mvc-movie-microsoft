using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do título deve ser entre 3 e 60 caracteres.")]
        [Required(ErrorMessage = "O título é obrigatório!")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de lançamento é obrigatório!")]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Deve usar apenas letras; a primeira letra deve ser maiúscula; não use números e/ou caracteres especiais.")]
        [Required(ErrorMessage = "O gênero é obrigatório!")]
        [StringLength(30, ErrorMessage = "O tamanho do gênero não pode passar de 30 caracteres.")]
        public string Genre { get; set; }

        [Range(1, 100, ErrorMessage = "O valor gasto deve estar entre 1 e 100 (lembre-se que a medida é em Milhões de $ (dolares))")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "double(18, 2)")]
        [Required(ErrorMessage = "O preço é obrigatório!")]
        [RegularExpression(@"[0-9\.]*$", ErrorMessage = "Insira apenas números.")]
        public double Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "O primeiro caractere precisa ser uma letra maiúscula.")]
        [StringLength(15, ErrorMessage = "O tamanho da classificação indicativa não pode passar de 15 caracteres.")]
        [Required(ErrorMessage = "A classificação indicativa é obrigatório!")]
        public string Rating { get; set; }
    }
}