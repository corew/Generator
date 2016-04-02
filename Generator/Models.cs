﻿
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `connectionString`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.\SQL90;Initial Catalog=C7V3;Integrated Security = SSPI;`
//     Schema:                 ``
//     Include Views:          `False`

using System;
using System.Collections.Generic;

namespace Generator
{
    public partial class SYS_MENU
    {
		public long Id { get; set; }
		public long? ParentId { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string NavigateUrl { get; set; }
		public int? SortIndex { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class PRD_DELIVERY
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ClientId { get; set; }
		public long? ProjectId { get; set; }
		public string ProjectName { get; set; }
		public long? CustomerId { get; set; }
		public string CustomerName { get; set; }
		public long? ContractId { get; set; }
		public string ContractName { get; set; }
		public long? LineId { get; set; }
		public string LineName { get; set; }
		public long? PlanId { get; set; }
		public string PlanName { get; set; }
		public long? TaskId { get; set; }
		public string TaskName { get; set; }
		public long? TheoryProportionId { get; set; }
		public string TheoryProportionName { get; set; }
		public long? ActualProportionId { get; set; }
		public string ActualProportionName { get; set; }
		public string ConstructionUnit { get; set; }
		public string BuildingUnit { get; set; }
		public string SupervisionUnit { get; set; }
		public string Address { get; set; }
		public decimal? Distance { get; set; }
		public string DistanceRange { get; set; }
		public string Marker { get; set; }
		public string Grade { get; set; }
		public string Slump { get; set; }
		public string StoneSizeLevel { get; set; }
		public string SandSizeLevel { get; set; }
		public string SpecialRequest { get; set; }
		public string PouringPart { get; set; }
		public string PouringMode { get; set; }
		public string MixerTruck { get; set; }
		public string MixerDriver { get; set; }
		public decimal? PlateVolume { get; set; }
		public int? PlateNumber { get; set; }
		public string Operator { get; set; }
		public DateTime? OperateTime { get; set; }
		public decimal? PlanQuantity { get; set; }
		public decimal? TotalQuantity { get; set; }
		public int? TotalTripCount { get; set; }
		public decimal? Quantity { get; set; }
		public decimal? PrtQuantity { get; set; }
		public decimal? ChkQuantity { get; set; }
		public decimal? PrdQuantity { get; set; }
		public decimal? ResQuantity { get; set; }
		public long? SourceDeliveryId { get; set; }
		public long? TargetDeliveryId { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string Remarks { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class CNF_LINE
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string ClientId { get; set; }
		public string Type { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string IPCBrand { get; set; }
		public string IPCModel { get; set; }
		public decimal? PlateVolume { get; set; }
		public decimal? Capacity { get; set; }
		public string IpAddress { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class SYS_ROLE
    {
		public long Id { get; set; }
		public string Name { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class CNF_MATERIAL
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string ClientId { get; set; }
		public int? SortIndex { get; set; }
		public string MapColumn { get; set; }
		public string Usage { get; set; }
		public string Type { get; set; }
		public string Modal { get; set; }
		public string Unit { get; set; }
		public string Orgin { get; set; }
		public string Manufacturer { get; set; }
		public long? Status { get; set; }
		public short? Verified { get; set; }
		public string Remarks { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class SYS_ROLE_MENU_ACTION
    {
		public long Id { get; set; }
		public long? RoleId { get; set; }
		public long? MenuId { get; set; }
		public long? ActionId { get; set; }
		public string ControlId { get; set; }
		public string ControlType { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class CNF_SILO
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string ClientId { get; set; }
		public int? SortIndex { get; set; }
		public string MapColumn { get; set; }
		public long? LineId { get; set; }
		public long? MaterialId { get; set; }
		public string MaterialName { get; set; }
		public string Type { get; set; }
		public decimal? Volume { get; set; }
		public decimal? MaxVolume { get; set; }
		public decimal? MinVolume { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class SYS_DEPARTMENT
    {
		public long Id { get; set; }
		public long? ParentId { get; set; }
		public string Name { get; set; }
		public int? SortIndex { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class SYS_COMPANY
    {
		public long Id { get; set; }
		public string Name { get; set; }
		public string FullName { get; set; }
		public string AbbrName { get; set; }
		public string RegionCode { get; set; }
		public decimal? RegisteredCapital { get; set; }
		public decimal? NetWorth { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public string ContactPhone { get; set; }
		public string Tel { get; set; }
		public string Fax { get; set; }
		public string Zip { get; set; }
		public string Email { get; set; }
		public string Website { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public string ValidRadius { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class SYS_ACTION
    {
		public long Id { get; set; }
		public string Name { get; set; }
		public long? MenuId { get; set; }
		public string ControlId { get; set; }
		public string ControlType { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class SYS_USER
    {
		public long Id { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public long? DepartmentId { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class INV_STOCK
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ClientId { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string Remarks { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class SYS_USER_ROLE
    {
		public long Id { get; set; }
		public long? UserId { get; set; }
		public long? RoleId { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class INV_STOCK_IN
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ClientId { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string Remarks { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class INV_STOCK_OUT
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ClientId { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string Remarks { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class WEG_WEIGHING
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ClientId { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string Remarks { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class PRD_MANUFACTURE
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ClientId { get; set; }
		public long? DeliveryId { get; set; }
		public long? LineId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public decimal? PlateVolume { get; set; }
		public int? PlateNumber { get; set; }
		public int? PlateIndex { get; set; }
		public decimal? Quantity { get; set; }
		public int? Duration { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string Remarks { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

    public partial class PRD_COMSUPTION
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ClientId { get; set; }
		public long? ManufactureId { get; set; }
		public long? SiloId { get; set; }
		public long? MaterialId { get; set; }
		public decimal? TheoryValue { get; set; }
		public decimal? ActualValue { get; set; }
		public decimal? Deviation { get; set; }
		public int? Status { get; set; }
		public int? Verified { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string LastUpdateBy { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public bool Active { get; set; }
		public long UserBelongTo { get; set; }
		public long CmpyBelongTo { get; set; }
	}

}
