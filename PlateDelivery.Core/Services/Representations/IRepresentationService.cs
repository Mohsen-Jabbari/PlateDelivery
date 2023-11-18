using PlateDelivery.Core.Models;
using PlateDelivery.Core.Models.Representations;

namespace PlateDelivery.Core.Services.Representations;
public interface IRepresentationService
{
    long CreateRepresentation(CreateRepresentationViewModel model);
    bool EditRepresentation(EditRepresentationViewModel model);
    bool DeleteRepresentation(long Id);
    bool RestoreRepresentation(long Id);

    bool IsRepresentationCodeExists(string Id);

    EditRepresentationViewModel GetById(long Id);
    RepresentationViewModel GetRepresentations(int pageId = 1, int take = 10, string? filterByName = "", string? filterByBrokerCode = "");
    RepresentationViewModel GetDeletedRepresentations(int pageId = 1, int take = 10, string? filterByName = "", string? filterByBrokerCode = "");
}
