using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Shop.Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Nume { get; set; }
        [Required]
        [Display(Name = "Model Compatibil")]
        public string ModelCompatibil { get; set; }
        public string Descriere { get; set; }
        [Required]
        public string CodProdus { get; set; }
        public string Producator { get; set; }
        public string Culoare { get; set; }
        [Required]
        [Display(Name = "Pret de Lista")]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Pret pentru 2-3")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Pret pentru 3+")]
        public double Price3 { get; set; }

        public int CategoryId {get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category {get; set; }
        public string ImageUrl { get; set; }
    }
}