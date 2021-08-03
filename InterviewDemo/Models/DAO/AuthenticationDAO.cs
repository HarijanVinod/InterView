using InterviewDemo.Models.DTO;
using InterviewDemo.Models.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewDemo.Models.DAO
{
    public class AuthenticationDAO
    {

        public UsersDTO ValidateUser(UsersParamDTO usersParamDTO)
        {
            try
            {
                using (var dbcontext = new InterviewEntities())
                {
                    User user = dbcontext.Users.Where(query => query.UserName.Equals(usersParamDTO.UserName) && query.Password.Equals(usersParamDTO.Password) && 
                                query.Active == true).SingleOrDefault();

                    if (user == null)
                        return null;
                    else
                    {
                        UsersDTO objuser = new UsersDTO();
                        objuser.UserId = user.UserId;
                        objuser.UserName = user.UserName;
                        objuser.DisplayName = user.DisplayName;
                        objuser.Active = user.Active;
                        return objuser;
                    }
                        
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
