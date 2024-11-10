using PlateDelivery.Core.Models.Documents;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.DataLayer.Entities.AccountAgg.Repository;
using PlateDelivery.DataLayer.Entities.CertainAgg.Repository;
using PlateDelivery.DataLayer.Entities.DocumentAgg;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Types;
using PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg.Repository;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;

namespace PlateDelivery.Core.Services.Documents;

internal class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repository;
    private readonly IProvinceRepository _provinceRepository;
    private readonly ICertainRepository _certainRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IServiceCodingRepository _serviceCodingRepository;

    public DocumentService(IDocumentRepository repository,
        IProvinceRepository provinceRepository, ICertainRepository certainRepository,
        IAccountRepository accountRepository, IServiceCodingRepository serviceCodingRepository)
    {
        _repository = repository;
        _provinceRepository = provinceRepository;
        _certainRepository = certainRepository;
        _accountRepository = accountRepository;
        _serviceCodingRepository = serviceCodingRepository;
    }

    //سند عادی یک سطر بانک یک سطر درآمد یک سطر مالیات
    public long CreateDocument(TopYarTmp topYar, ServiceCoding service, long maxOrder)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            //var maxOrder = _repository.GetMaxOrder();
            if (maxOrder == 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var ServiceCertain = _certainRepository.Get(service.CertainId);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                var account = _accountRepository.GetByIban(topYar.Iban);
                string description = string.Concat("بابت درآمد ", service.ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);
                decimal income = (decimal.Parse(topYar.Amount) * 100) / 110;
                decimal tax = decimal.Parse(topYar.Amount) - decimal.Round(income);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);

                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                documents.Add(incomeRecord);
                //tax record
                var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", tax.ToString(), year, month);
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
            else if (maxOrder > 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //درسافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var ServiceCertain = _certainRepository.Get(service.CertainId);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                var account = _accountRepository.GetByIban(topYar.Iban);
                string description = string.Concat("بابت درآمد ", service.ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);
                decimal income = (decimal.Parse(topYar.Amount) * 100) / 110;
                decimal tax = decimal.Parse(topYar.Amount) - decimal.Round(income);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);

                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                documents.Add(incomeRecord);
                //tax record
                var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", tax.ToString(), year, month);
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    //سند 5 سطری. برای مواردی که تسهیم دارند. باید یک سطر بانک 2 سطر درآمد و 2 سطر مالیات باشد
    public long CreateDocument(TopYarTmp topYar, List<ServiceCoding> services, long maxOrder)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            //var maxOrder = _repository.GetMaxOrder();
            if (maxOrder == 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var account = _accountRepository.GetByIban(topYar.Iban);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                string description = string.Concat("بابت درآمد ", services.First().ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);


                foreach (var service in services)
                {
                    var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                    var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                    if (long.Parse(topYar.Amount) == serviceAmount)
                    {
                        var ServiceCertain = _certainRepository.Get(service.CertainId);
                        decimal income = (decimal.Parse(service.Amount) * 100) / 110;
                        decimal tax = decimal.Parse(service.Amount) - decimal.Round(income);

                        //income record
                        var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                            , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                            province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                        documents.Add(incomeRecord);
                        //tax record
                        var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                            , province.CodeLevel4 ?? service.CodeLevel4, null, null
                            , description, "0", tax.ToString(), year, month);
                        documents.Add(taxRecord);
                    }
                    else
                    {
                        var ServiceCertain = _certainRepository.Get(service.CertainId);
                        decimal income = (decimal.Parse(service.OldAmount) * 100) / 110;
                        decimal tax = decimal.Parse(service.OldAmount) - decimal.Round(income);

                        //income record
                        var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                            , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                            province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                        documents.Add(incomeRecord);
                        //tax record
                        var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                            , province.CodeLevel4 ?? service.CodeLevel4, null, null
                            , description, "0", tax.ToString(), year, month);
                        documents.Add(taxRecord);
                    }
                }

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
            else if (maxOrder > 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var account = _accountRepository.GetByIban(topYar.Iban);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                string description = string.Concat("بابت درآمد ", services.First().ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);

                foreach (var service in services)
                {
                    var ServiceCertain = _certainRepository.Get(service.CertainId);
                    var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                    var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                    if (long.Parse(topYar.Amount) == serviceAmount)
                    {
                        decimal income = (decimal.Parse(service.Amount) * 100) / 110;
                        decimal tax = decimal.Parse(service.Amount) - decimal.Round(income);

                        //income record
                        var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                            , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                            province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                        documents.Add(incomeRecord);
                        //tax record
                        var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                            , province.CodeLevel4 ?? service.CodeLevel4, null, null
                            , description, "0", tax.ToString(), year, month);
                        documents.Add(taxRecord);
                    }

                    else
                    {
                        decimal income = (decimal.Parse(service.OldAmount) * 100) / 110;
                        decimal tax = decimal.Parse(service.OldAmount) - decimal.Round(income);

                        //income record
                        var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                            , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                            province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                        documents.Add(incomeRecord);
                        //tax record
                        var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                            , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                            , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                            , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                            , province.CodeLevel4 ?? service.CodeLevel4, null, null
                            , description, "0", tax.ToString(), year, month);
                        documents.Add(taxRecord);
                    }
                }

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    //برای حالتی است که رکورد فقط درآمد خالص است و مالیات جداگانه در گزارش تاپ یار آمده است
    public long CreateIncomeDocument(TopYarTmp topYar, ServiceCoding service, long maxOrder)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            //var maxOrder = _repository.GetMaxOrder();
            if (maxOrder == 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var ServiceCertain = _certainRepository.Get(service.CertainId);
                var account = _accountRepository.GetByIban(topYar.Iban);
                string description = string.Concat("بابت درآمد ", service.ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", topYar.Amount, year, month);
                documents.Add(incomeRecord);

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
            else if (maxOrder > 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //درسافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var ServiceCertain = _certainRepository.Get(service.CertainId);
                var account = _accountRepository.GetByIban(topYar.Iban);
                string description = string.Concat("بابت درآمد ", service.ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", topYar.Amount, year, month);
                documents.Add(incomeRecord);

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    //برای حالتی است که رکورد فقط مالیات است و درآمد جداگانه در گزارش تاپ یار آمده است
    public long CreateTaxDocument(TopYarTmp topYar, ServiceCoding service, long maxOrder)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            //var maxOrder = _repository.GetMaxOrder();
            if (maxOrder == 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //درسافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var ServiceCertain = _certainRepository.Get(service.CertainId);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                var account = _accountRepository.GetByIban(topYar.Iban);
                string description = string.Concat("بابت درآمد ", service.ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);

                //tax record
                var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", topYar.Amount, year, month);
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
            else if (maxOrder > 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //درسافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var ServiceCertain = _certainRepository.Get(service.CertainId);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                var account = _accountRepository.GetByIban(topYar.Iban);
                string description = string.Concat("بابت درآمد ", service.ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);

                //tax record
                var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", topYar.Amount, year, month);
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    public long CreateTaxDocument(TopYarTmp topYar, List<ServiceCoding> services, long maxOrder)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            //var maxOrder = _repository.GetMaxOrder();
            if (maxOrder == 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var account = _accountRepository.GetByIban(topYar.Iban);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                string description = string.Concat("بابت درآمد ", services.First().ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);


                foreach (var service in services)
                {
                    if (service.IncludeTax)
                    {
                        var ServiceCertain = _certainRepository.Get(service.CertainId);
                        var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                        var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                        if (long.Parse(topYar.Amount) == serviceAmount)
                        {
                            decimal income = (decimal.Parse(service.Amount) * 100) / 110;
                            decimal tax = decimal.Parse(service.Amount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }

                        else
                        {
                            decimal income = (decimal.Parse(service.OldAmount) * 100) / 110;
                            decimal tax = decimal.Parse(service.OldAmount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }
                    }

                    else if (!service.IncludeTax)
                    {
                        var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                        var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                        if (long.Parse(topYar.Amount) == serviceAmount)
                        {
                            var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", service.Amount, year, month);
                            documents.Add(taxRecord);
                        }
                        else
                        {
                            var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", service.OldAmount, year, month);
                            documents.Add(taxRecord);
                        }
                    }

                }

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
            else if (maxOrder > 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var account = _accountRepository.GetByIban(topYar.Iban);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                string description = string.Concat("بابت درآمد ", services.First().ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);


                foreach (var service in services)
                {
                    if (service.IncludeTax)
                    {
                        var ServiceCertain = _certainRepository.Get(service.CertainId);
                        var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                        var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                        if (long.Parse(topYar.Amount) == serviceAmount)
                        {
                            decimal income = (decimal.Parse(service.Amount) * 100) / 110;
                            decimal tax = decimal.Parse(service.Amount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }

                        else
                        {
                            decimal income = (decimal.Parse(service.OldAmount) * 100) / 110;
                            decimal tax = decimal.Parse(service.OldAmount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }
                    }

                    else if (!service.IncludeTax)
                    {
                        var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                        var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                        if (long.Parse(topYar.Amount) == serviceAmount)
                        {
                            var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", service.Amount, year, month);
                            documents.Add(taxRecord);
                        }

                        else
                        {
                            var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", service.OldAmount, year, month);
                            documents.Add(taxRecord);
                        }

                    }

                }

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    //برای حالتی است که در مبلغ و خدمت سه رکورد داریم که 2 تای آنها نباید مالیت سند بخورد ولی یکی از آنها 
    //باید مالیت برای آن ثبت شود. سند 5 سطری است
    public long CreateTaxTaxIncomeDocument(TopYarTmp topYar, List<ServiceCoding> services, long maxOrder)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            //var maxOrder = _repository.GetMaxOrder();
            if (maxOrder == 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var account = _accountRepository.GetByIban(topYar.Iban);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                string description = string.Concat("بابت درآمد ", services.First().ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);


                foreach (var service in services)
                {
                    if (service.IncludeTax)
                    {
                        var ServiceCertain = _certainRepository.Get(service.CertainId);
                        var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                        var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                        if (long.Parse(topYar.Amount) == serviceAmount)
                        {
                            decimal income = (decimal.Parse(service.Amount) * 100) / 110;
                            decimal tax = decimal.Parse(service.Amount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }

                        else
                        {
                            decimal income = (decimal.Parse(service.OldAmount) * 100) / 110;
                            decimal tax = decimal.Parse(service.OldAmount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }
                    }
                    else
                    {
                        if (service.CertainId == 2)
                        {
                            var ServiceCertain = _certainRepository.Get(service.CertainId);
                            var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                            var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                            if (long.Parse(topYar.Amount) == serviceAmount)
                            {
                                var IncomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, province.ProvinceCode, service.CodeLevel6
                                , description, "0", service.Amount, year, month);
                                documents.Add(IncomeRecord);
                            }

                            else
                            {
                                var IncomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, province.ProvinceCode, service.CodeLevel6
                                , description, "0", service.OldAmount, year, month);
                                documents.Add(IncomeRecord);
                            }
                        }

                        else
                        {
                            var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                            var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                            if (long.Parse(topYar.Amount) == serviceAmount)
                            {
                                var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", service.Amount, year, month);
                                documents.Add(taxRecord);
                            }

                            else
                            {
                                var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", service.OldAmount, year, month);
                                documents.Add(taxRecord);
                            }
                        }
                    }
                }

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
            else if (maxOrder > 0)
            {
                List<Document> documents = new();
                //دریافت کد سطح 5 از طریق استان و شهر رکورد تاپ یار
                var province = _provinceRepository.GetProvinceByNameAndSubName(topYar.ProvinceName, topYar.SubProvince);
                //دریافت کد معین بانک
                var bankCertain = _certainRepository.Get(1);
                //دریافت کد معین خدمت
                var account = _accountRepository.GetByIban(topYar.Iban);
                //دریافت کد معین مالیات
                var taxCertain = _certainRepository.Get(4);
                string description = string.Concat("بابت درآمد ", services.First().ServiceName,
                    " با شماره پایانه ", topYar.Terminal, " در تاریخ ", topYar.TransactionDate);

                //تبدیل تاریخ تراکنش به سال و ماه
                var year = _repository.GetYear(topYar.TransactionDate);
                var month = _repository.GetMonth(topYar.TransactionDate);


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0", year, month);
                documents.Add(bankRecord);


                foreach (var service in services)
                {
                    if (service.IncludeTax)
                    {
                        var ServiceCertain = _certainRepository.Get(service.CertainId);
                        var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                        var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                        if (long.Parse(topYar.Amount) == serviceAmount)
                        {
                            decimal income = (decimal.Parse(service.Amount) * 100) / 110;
                            decimal tax = decimal.Parse(service.Amount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }

                        else
                        {
                            decimal income = (decimal.Parse(service.OldAmount) * 100) / 110;
                            decimal tax = decimal.Parse(service.OldAmount) - decimal.Round(income);

                            //income record
                            var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                                province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString(), year, month);
                            documents.Add(incomeRecord);
                            //tax record
                            var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", tax.ToString(), year, month);
                            documents.Add(taxRecord);
                        }
                    }
                    else
                    {
                        if (service.CertainId == 2)
                        {
                            var ServiceCertain = _certainRepository.Get(service.CertainId);
                            var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                            var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                            if (long.Parse(topYar.Amount) == serviceAmount)
                            {
                                var IncomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, province.ProvinceCode, service.CodeLevel6
                                , description, "0", service.Amount, year, month);
                                documents.Add(IncomeRecord);
                            }

                            else
                            {
                                var IncomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, province.ProvinceCode, service.CodeLevel6
                                , description, "0", service.OldAmount, year, month);
                                documents.Add(IncomeRecord);
                            }
                        }

                        else
                        {
                            var serviceAmount = services.Sum(s => long.Parse(s.Amount));
                            var serviceOldAmount = services.Sum(s => long.Parse(s.OldAmount));

                            if (long.Parse(topYar.Amount) == serviceAmount)
                            {
                                var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", service.Amount, year, month);
                                documents.Add(taxRecord);
                            }

                            else
                            {
                                var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                                , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                                , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                                , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                                , province.CodeLevel4 ?? service.CodeLevel4, null, null
                                , description, "0", service.OldAmount, year, month);
                                documents.Add(taxRecord);
                            }
                        }
                    }
                }

                _repository.AddRange(documents);
                //_repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    public bool DeleteDocument(long Id)
    {
        throw new NotImplementedException();
    }

    public bool EditDocument(CreateAndEditDocumentViewModel model)
    {
        throw new NotImplementedException();
    }

    public DocumentViewModel GetDocumentsByDocDate(string docDate, int pageId = 1, int take = 10)
    {
        IQueryable<Document> result = _repository.GetDocumentByDate(docDate);

        int takeData = take;
        int skip = (pageId - 1) * takeData;

        DocumentViewModel list = new();
        list.PageCount = (int)Math.Ceiling(result.Count() / (double)takeData);
        list.CurrentPage = pageId;
        list.PageCount = (int)Math.Ceiling(result.Count() / (double)takeData);
        list.Documents = result.OrderBy(u => u.Year).OrderBy(u => u.Month).Skip(skip).Take(takeData).ToList();
        list.DocumentCounts = result.Distinct().GroupBy(u => u.Order).Count();
        return list;
    }

    public List<Document> GetDocumentsByDocDate(string docDate)
    {
        IQueryable<Document> result = _repository.GetDocumentByDate(docDate);
        return result.ToList();
    }

    public List<Document> GetDocumentsByDocDateForTax(string docDate)
    {
        List<string> validCertains = new() { "60120", "69090" };
        IQueryable<Document> result = _repository.GetDocumentByDate(docDate);
        result = result.Where(r => validCertains.Contains(r.CertainCode));
        #region MyRegion
        //var taxService = _serviceCodingRepository.GetAll();
        //taxService = taxService.Where(s => s.IncludeTax == false && s.CodeLevel6 == null).ToList();
        //var taxArrey = taxService.Select(s => s.ServiceCode).ToArray();
        //var list = result.ToList();
        //var listToDelete = list.Where(r => taxArrey.Contains(r.ServiceCode)).ToList();
        //var taxAmount = taxService.Where(s => s.CodeLevel6 == null).Select(s => s.Amount).ToList();
        //listToDelete = listToDelete.Where(l => taxAmount.Contains(l.Amount)).ToList();
        //list = list.Except(listToDelete).ToList();
        #endregion

        return result.ToList();
    }

    public SummaryExportDocumentViewModel GetMainHeadListOfDocumentsForExport(int pageId = 1, int take = 10, DocumentYears Year = DocumentYears.NotSelected, DocumentMonth Month = DocumentMonth.NotSelected)
    {
        IQueryable<ExportSummaryType> result = _repository.GetSummary();

        if (Year != DocumentYears.NotSelected)
        {
            result = result.Where(r => r.DocumentYear == Year);
        }

        if (Month != DocumentMonth.NotSelected)
        {
            result = result.Where(r => r.DocumentMonth == Month);
        }

        int takeData = take;
        int skip = (pageId - 1) * takeData;
        var x = result.GroupBy(u => new { year = u.DocumentYear, month = u.DocumentMonth, docDate = u.TransactionDate })
            .Select(n => new { n.Key.year, n.Key.month, n.Key.docDate, cnt = n.Count() });
        SummaryExportDocumentViewModel list = new();
        SummaryDocuments summary = new();

        list.SummaryDocuments = new();
        foreach (var item in x)
        {
            list.SummaryDocuments.Add(new SummaryDocuments()
            {
                Year = item.year,
                Month = item.month,
                DocumentDate = item.docDate,
                Count = item.cnt,
                CreditAmount = GetDocumentsByDocDateForTax(item.docDate).Sum(d => long.Parse(d.Credit))
            });
        }
        list.SummaryCounts = list.SummaryDocuments.Count;
        list.PageCount = (int)Math.Ceiling(list.SummaryDocuments.Count / (double)takeData);
        list.CurrentPage = pageId;
        list.LastPage = list.PageCount;
        list.PrevPage = Math.Max(pageId - 1, list.CurrentPage);
        list.NextPage = Math.Max(pageId + 1, list.LastPage);
        list.SummaryDocuments = list.SummaryDocuments
            .OrderByDescending(l => l.Year).ThenByDescending(l => l.Month).ThenBy(l => l.DocumentDate).Skip(skip).Take(takeData).ToList();
        return list;
    }

    public long GetMaxOrder()
    {
        return _repository.GetMaxOrder();
    }

    public bool IsDocumentDateExists(string docDate)
    {
        if (_repository.GetDocumentByDate(docDate).Any())
            return true;
        return false;
    }

    public void SaveChanges()
    {
        _repository.SaveSync();
    }
}
