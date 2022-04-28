using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Elev8NetCoreApp.Models
{
    public class EmployeeDetailsInfo
    {
        public int Id { get; set; }
        [Display(Name="Surname:")]
        [Required(ErrorMessage ="Surname is Required.")]
        public string surName { get; set; }
        [Display(Name = "Other Names:")]

        
        public string otherNames { get; set; }
        [Display(Name = "Mobile No:")]
        [Required(ErrorMessage = "Mobile is Required.")]
        public string mobileNo { get; set; }
        [Display(Name = "Address:")]
        public string address { get; set; }
        [Display(Name = "Gender:")]
        public string gender { get; set; }

        public int CreateEmployeeRecord(EmployeeDetailsInfo employeesInfo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Startup.connectionString))
                {
                    //Insert into [TableName](Properties) Values(content)
                    String query = "INSERT INTO EmployeesDetailsTbl(Surname,OtherNames,Gender, MobileNo,Address) Values(@Surname,@OtherNames,@Gender, @MobileNo,@Address)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Surname", employeesInfo.surName);
                        command.Parameters.AddWithValue("@OtherNames", employeesInfo.otherNames);
                        command.Parameters.AddWithValue("@Gender", employeesInfo.gender);
                        command.Parameters.AddWithValue("@MobileNo", employeesInfo.mobileNo);
                        command.Parameters.AddWithValue("@Address", employeesInfo.address);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        return result;


                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return -1;
            }

        }

        public DataTable ReturnEmployeesRecord()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                String sql = "SELECT * FROM EmployeesDetailsTbl order by Id desc";

                SqlCommand sqlcom = new SqlCommand(sql, connection);
                try
                {
                    sqlcom.Connection.Open();
                    sqlcom.ExecuteNonQuery();
                    SqlDataReader reader = sqlcom.ExecuteReader();

                    dt.Load(reader);
                    
                }
                catch (SqlException ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }


            return dt;
        }

        public DataTable ReturnEmployeesRecord(int Id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(Startup.connectionString ))
            {
                String sql = "SELECT * FROM EmployeesDetailsTbl where Id=@Id";
                using (SqlCommand sqlcom = new SqlCommand(sql, connection))
                {

                    try
                    {
                        sqlcom.Parameters.AddWithValue("@Id", Id);
                        sqlcom.Connection.Open();
                        sqlcom.ExecuteNonQuery();
                        SqlDataReader reader = sqlcom.ExecuteReader();

                        dt.Load(reader);

                    }
                    catch (SqlException ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }

                }


            }


            return dt;
        }

    }
}
