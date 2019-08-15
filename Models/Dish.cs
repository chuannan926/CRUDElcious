using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        [Required]
        public string Chef_Name { get; set; }

        
        [Required]
        public string Dish_Name{ get; set; }
        
        [Required]
        [Range(1,5)]
        public int Tastiness{ get; set; }

        [Required]
        [Range(0,Int32.MaxValue)]
        public int Calories{ get; set; }

        [Required]
        public string Description{ get; set;} 

       


    }
}