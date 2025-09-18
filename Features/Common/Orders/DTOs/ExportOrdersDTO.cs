namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record ExportOrdersDTO(byte[] FileContent, string FileName, string ContentType);
}
