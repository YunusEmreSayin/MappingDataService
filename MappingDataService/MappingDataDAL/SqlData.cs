using MappingDataService.Models;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MappingDataService.MappingDataDAL
{
    public class SqlData
    {
        public readonly IConfiguration _configuration;
        public string _connectionString;
        public string _QBAR_getDATA_Cleared;
        public SqlData(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _QBAR_getDATA_Cleared = _configuration["StoredProcedures:QBAR_getDATA_Cleared"];
            
        }
        public async Task<List<MapData>> runSp(int IslemId)
        {
            List<MapData> list = new List<MapData>();

            try
            {


                using (SqlConnection con=new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand(_QBAR_getDATA_Cleared, con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@IslemID", IslemId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            MapData mapData = new MapData();
                           
                            mapData.DefectPosX = (decimal)reader["DefectPosX"];
                            mapData.DefectPosY = (decimal)reader["DefectPosY"];

                            list.Add(mapData);

                        }
                    }
                                            
                   await con.CloseAsync();
                }

            }
            
            catch (Exception e)
            {
                throw new Exception("Hata",e);
               
            }


            return list;
        }
    }
}
