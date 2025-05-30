﻿using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Documents;
public class CreateAndEditDocumentViewModel
{
    public long Order { get; set; }
    public DocumentYears Year { get; set; }
    public DocumentMonth Month { get; set; }

    [Display(Name = "شماره مرجع")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string RetrivalRef { get; set; }
    [Display(Name = "شماره پیگیری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TrackingNo { get; set; }
    [Display(Name = "تاریخ تراکنش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TransactionDate { get; set; }
    [Display(Name = "ساعت تراکنش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TransactionTime { get; set; }
    [Display(Name = "تاریخ مالی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string FinancialDate { get; set; }
    [Display(Name = "شماره شبا")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Iban { get; set; }
    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Amount { get; set; }
    [Display(Name = "مبلغ اصلی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string PrincipalAmount { get; set; }
    [Display(Name = "شماره کارت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string CardNo { get; set; }
    [Display(Name = "شماره پایانه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Terminal { get; set; }
    [Display(Name = "محل نصب")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string InstallationPlace { get; set; }
    [Display(Name = "کد خدمت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ServiceCode { get; set; }
    [Display(Name = "نام خدمت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ServiceName { get; set; }
    [Display(Name = "نام استان")]
    public string? ProvinceName { get; set; }
    [Display(Name = "نام شهر / مرکز")]
    public string SubProvince { get; set; }
    [Display(Name = "کد استان")]
    public string? ProvinceCode { get; set; }
    [Display(Name = "کد معین")]
    public string? CertainCode { get; set; }
    [Display(Name = "کد تفضیل 4")]
    public string? CodeLevel4 { get; set; }
    [Display(Name = "کد تفضیل 5")]
    public string? CodeLevel5 { get; set; }
    [Display(Name = "کد تفضیل 6")]
    public string? CodeLevel6 { get; set; }
    [Display(Name = "توضیحات سند")]
    public string? Description { get; set; }
    [Display(Name = "بدهکار")]
    public string? Debt { get; private set; }
    [Display(Name = "بستانکار")]
    public string? Credit { get; private set; }
}
