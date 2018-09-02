using System.Runtime.Serialization;

namespace BetaViews.Messages.Dtos
{
    
    public class TokenAuthorizationDTO
    {
        [IgnoreDataMember]
        public string Authorization { get; set; }
    }
}
