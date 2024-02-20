using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbarakaTestAPI.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }
        //[RegularExpression("^[1-9]{1}[0-9]{9}[02468]{1}$\r\n")]
        public required string TCKNO { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
