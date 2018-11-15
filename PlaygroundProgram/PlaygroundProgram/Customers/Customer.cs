using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundProgram.Customers
{
    [Serializable]
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
    }
    public class Gift
    {
        public string Name = "Raspberry Pi 2";
        public int GetRetailPrice()
        {
            return 35;
        }

        internal int GetPriceAfterDiscount()
        {
            return 30;
        }
    }


    [Serializable]
    public class Customer
    {

        public string FullName { get; set; }

        public int Id { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public string MothersMaiden { get; set; }

        internal string GetEmailAddress()
        {
            return EmailAddress;
        }

        public DateTime NextBirthday { get; set; }
        public string CCType { get; set; }
        public string CaseFileID { get; set; }
        public string UPS { get; set; }
        public string Occupation { get; set; }
        public string Domain { get; set; }
        public string MembershipLevel { get; set; }
        public string Kilograms { get; set; }



        private int _pendingInvoiceID;



        public int PendingInvoiceID
        {
            get { return _pendingInvoiceID; }
            set { _pendingInvoiceID = value; }
        }

        public Address Address { get; set; }
        public ClubMembershipTypes ClubMembershipType { get; set; }
        public int YearsAsCustomer { get { return 5; } }

        public double GetYearlyEarnings(int year)
        {
            return year * 2;
        }

        internal int GetTotalMoneySpentAtStoreInUSD()
        {
            return ((int)FirstName[0]) * 50;
        }
    }

    public enum Gender
    {
        Male, Female
    }

    public enum ClubMembershipTypes
    {
        Premium, Platinum
    }
}
