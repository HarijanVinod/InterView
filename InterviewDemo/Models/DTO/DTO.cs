using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InterviewDemo.Models.DTO
{
    [Serializable, DataContract]
    public class UsersDTO
    {
        [DataMember] public int UserId { get; set; }
        [DataMember] public string UserName { get; set; }
        [DataMember] public string Password { get; set; }
        [DataMember] public string DisplayName { get; set; }
        [DataMember] public bool Active { get; set; }
    }
    public class UsersParamDTO
    {
        [DataMember] public string UserName { get; set; }
        [DataMember] public string Password { get; set; }
        [DataMember] public bool RememberMe { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ApplicationDTO
    {
        [DataMember] public string AccessToken { get; set; }
        [DataMember] public string AuthToken { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UserName { get; set; }
        [DataMember] public object RememberMe { get; set; }
        [DataMember] public bool IsAuthenticated { get; set; }

        [DataMember]
        public int ResultCode { get; set; }

        [DataMember]
        public string ResultMessage { get; set; }
    }

    [Serializable, DataContract]
    public class LossTypesDTO
    {
        [DataMember] public int LossTypeId { get; set; }
        [DataMember] public string LossTypeCode { get; set; }
        [DataMember] public string LossTypeDescription { get; set; }
    }

    [Serializable, DataContract]
    public class ListLossTypesDTO
    {
        [DataMember] public List<LossTypesDTO> lstlosstypesDTO { get; set; }
        [DataMember] public int CurrentPageIndex { get; set; }
        [DataMember] public int PageCount { get; set; }
    }
}
