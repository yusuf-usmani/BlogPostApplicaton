using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BOL
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            BlogPostEntities db = new BlogPostEntities();
            string userEmailValue = value.ToString();
            int count = db.tbl_user.Where(x => x.Email == userEmailValue).ToList().Count();
            if (count != 0)
                return new ValidationResult("User Already Exist With This Email ID");
            return ValidationResult.Success;
        }
    }
    public class tblUserValidation
    {
        [Required]
        [EmailAddress]
        [UniqueEmail]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]

        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }

    [MetadataType(typeof(tblUserValidation))]
    public partial class tbl_user
    {
        public string ConfirmPassword { get; set; }
    }
}
