﻿
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `connectionString`
//     Provider:               `System.Data.SQLite`
//     Connection String:      `Data Source=E:\Tools\SQLite\northwindEF.db;Version=3;`
//     Schema:                 ``
//     Include Views:          `False`

using System;
using System.Collections.Generic;

namespace Generator
{
    public partial class Regions
    {
		public int RegionID { get; set; }
		public string RegionDescription { get; set; }
	}

    public partial class PreviousEmployees
    {
		public int EmployeeID { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Title { get; set; }
		public string TitleOfCourtesy { get; set; }
		public string BirthDate { get; set; }
		public string HireDate { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string HomePhone { get; set; }
		public string Extension { get; set; }
		public string Photo { get; set; }
		public string Notes { get; set; }
		public string PhotoPath { get; set; }
	}

    public partial class Employees
    {
		public int EmployeeID { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Title { get; set; }
		public string TitleOfCourtesy { get; set; }
		public string BirthDate { get; set; }
		public string HireDate { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string HomePhone { get; set; }
		public string Extension { get; set; }
		public string Photo { get; set; }
		public string Notes { get; set; }
		public string PhotoPath { get; set; }
	}

    public partial class Customers
    {
		public string CustomerID { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
	}

    public partial class Suppliers
    {
		public int SupplierID { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string HomePage { get; set; }
	}

    public partial class InternationalOrders
    {
		public int OrderID { get; set; }
		public string CustomsDescription { get; set; }
		public string ExciseTax { get; set; }
	}

    public partial class OrderDetails
    {
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public string UnitPrice { get; set; }
		public string Quantity { get; set; }
		public double Discount { get; set; }
	}

    public partial class Territories
    {
		public int TerritoryID { get; set; }
		public string TerritoryDescription { get; set; }
		public int RegionID { get; set; }
	}

    public partial class EmployeesTerritories
    {
		public int EmployeeID { get; set; }
		public int TerritoryID { get; set; }
	}

    public partial class Products
    {
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public int? SupplierID { get; set; }
		public int? CategoryID { get; set; }
		public string QuantityPerUnit { get; set; }
		public string UnitPrice { get; set; }
		public string UnitsInStock { get; set; }
		public string UnitsOnOrder { get; set; }
		public string ReorderLevel { get; set; }
		public string Discontinued { get; set; }
		public string DiscontinuedDate { get; set; }
	}

    public partial class Orders
    {
		public int OrderID { get; set; }
		public string CustomerID { get; set; }
		public int? EmployeeID { get; set; }
		public string OrderDate { get; set; }
		public string RequiredDate { get; set; }
		public string ShippedDate { get; set; }
		public string Freight { get; set; }
		public string ShipName { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }
	}

    public partial class Categories
    {
		public int CategoryID { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public string Picture { get; set; }
	}

}
