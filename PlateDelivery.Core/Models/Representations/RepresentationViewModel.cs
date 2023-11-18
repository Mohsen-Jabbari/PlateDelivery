using PlateDelivery.DataLayer.Entities.RepresentationAgg;
using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.Core.Models.Representations;
public class CreateRepresentationViewModel : BaseDto
{
    public string RepresentationCode { get; set; }
    public string ValidityDate { get; set; }
    public string BrokerName { get; set; }
    public string BrokerCode { get; set; }
    public string BrokerTell { get; set; }
    public string BrokerMobile { get; set; }
    public string Extra { get; set; }
    public string BrokerCodeR { get; set; }
}

public class EditRepresentationViewModel : BaseDto
{
    public string RepresentationCode { get; set; }
    public string ValidityDate { get; set; }
    public string BrokerName { get; set; }
    public string BrokerCode { get; set; }
    public string BrokerTell { get; set; }
    public string BrokerMobile { get; set; }
    public string Extra { get; set; }
    public string BrokerCodeR { get; set; }
}

public class RepresentationViewModel
{
    public List<Representation> Representations { get; set; }
    public int RepresentationCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int LastPage { get; set; }
    public int PrevPage { get; set; }
    public int NextPage { get; set; }
}