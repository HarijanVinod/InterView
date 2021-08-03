using InterviewDemo.Models.DTO;
using InterviewDemo.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewDemo.Models.DAO
{
    public class LoadLossTypeDetailsDAO
    {
        public ListLossTypesDTO GetLossTypeData(int currentPage)
        {
            try
            {
                int maxRows = 10;
                ListLossTypesDTO listlosstypesDTO = new ListLossTypesDTO();
                using (var dbcontext = new InterviewEntities())
                {
                    listlosstypesDTO.lstlosstypesDTO = new List<LossTypesDTO>();

                    var result = (from losstype in dbcontext.LossTypes
                                  select losstype)
                                .OrderBy(losstype => losstype.LossTypeId)
                                .Skip((currentPage - 1) * maxRows)
                                .Take(maxRows).ToList();

                    foreach (var item in result)
                    {
                        LossTypesDTO objlosstypeDTO = new LossTypesDTO()
                        {
                            LossTypeId = item.LossTypeId,
                            LossTypeCode = item.LossTypeCode,
                            LossTypeDescription = item.LossTypeDescription,
                        };

                        listlosstypesDTO.lstlosstypesDTO.Add(objlosstypeDTO);
                    }

                    double pageCount = (double)((decimal)dbcontext.LossTypes.Count() / Convert.ToDecimal(maxRows));
                    listlosstypesDTO.PageCount = (int)Math.Ceiling(pageCount);

                    listlosstypesDTO.CurrentPageIndex = currentPage;

                }
                return listlosstypesDTO;
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
    
}