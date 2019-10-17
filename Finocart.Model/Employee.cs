using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Employee 
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Employee Salary
        /// </summary>
        public decimal Salary { get; set; }
    }
}
