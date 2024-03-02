using PlateDelivery.Core.Models.Documents;
using PlateDelivery.DataLayer.Entities.AccountAgg.Repository;
using PlateDelivery.DataLayer.Entities.CertainAgg.Repository;
using PlateDelivery.DataLayer.Entities.DocumentAgg;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;
using PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;

namespace PlateDelivery.Core.Services.Documents;

internal class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repository;
    private readonly IProvinceRepository _provinceRepository;
    private readonly ICertainRepository _certainRepository;
    private readonly IAccountRepository _accountRepository;

    public DocumentService(IDocumentRepository repository,
        IProvinceRepository provinceRepository, ICertainRepository certainRepository,
        IAccountRepository accountRepository)
    {
        _repository = repository;
        _provinceRepository = provinceRepository;
        _certainRepository = certainRepository;
        _accountRepository = accountRepository;
    }

    //سند عادی یک سطر بانک یک سطر درآمد یک سطر مالیات
    public long CreateDocument(TopYarTmp topYar, ServiceCoding service)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            var maxOrder = _repository.GetMaxOrder();
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
                decimal income = (decimal.Parse(topYar.Amount) * 100) / 109;
                decimal tax = decimal.Parse(topYar.Amount) - decimal.Round(income);


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString());
                documents.Add(incomeRecord);
                //tax record
                var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", tax.ToString());
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                _repository.SaveSync();
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
                decimal income = (decimal.Parse(topYar.Amount) * 100) / 109;
                decimal tax = decimal.Parse(topYar.Amount) - decimal.Round(income);


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", decimal.Round(income).ToString());
                documents.Add(incomeRecord);
                //tax record
                var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", tax.ToString());
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                _repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    //سند 5 سطری. برای مواردی که تسهیم دارند. باید یک سطر بانک 2 سطر درآمد و 2 سطر مالیات باشد
    public long CreateDocument(TopYarTmp topYar, List<ServiceCoding> services)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            var maxOrder = _repository.GetMaxOrder();
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


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);


                foreach (var service in services)
                {
                    var ServiceCertain = _certainRepository.Get(service.CertainId);
                    decimal income = (decimal.Parse(service.Amount) * 109) / 100;
                    decimal tax = decimal.Round(income) - decimal.Parse(service.Amount);

                    //income record
                    var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                        , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                        , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                        , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                        , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                        province.ProvinceCode, service.CodeLevel6, description, "0", service.Amount);
                    documents.Add(incomeRecord);
                    //tax record
                    var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                        , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                        , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                        , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                        , province.CodeLevel4 ?? service.CodeLevel4, null, null
                        , description, "0", tax.ToString());
                    documents.Add(taxRecord);
                }

                _repository.AddRange(documents);
                _repository.SaveSync();
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


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, services.First().ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);

                foreach (var service in services)
                {
                    var ServiceCertain = _certainRepository.Get(service.CertainId);
                    decimal income = (decimal.Parse(service.Amount) * 109) / 100;
                    decimal tax = decimal.Round(income) - decimal.Parse(service.Amount);

                    //income record
                    var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                        , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                        , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                        , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                        , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                        province.ProvinceCode, service.CodeLevel6, description, "0", service.Amount);
                    documents.Add(incomeRecord);
                    //tax record
                    var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                        , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                        , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                        , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                        , province.CodeLevel4 ?? service.CodeLevel4, null, null
                        , description, "0", tax.ToString());
                    documents.Add(taxRecord);
                }

                _repository.AddRange(documents);
                _repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    //برای حالتی است که رکورد فقط درآمد خالص است و مالیات جداگانه در گزارش تاپ یار آمده است
    public long CreateIncomeDocument(TopYarTmp topYar, ServiceCoding service)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            var maxOrder = _repository.GetMaxOrder();
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


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", topYar.Amount);
                documents.Add(incomeRecord);

                _repository.AddRange(documents);
                _repository.SaveSync();
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


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);

                //income record
                var incomeRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, ServiceCertain.CertainCode
                    , (province.CodeLevel4 != null) ? province.CodeLevel4 : service.CodeLevel4,
                    province.ProvinceCode, service.CodeLevel6, description, "0", topYar.Amount);
                documents.Add(incomeRecord);

                _repository.AddRange(documents);
                _repository.SaveSync();
                return bankRecord.Order;
            }
        }
        return -1;
    }

    //برای حالتی است که رکورد فقط مالیات است و درآمد جداگانه در گزارش تاپ یار آمده است
    public long CreateTaxDocument(TopYarTmp topYar, ServiceCoding service)
    {
        if (!_repository.Exists(y => y.RetrivalRef == topYar.RetrivalRef
                && y.ServiceCode == topYar.ServiceCode && y.Amount == topYar.Amount))
        {
            var maxOrder = _repository.GetMaxOrder();
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


                //bank record
                var bankRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);

                //tax record
                var taxRecord = new Document(1, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", topYar.Amount);
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                _repository.SaveSync();
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


                //bank record
                var bankRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, bankCertain.CertainCode, account.BankCode,
                    "9999999999", null, description, topYar.Amount, "0");
                documents.Add(bankRecord);

                //tax record
                var taxRecord = new Document(maxOrder, topYar.RetrivalRef, topYar.TrackingNo, topYar.TransactionDate
                    , topYar.TransactionTime, topYar.FinancialDate, topYar.Iban, topYar.Amount, topYar.PrincipalAmount
                    , topYar.CardNo, topYar.Terminal, topYar.InstallationPlace, topYar.ServiceCode, service.ServiceName
                    , topYar.ProvinceName, topYar.SubProvince, null, taxCertain.CertainCode
                    , province.CodeLevel4 ?? service.CodeLevel4, null, null
                    , description, "0", topYar.Amount);
                documents.Add(taxRecord);

                _repository.AddRange(documents);
                _repository.SaveSync();
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
}
