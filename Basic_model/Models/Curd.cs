using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web;
namespace Basic_model.Models
{
    public class Curd
    {
        [Key]
        public int Users_id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile number")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please Enter Email id")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Enter the vaild email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

       [Required(ErrorMessage = "Please Enter Confirm Password")]
       // [DataType(DataType.Password)]
        [Compare("Password")]
        public string Con_password { get; set; }

        //[Required, Range(1, int.MaxValue, ErrorMessage = "Error: Must Choose a Role")]
        public string Role { get; set; }

        // [Required, Range(1, int.MaxValue, ErrorMessage = "Error: Must Choose a Status")]
        public bool Status { get; set; }

    }
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class DisplayModel
    {
            [Key]
            public int Users_id { get; set; }
            public string Name { get; set; }
            public DateTime DOB { get; set; }
            public string Gender { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; } 
            public string Password { get; set; }
            public string Con_password { get; set;}    
            public string Role { get; set; }
            public bool Status { get; set; }
    }

    public class DropModel
    {
        public string CustomerID { get;  set; }

    }

    public class Drop_displayModel
    {
        public string CustomerID { get; set; }
        public DateTime OrderDate { get;  set; }
        public double Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set;}
    }

    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }

    public class OTPModel
    {
        public string OTPCode { get; set; }
    }

    public class Order_date
    {
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
       
    }


    public class Order_date_details
    {
        public string CustomerID { get; set;}
        public DateTime OrderDate { get; set;}
        public double Freight { get; set;}
        public string ShipName { get; set;}
        public string ShipAddress { get; set; }
    }
    public class View_date_details
    {
        public Order_date od1 { get; set; }
        public IEnumerable<Order_date_details> odd1 { get; set; }
    }

    public class Forg_Email
    {
        public string Email { get; set; }
    }

    public class Reset_Password
    {
       

        [Required(ErrorMessage = "Please Enter Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        // [DataType(DataType.Password)]
        [Compare("Password")]
        public string Con_password { get; set; }

    }


}


