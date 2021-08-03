using InterviewDemo.Models.DAO;
using InterviewDemo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewDemo.Models.BO
{
    public class AuthenticationBO
    {
        public UsersDTO ValidateUser(UsersParamDTO usersParamDTO)
        {
            return new AuthenticationDAO().ValidateUser(usersParamDTO);
        }
    }
}
