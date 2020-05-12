using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for cBaseDatos
/// </summary>
///     
public class accesoBD
    {
        public static string ObtenerConexión()
        {
            string strConexión;
            strConexión = ConfigurationManager.ConnectionStrings["db_conexion"].ConnectionString;
            return strConexión;
        }
    }
public class cBaseDatos
{
    public static bool spError(string err_Nombre, string err_Parameters, string err_Data, string err_HelpLink, string err_InnerException, string err_Message, string err_Source, string err_StackTrace, DateTime err_fecha, string err_tipo)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spError", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paErr_Nombre = cmdComandoInicio.Parameters.Add("@err_Nombre", SqlDbType.NVarChar, -1);
        SqlParameter paErr_Parameters = cmdComandoInicio.Parameters.Add("@err_Parameters", SqlDbType.NVarChar, -1);
        SqlParameter paErr_Data = cmdComandoInicio.Parameters.Add("@err_Data", SqlDbType.NVarChar, -1);
        SqlParameter paErr_HelpLink = cmdComandoInicio.Parameters.Add("@err_HelpLink", SqlDbType.NVarChar, -1);
        SqlParameter paErr_InnerException = cmdComandoInicio.Parameters.Add("@err_InnerException", SqlDbType.NVarChar, -1);
        SqlParameter paErr_Message = cmdComandoInicio.Parameters.Add("@err_Message", SqlDbType.NVarChar, -1);
        SqlParameter paErr_Source = cmdComandoInicio.Parameters.Add("@err_Source", SqlDbType.NVarChar, -1);
        SqlParameter paErr_StackTrace = cmdComandoInicio.Parameters.Add("@err_StackTrace", SqlDbType.NVarChar, -1);
        SqlParameter paErr_tipo = cmdComandoInicio.Parameters.Add("@err_tipo", SqlDbType.NVarChar, 200);
        SqlParameter paErr_fecha = cmdComandoInicio.Parameters.Add("@err_fecha", SqlDbType.DateTime);
        if (err_Nombre == null)
        {
            paErr_Nombre.Value = DBNull.Value;
        }
        else
        {
            paErr_Nombre.Value = err_Nombre;
        }
        if (err_Parameters == null)
        {
            paErr_Parameters.Value = DBNull.Value;
        }
        else
        {
            paErr_Parameters.Value = err_Parameters;
        }
        if (err_Data == null)
        {
            paErr_Data.Value = DBNull.Value;
        }
        else
        {
            paErr_Data.Value = err_Data;
        }
        if (err_HelpLink == null)
        {
            paErr_HelpLink.Value = DBNull.Value;
        }
        else
        {
            paErr_HelpLink.Value = err_HelpLink;
        }
        if (err_HelpLink == null)
        {
            paErr_HelpLink.Value = DBNull.Value;
        }
        else
        {
            paErr_HelpLink.Value = err_HelpLink;
        }

        if (err_tipo == null)
        {
            paErr_tipo.Value = DBNull.Value;
        }
        else
        {
            paErr_tipo.Value = err_tipo;
        }
        if (err_StackTrace == null)
        {
            paErr_StackTrace.Value = DBNull.Value;
        }
        else
        {
            paErr_StackTrace.Value = err_StackTrace;
        }
        if (err_Source == null)
        {
            paErr_Source.Value = DBNull.Value;
        }
        else
        {
            paErr_Source.Value = err_Source;
        }
        if (err_Message == null)
        {
            paErr_Message.Value = DBNull.Value;
        }
        else
        {
            paErr_Message.Value = err_Message;
        }
        if (err_InnerException == null)
        {
            paErr_InnerException.Value = DBNull.Value;
        }
        else
        {
            paErr_InnerException.Value = err_InnerException;
        }
        paErr_fecha.Value = err_fecha;

        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }

}
