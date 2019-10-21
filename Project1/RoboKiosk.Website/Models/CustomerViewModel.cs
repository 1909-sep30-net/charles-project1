using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoboKiosk.Website.Models
{
    public class CustomerViewModel
    {

        //[DisplayName("ID")] // this one is associated with the Html.DisplayNameFor helper

        //[DisplayFormat(] // there are attributes like Display and DisplayFormat
        // to modify here in this one place how values in this property will be displayed in some view
        // (when we use the Html.DisplayFor HTML helper!)
        [Required] // can't be null or empty string
        [DisplayName("Name")]
        
        public string FName { get; set; }

        [Required]
        [DisplayName("Family Name")]
        public string LName { get; set; }

        [Required]
        [DisplayName("Mobile Number")]

        public string PhoneNum { get; set; }

        //password for customer
        [Required]
        [DisplayName("Password")]
        public string CustId { get; set; }

        //index for the customer from the DB
        [DisplayName("Customer Number")]
        public int CustNum { get; set; }



        // in ASP.NET, client-side validation is driven by DataAnnotations attributes like these
        //   in combination with jquery Validation library, and the tag helpers for input and validation span.

        // server-side validation is driven by the same properties, which are checked during model binding
        // and erors are put into ModelState. you do have to write the code ot check ModelState.

        //#QUESTION# do I simply iterate over the customers?
        public List<string> Customers { get; set; }

    }
}

