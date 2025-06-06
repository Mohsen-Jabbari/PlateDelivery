﻿using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.TopYarTmps;

public class TopYarTmpViewModel
{
    public List<TopYarTmp> TopYarTmps { get; set; }
    public int TopYarTmpCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public string ProvinceMessage { get; set; }
    public string IbanMessage { get; set; }
    public string ServiceMessage { get; set; }
    public string MultiplexMessage { get; set; }
}

public class CreateAndEditTopYarTmpViewModel : BaseDto
{
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
    public string ServiceName { get; set; }
    [Display(Name = "نام استان")]
    public string? ProvinceName { get; set; }
    [Display(Name = "شناسه")]
    public string? Identification { get; set; }
    [Display(Name = "نام شهر / مرکز")]
    public string? SubProvince { get; set; }
    [Display(Name = "کد استان")]
    public string? ProvinceCode { get; set; }
}
