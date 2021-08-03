using InterviewDemo.Models.DAO;
using InterviewDemo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewDemo.Models.BO
{
    public class LoadLossTypeDetailsBO
    {
        public ListLossTypesDTO GetLossTypeData(int currentPage)
        {
            return new LoadLossTypeDetailsDAO().GetLossTypeData(currentPage);
        }
    }
}
