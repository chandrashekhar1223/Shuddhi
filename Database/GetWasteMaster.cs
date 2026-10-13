using Shuddhi.Model;
using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class GetWasteMaster
    {

        private readonly string connStr;

        public GetWasteMaster(IConfiguration configuration)
        {
            connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public WasteMasterResponse GetWasteMaster1()
        {
            WasteMasterResponse response = new WasteMasterResponse();
            List<WasteTypeModel> wasteTypes = new List<WasteTypeModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetWasteMaster", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int wasteTypeId = Convert.ToInt32(dr["WasteTypeId"]);

                                WasteTypeModel wasteType = wasteTypes.FirstOrDefault(x => x.WasteTypeId == wasteTypeId);

                                if (wasteType == null)
                                {
                                    wasteType = new WasteTypeModel
                                    {
                                        WasteTypeId = wasteTypeId,
                                        WasteType = dr["WasteType"].ToString(),
                                        SubWasteTypes = new List<WasteSubTypeModel>()
                                    };

                                    wasteTypes.Add(wasteType);
                                }

                                wasteType.SubWasteTypes.Add(new WasteSubTypeModel
                                {
                                    WasteSubTypeId = Convert.ToInt32(dr["WasteSubTypeId"]),
                                    WasteSubType = dr["WasteSubType"].ToString()
                                });
                            }
                        }
                    }




                    response.OutputStatus = "Success";
                    response.WasteTypes = wasteTypes;

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.OutputStatus = "Error: " + ex.Message;
                response.WasteTypes = null;
                return response;


            }
        }
    }
}
