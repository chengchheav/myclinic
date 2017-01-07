using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MyClinic.Infrastructure
{
    public class UserAdd
    {
        public int Id { get; set; }

        
        [DisplayName("Name")]
        [Required(ErrorMessage = "The Name can't be empty.")]
        [StringLength(200)]
        public string Name { get; set; }

        
        [DisplayName("User Name")]
        [Required(ErrorMessage = "The User​​ Name can't be empty.")]
        [StringLength(200)]
        public string UserName { get; set; }


        [DisplayName("Password")]
        [Required(ErrorMessage = "The Password can't be empty.")]
        [DataType(DataType.Password, ErrorMessage = "Password is not strong.")]
        [StringLength(50)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "The Confirm Password can't be empty.")]
        public string ConfirmPassword { get; set; }

        [StringLength(200)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid.")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "The Email is not valid (ex : example@gmail.com).")]
        public string Email { get; set; }

        [DisplayName("User Type")]
        [StringLength(200)]
        public string UserType { get; set; }

        [DisplayName("Telephone")]
        [StringLength(200)]
        public string Tel { get; set; }

        public int IsActived { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public string ImageStream { get; set; }
    }

    public class UserEdit
    {
        public int Id { get; set; }

        
        [DisplayName("Name")]
        [Required(ErrorMessage = "The Name can't be empty.")]
        [StringLength(200)]
        public string Name { get; set; }

        
        [DisplayName("User Name")]
        [Required(ErrorMessage = "The User​​ Name can't be empty.")]
        [StringLength(200)]
        public string UserName { get; set; }


        [DisplayName("Password")]
        [Required(ErrorMessage = "The Password can't be empty.")]
        [DataType(DataType.Password, ErrorMessage = "Password is not strong.")]
        [StringLength(50)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "The Confirm Password can't be empty.")]
        public string ConfirmPassword { get; set; }

        [StringLength(200)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid.")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "The Email is not valid (ex : example@gmail.com).")]
        public string Email { get; set; }

        [DisplayName("User Type")]
        [StringLength(200)]
        public string UserType { get; set; }

        [DisplayName("Telephone")]
        [StringLength(200)]
        public string Tel { get; set; }

        public int IsActived { get; set; }

        
        public string Image { get; set; }

        public string ImageStream { get; set; }
    }



    [AttributeUsage(AttributeTargets.Class)]
    public class PropertiesMatchAttribute : ValidationAttribute
    { 

        public String FirstPropertyName { get; set; }
        public String SecondPropertyName { get; set; } 

        //Constructor to take in the property names that are supposed to be checked
        public PropertiesMatchAttribute(String firstPropertyName, String secondPropertyName )
        {
            FirstPropertyName = firstPropertyName;
            SecondPropertyName = secondPropertyName ;
        } 

        public override Boolean IsValid(Object value)
        {
            Type objectType = value.GetType();
            //Get the property info for the object passed in.  This is the class the attribute is
            //  attached to
            //I would suggest caching this part... at least the PropertyInfo[]
            PropertyInfo[] neededProperties =
            objectType.GetProperties()
            .Where(propertyInfo => propertyInfo.Name == FirstPropertyName || propertyInfo.Name == SecondPropertyName)
            .ToArray();

            if(neededProperties.Count() != 2)
            {
                throw new ApplicationException("PropertiesMatchAttribute error on " + objectType.Name);
            }

            Boolean isValid = true;

            //Convert both values to string and compare...  Probably could be done better than this
            //  but let's not get bogged down with how dumb I am.  We should be concerned about
            //  dumb you are, jerkface.
            if(!Convert.ToString(neededProperties[0].GetValue(value, null)).Equals(Convert.ToString(neededProperties[1].GetValue(value, null))))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
