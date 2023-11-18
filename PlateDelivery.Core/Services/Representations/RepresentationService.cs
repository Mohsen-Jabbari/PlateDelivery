using PlateDelivery.Core.Models;
using PlateDelivery.Core.Models.Representations;
using PlateDelivery.DataLayer.Entities.RepresentationAgg;
using PlateDelivery.DataLayer.Entities.RepresentationAgg.Repository;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;

namespace PlateDelivery.Core.Services.Representations;

internal class RepresentationService : IRepresentationService
{
    private readonly IRepresentationRepository _repository;

    public RepresentationService(IRepresentationRepository repository)
    {
        _repository = repository;
    }

    public long CreateRepresentation(CreateRepresentationViewModel model)
    {
        if (!_repository.Exists(u => u.BrokerCode == model.BrokerCode))
        {
            var represantation = new Representation(model.RepresentationCode, model.ValidityDate, model.BrokerName, model.BrokerCode,
                model.BrokerTell, model.BrokerMobile, model.Extra, model.BrokerCodeR, false);
            _repository.Add(represantation);
            _repository.SaveSync();
            return represantation.Id;
        }
        return -1;
    }

    public bool DeleteRepresentation(long Id)
    {
        var oldRepresantation = _repository.GetTrackingSync(Id);
        if (oldRepresantation != null)
        {
            oldRepresantation.SetIsDeleteFalse(true);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public bool RestoreRepresentation(long Id)
    {
        var oldRepresantation = _repository.GetTrackingSync(Id);
        if (oldRepresantation != null)
        {
            oldRepresantation.SetIsDeleteTrue(false);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public bool EditRepresentation(EditRepresentationViewModel model)
    {
        var oldRepresantation = _repository.GetTrackingSync(model.Id);
        if (oldRepresantation != null)
        {
            oldRepresantation.Edit(model.RepresentationCode, model.ValidityDate, model.BrokerName, model.BrokerCode,
                model.BrokerTell, model.BrokerMobile, model.Extra, model.BrokerCodeR, false);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public EditRepresentationViewModel GetById(long Id)
    {
        var representation = _repository.GetTrackingSync(Id);
        if (representation != null)
            return new EditRepresentationViewModel()
            {
                BrokerCode = representation.BrokerCode,
                BrokerTell = representation.BrokerTell,
                BrokerMobile = representation.BrokerMobile,
                Extra = representation.Extra,
                BrokerCodeR = representation.BrokerCodeR,
                BrokerName = representation.BrokerName,
                CreationDate = representation.CreationDate,
                Id = representation.Id,
                RepresentationCode = representation.RepresentationCode,
                ValidityDate = representation.ValidityDate
            };
        return new EditRepresentationViewModel()
        {
            Id = -1
        };
    }

    public RepresentationViewModel GetRepresentations(int pageId = 1, int take = 10, string? filterByName = "", string? filterByBrokerCode = "")
    {
        var result = _repository.GetAll().Where(u => u.IsDelete == false).ToList();  //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByName))
            {
                result = result.Where(u => u.BrokerName.Contains(filterByName)).ToList();
            }

            if (!string.IsNullOrEmpty(filterByBrokerCode))
            {
                result = result.Where(u => u.BrokerCode.Contains(filterByBrokerCode) || u.BrokerCodeR.Contains(filterByBrokerCode)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            RepresentationViewModel list = new RepresentationViewModel();
            list.Representations = result.OrderByDescending(u => u.CreationDate).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.LastPage = list.PageCount;
            list.PrevPage = Math.Max(pageId - 1, list.CurrentPage);
            list.NextPage = Math.Max(pageId + 1, list.LastPage);
            list.RepresentationCounts = result.Count;
            return list;
        }
        return new RepresentationViewModel();
    }

    public RepresentationViewModel GetDeletedRepresentations(int pageId = 1, int take = 10, string? filterByName = "", string? filterByBrokerCode = "")
    {
        var result = _repository.GetAll().Where(u => u.IsDelete == true).ToList();  //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByName))
            {
                result = result.Where(u => u.BrokerName.Contains(filterByName)).ToList();
            }

            if (!string.IsNullOrEmpty(filterByBrokerCode))
            {
                result = result.Where(u => u.BrokerCode.Contains(filterByBrokerCode) || u.BrokerCodeR.Contains(filterByBrokerCode)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            RepresentationViewModel list = new RepresentationViewModel();
            list.Representations = result.OrderByDescending(u => u.CreationDate).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.LastPage = list.PageCount;
            list.PrevPage = Math.Max(pageId - 1, list.CurrentPage);
            list.NextPage = Math.Max(pageId + 1, list.LastPage);
            list.RepresentationCounts = result.Count;
            return list;
        }
        return new RepresentationViewModel();
    }

    public bool IsRepresentationCodeExists(string Id)
    {
        return _repository.Exists(u => u.RepresentationCode == Id);
    }
}