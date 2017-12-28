using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;
namespace WebApplication1.@base
{
	public class consulta
	{
        public IList<modelo> getimag(String key , String Id)
        {
            ConnectionStringSettings strConnString = ConfigurationManager.ConnectionStrings["MardisEngine_TestEntities"];
            IList<modelo> slmodel =new List<modelo>();
            try
            {
                using (SqlConnection cn = new SqlConnection(strConnString.ToString()))
                {
                    cn.Open();
                    string commandText = "Select  Title,value,Code,idimg From MardisCommon.vw_fotos_mardisp "
                                         + "WHERE [key] = @key and id=@id;";
                    SqlCommand command = new SqlCommand(commandText, cn);

                    command.Parameters.Add("@key", SqlDbType.VarChar);
                    command.Parameters.Add("@id", SqlDbType.VarChar);
                    command.Parameters["@id"].Value = Id;
                    command.Parameters["@key"].Value = key;
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        slmodel.Add(new modelo
                        {
                            name = dr["Title"].ToString(),
                            base64 = String.Format("data:image/gif;base64,{0}" ,Convert.ToBase64String((byte[])dr["value"])),
                            Code = dr["Code"].ToString(),
                            nameImg = dr["idimg"].ToString(),
                            uri=Id

                        });


                    }

                }
                return slmodel;
            }
            catch (Exception e)
            {
                
                return null;
            }
           
          
            
        }
        public   IList<ValidateModel> GetVAlidate(String Id)
        {
            ConnectionStringSettings strConnString = ConfigurationManager.ConnectionStrings["MardisEngine_TestEntities"];
            IList<ValidateModel>  model = new List<ValidateModel> ();
           ValidateModel slmodel = new ValidateModel();
            try
            {
                using (SqlConnection cn = new SqlConnection(strConnString.ToString()))
                {
                    
   

                    cn.Open();
                    string commandText = "Select  AggregateUri,typeObs,description From MardisCore.validate_project "
                                         + "WHERE  AggregateUri=@id;";
                    SqlCommand command = new SqlCommand(commandText, cn);
                    command.Parameters.Add("@id", SqlDbType.VarChar);
                    command.Parameters["@id"].Value = Id;
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {

                        slmodel.AggregateUri = dr["AggregateUri"].ToString();
                        slmodel.typeObs = bool.Parse(dr["typeObs"].ToString());
                        slmodel.description = dr["description"].ToString();
                    }
                    model.Add(slmodel);
                }
                return model;
            }
            catch (Exception)
            {

                return null;
            }



        }
        public int SAveVAlidate(ValidateModel dato)
        {
            ConnectionStringSettings strConnString = ConfigurationManager.ConnectionStrings["MardisEngine_TestEntities"];
            IList<ValidateModel> model = new List<ValidateModel>();
            ValidateModel slmodel = new ValidateModel();
            try
            {
                using (SqlConnection cn = new SqlConnection(strConnString.ToString()))
                {


                    string commandText;
                    SqlCommand command;
                    cn.Open();
                    string commandText1 = "Select 1 From MardisCore.validate_project "
                                        + "WHERE  AggregateUri=@id;";
                    SqlCommand command1 = new SqlCommand(commandText1, cn);
                    command1.Parameters.Add("@id", SqlDbType.VarChar);
                    command1.Parameters["@id"].Value = dato.AggregateUri;
                    var i =  command1.ExecuteScalar();
                    if (i !=null)
                    {
                        commandText = "update MardisCore.validate_project set typeObs=@estado,description=@descrip,user_web=@usuario,DateValidation=getdate() "
                                  + "WHERE  AggregateUri=@id;";

                        command = new SqlCommand(commandText, cn);
                        command.Parameters.Add("@id", SqlDbType.VarChar);
                        command.Parameters["@id"].Value = dato.AggregateUri;
                        command.Parameters.Add("@estado", SqlDbType.Bit);
                        command.Parameters["@estado"].Value = dato.typeObs;
                        command.Parameters.Add("@descrip", SqlDbType.VarChar);
                        command.Parameters["@descrip"].Value = dato.description;

                        command.Parameters.Add("@usuario", SqlDbType.VarChar);
                        command.Parameters["@usuario"].Value = dato.user_web;
                        int fila = command.ExecuteNonQuery();
                    }
                    else {
                        commandText = "INSERT into MardisCore.validate_project VALUES( "
                                           + "newid(), @id,@estado,@descrip,@usuario ,GETDATE())";

                        command = new SqlCommand(commandText, cn);
                        command.Parameters.Add("@id", SqlDbType.VarChar);
                        command.Parameters["@id"].Value = dato.AggregateUri;
                        command.Parameters.Add("@estado", SqlDbType.Bit);
                        command.Parameters["@estado"].Value = dato.typeObs;
                        command.Parameters.Add("@descrip", SqlDbType.VarChar);
                        command.Parameters["@descrip"].Value = dato.description;

                        command.Parameters.Add("@usuario", SqlDbType.VarChar);
                        command.Parameters["@usuario"].Value = dato.user_web;
                        int fila = command.ExecuteNonQuery();
                    }

                }
                return 1;
            }
            catch (Exception e)
            {

                return 0;
            }



        }
    }
}