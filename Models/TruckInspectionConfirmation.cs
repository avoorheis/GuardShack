public class TruckInspectionConfirmation
{
    public int SignatureID { get; set; }
    public int? OrderNumber { get; set; }
    public string Signature { get; set; }
    public bool? ProductDischarge { get; set; }
    public bool? HoseHousing { get; set; }
    public DateTime? SignatureDate { get; set; }
}