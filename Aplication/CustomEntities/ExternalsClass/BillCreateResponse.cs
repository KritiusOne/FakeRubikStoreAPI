namespace Aplication.CustomEntities.ExternalsClass
{
    public class BillCreateResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public NumberingRange Numbering_Range { get; set; }
        public List<string> Billing_Period { get; set; }
        public Bill Bill { get; set; }
    }

    public class Company
    {
        public string Url_Logo { get; set; }
        public string Nit { get; set; }
        public string Dv { get; set; }
        public string Name { get; set; }
        public string Graphic_Representation_Name { get; set; }
        public string Registration_Code { get; set; }
        public string Economic_Activity { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public string Municipality { get; set; }
    }

    public class Customer
    {
        public string Identification { get; set; }
        public string Dv { get; set; }
        public string Graphic_Representation_Name { get; set; }
        public string Trade_Name { get; set; }
        public string Company { get; set; }
        public string Names { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public LegalOrganization Legal_Organization { get; set; }
        public Tribute Tribute { get; set; }
        public Municipality Municipality { get; set; }
    }

    public class LegalOrganization
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Tribute
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Municipality
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class NumberingRange
    {
        public string Prefix { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Resolution_Number { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public int Months { get; set; }
    }

    public class Bill
    {
        public int Id { get; set; }
        public Document Document { get; set; }
        public string Number { get; set; }
        public string Reference_Code { get; set; }
        public int Status { get; set; }
        public int Send_Email { get; set; }
        public string Qr { get; set; }
        public string Cufe { get; set; }
        public string Validated { get; set; }
        public string Discount_Rate { get; set; }
        public string Discount { get; set; }
        public string Gross_Value { get; set; }
        public string Taxable_Amount { get; set; }
        public string Tax_Amount { get; set; }
        public string Total { get; set; }
        public string Observation { get; set; }
        public List<string> Errors { get; set; }
        public string Created_At { get; set; }
        public string Payment_Due_Date { get; set; }
        public string Qr_Image { get; set; }
    }

    public class Document
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

}
