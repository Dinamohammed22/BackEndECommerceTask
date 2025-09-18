namespace KOG.ECommerce.Features.Common.Companies.DTOs
{
    public record ExportCompanyDTO(byte[] FileContent, string FileName, string ContentType);
}
