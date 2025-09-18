namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record ExportClientsDTO(byte[] FileContent, string FileName, string ContentType);
}
