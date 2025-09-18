using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Emails;

namespace KOG.ECommerce.Features.Common.Emails.DTOs
{
    public class EmailDTO
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> EmailAdresses { get; set; }

    }
   
}
